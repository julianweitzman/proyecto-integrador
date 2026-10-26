// ============================================================================
//  CsvManager.cs  (parte de DataBases)
//  Guarda y lee tablas en archivos CSV. Los archivos, columnas y miembros salen del
//  EntidadModelo (configurado con ConstructorModelo); ya no se usan atributos.
//  Requisitos: .NET 8 o superior (ArgumentException.ThrowIfNullOrWhiteSpace).
// ============================================================================

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataBases
{
    // ───────────────────────── Excepciones propias ─────────────────────────

    /// <summary>Base de todas las excepciones de CsvManager.</summary>
    public class CsvException : Exception
    {
        public CsvException(string mensaje, Exception? inner = null) : base(mensaje, inner) { }
    }

    /// <summary>El CSV (datos externos) tiene un problema. Incluye línea y columna.</summary>
    public class CsvFormatException : CsvException
    {
        public int Linea { get; }
        public string? Columna { get; }

        public CsvFormatException(string mensaje, int linea, string? columna = null, Exception? inner = null)
            : base($"Línea {linea}{(columna != null ? $", columna '{columna}'" : "")}: {mensaje}", inner)
        {
            Linea = linea;
            Columna = columna;
        }
    }

    /// <summary>La clase T no cumple las reglas del sistema (error del programador, no del CSV).</summary>
    public class CsvConfiguracionException : CsvException
    {
        public CsvConfiguracionException(string mensaje) : base(mensaje) { }
    }

    // ───────────────────────────── CsvManager ──────────────────────────────

    internal static class CsvManager
    {
        // Evita que dos hilos lean o escriban archivos al mismo tiempo.
        private static readonly object bloqueo = new();

        // Texto con el que se escribe un valor null. Solo vale como null si va sin comillas:
        // un string cuyo contenido es "null" se escribe entre comillas y no se confunde.
        private const string NullTexto = "null";

        // Una celda leída: el texto y si venía entre comillas.
        // Permite distinguir "" (texto vacío) de una celda vacía sin comillas (null).
        private readonly record struct Celda(string Texto, bool EntreComillas);

        // ───────────────────────────── ESCRITURA ─────────────────────────────

        internal static void Escribir<T>(EntidadModelo entidad, IEnumerable<T> datos)
        {
            ArgumentNullException.ThrowIfNull(datos);

            string archivoPath = entidad.Ruta;
            IReadOnlyList<ColumnaModelo> columnas = entidad.Columnas;

            List<string> lineas = new();

            lineas.Add(string.Join(",", columnas.Select(c => c.Nombre)));

            foreach (T dato in datos)
            {
                if (dato is null)
                    throw new ArgumentException("La tabla contiene un elemento null.", nameof(datos));

                lineas.Add(string.Join(",",
                    columnas.Select(c => FormatearValor(c.Miembro.Leer(dato), c.Miembro.Tipo))));
            }

            lock (bloqueo)
            {
                string rutaCompleta = Path.GetFullPath(archivoPath);
                string? directorio = Path.GetDirectoryName(rutaCompleta);

                if (!string.IsNullOrEmpty(directorio))
                    Directory.CreateDirectory(directorio);

                // Escritura atómica: se escribe en un archivo temporal y luego se reemplaza
                // el original, de modo que un corte a mitad no daña el archivo existente.
                string temporal = rutaCompleta + ".tmp";

                try
                {
                    File.WriteAllLines(temporal, lineas);
                    File.Move(temporal, rutaCompleta, overwrite: true);
                }
                catch
                {
                    try { if (File.Exists(temporal)) File.Delete(temporal); } catch { /* ignorar */ }
                    throw;
                }
            }
        }

        // ────────────────────────────── LECTURA ──────────────────────────────

        /// <summary>Lee el CSV de la entidad. Si no existe: lista vacía, o FileNotFoundException con exigirArchivo.</summary>
        internal static List<T> Leer<T>(EntidadModelo entidad, bool exigirArchivo)
        {
            string archivoPath = entidad.Ruta;

            if (!File.Exists(archivoPath))
            {
                if (exigirArchivo)
                    throw new FileNotFoundException($"El archivo CSV no existe: {archivoPath}", archivoPath);

                return new List<T>();
            }

            IReadOnlyList<ColumnaModelo> columnas = entidad.Columnas;

            string[] lineas;
            lock (bloqueo)
                lineas = File.ReadAllLines(archivoPath);

            // Archivo vacío o con solo líneas en blanco → lista vacía.
            int indiceEncabezado = Array.FindIndex(lineas, l => !string.IsNullOrWhiteSpace(l));
            if (indiceEncabezado < 0)
            {
                return new List<T>();
            }

            // ── Encabezado ──
            int numeroEncabezado = indiceEncabezado + 1;
            List<Celda> celdasEncabezado = ParsearLinea(lineas[indiceEncabezado], numeroEncabezado);
            string[] nombres = celdasEncabezado.Select(c => c.Texto.Trim()).ToArray();

            // Excel en configuración regional argentina suele guardar con ';'.
            if (nombres.Length == 1 && nombres[0].Contains(';'))
                throw new CsvFormatException(
                    "Parece que el archivo usa ';' como separador. Este formato requiere ','.",
                    numeroEncabezado);

            string[] duplicadas = nombres.GroupBy(n => n).Where(g => g.Count() > 1).Select(g => g.Key).ToArray();
            if (duplicadas.Length > 0)
                throw new CsvFormatException(
                    $"Columnas duplicadas en el encabezado: {string.Join(", ", duplicadas)}.",
                    numeroEncabezado);

            // Columnas del archivo que la clase no define.
            string[] desconocidas = nombres.Where(n => !columnas.Any(c => c.Nombre == n)).ToArray();
            if (desconocidas.Length > 0)
                throw new CsvFormatException(
                    $"Las columnas [{string.Join(", ", desconocidas)}] no existen en {typeof(T).Name}.",
                    numeroEncabezado);

            // Columnas de la clase que faltan en el archivo.
            string[] faltantes = columnas.Select(c => c.Nombre).Where(n => !nombres.Contains(n)).ToArray();
            if (faltantes.Length > 0)
                throw new CsvFormatException(
                    $"Faltan las columnas [{string.Join(", ", faltantes)}] requeridas por {typeof(T).Name}.",
                    numeroEncabezado);

            // Campo correspondiente a cada columna del archivo, en el orden del archivo.
            Miembro[] miembrosOrdenados = nombres
                .Select(n => columnas.First(c => c.Nombre == n).Miembro)
                .ToArray();

            // El constructor se busca una sola vez, no por cada fila.
            (ConstructorInfo constructor, int[] indices) = BuscarConstructor(typeof(T), miembrosOrdenados);
            ParameterInfo[] parametros = constructor.GetParameters();

            // ── Filas ──
            List<T> datos = new();

            for (int i = indiceEncabezado + 1; i < lineas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineas[i]))
                    continue;

                int numeroLinea = i + 1;
                List<Celda> celdas = ParsearLinea(lineas[i], numeroLinea);

                if (celdas.Count != nombres.Length)
                    throw new CsvFormatException(
                        $"Se esperaban {nombres.Length} columnas pero hay {celdas.Count}.",
                        numeroLinea);

                object?[] argumentos = new object?[parametros.Length];

                for (int p = 0; p < parametros.Length; p++)
                {
                    int col = indices[p];
                    argumentos[p] = ConvertirValor(
                        celdas[col],
                        parametros[p].ParameterType,
                        numeroLinea,
                        nombres[col]);
                }

                try
                {
                    datos.Add((T)constructor.Invoke(argumentos));
                }
                catch (TargetInvocationException ex)
                {
                    // El constructor lanzó una excepción (por ejemplo, una validación):
                    // el mensaje útil está en InnerException.
                    Exception causa = ex.InnerException ?? ex;
                    throw new CsvFormatException(
                        $"El constructor de {typeof(T).Name} rechazó los datos: {causa.Message}",
                        numeroLinea,
                        null,
                        causa);
                }
            }

            return datos;
        }

        // ──────────────────────────── REFLECTION ─────────────────────────────

        // true si el tipo admite null: una clase o un Nullable<T>.
        private static bool AceptaNull(Type tipo) =>
            !tipo.IsValueType || Nullable.GetUnderlyingType(tipo) != null;

        // Tipos que CsvManager sabe escribir y leer de vuelta.
        internal static bool EsTipoSoportado(Type tipo)
        {
            Type t = Nullable.GetUnderlyingType(tipo) ?? tipo;

            return t.IsEnum
                || t == typeof(string)
                || t == typeof(decimal)
                || t == typeof(DateTime)
                || t == typeof(DateTimeOffset)
                || t == typeof(DateOnly)
                || t == typeof(TimeOnly)
                || t == typeof(TimeSpan)
                || t == typeof(Guid)
                || (t.IsPrimitive && t != typeof(IntPtr) && t != typeof(UIntPtr));
        }

        internal static void ValidarConstructor(EntidadModelo entidad) =>
            BuscarConstructor(entidad.Tipo, entidad.Columnas.Select(c => c.Miembro).ToArray());

        /// <summary>
        /// Busca un constructor (público o no) cuyos parámetros coincidan con los miembros
        /// por nombre (sin distinguir mayúsculas) y tipo. Devuelve también, para cada
        /// parámetro, el índice de la columna que le corresponde.
        /// </summary>
        private static (ConstructorInfo, int[]) BuscarConstructor(Type tipo, Miembro[] miembrosPorColumna)
        {
            ConstructorInfo[] constructores = tipo.GetConstructors(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (ConstructorInfo constructor in constructores)
            {
                ParameterInfo[] parametros = constructor.GetParameters();

                if (parametros.Length != miembrosPorColumna.Length)
                    continue;

                int[] indices = new int[parametros.Length];
                bool coincide = true;

                for (int p = 0; p < parametros.Length && coincide; p++)
                {
                    int col = Array.FindIndex(miembrosPorColumna, m =>
                        string.Equals(m.Nombre, parametros[p].Name, StringComparison.OrdinalIgnoreCase) &&
                        m.Tipo == parametros[p].ParameterType);

                    if (col < 0)
                        coincide = false;
                    else
                        indices[p] = col;
                }

                if (coincide)
                    return (constructor, indices);
            }

            string esperado = string.Join(", ",
                miembrosPorColumna.Select(m => $"{m.Tipo.Name} {m.Nombre}"));

            throw new CsvConfiguracionException(
                $"No se encontró un constructor compatible para {tipo.Name}. " +
                $"Se necesita uno con los parámetros (en cualquier orden): ({esperado}). " +
                "El nombre de cada parámetro debe coincidir con el del miembro (sin distinguir mayúsculas) y el tipo debe ser idéntico.");
        }

        // ───────────────────────────── FORMATO ───────────────────────────────

        private static string FormatearValor(object? valor, Type tipo)
        {
            // Un valor null se escribe como la palabra null, sin comillas.
            if (valor == null)
                return NullTexto;

            Type real = Nullable.GetUnderlyingType(tipo) ?? tipo;

            // Un enum sin [Flags] cuyo valor no está definido se escribiría como un número
            // que Read rechazaría: se detecta al escribir y no al leer.
            if (real.IsEnum &&
                !real.IsDefined(typeof(FlagsAttribute), false) &&
                !Enum.IsDefined(real, valor))
            {
                throw new ArgumentException(
                    $"El valor {valor} no está definido en el enum {real.Name}; no podría leerse de vuelta.");
            }

            // string, char y enum van siempre entre comillas: un char puede ser una coma
            // y un enum con [Flags] se escribe como "A, B" (con coma).
            if (real == typeof(string) || real == typeof(char) || real.IsEnum)
                return $"\"{EscaparString(valor.ToString()!)}\"";

            // Formatos que se pueden leer de vuelta sin ambigüedad. El orden importa:
            // DateTime y similares también son IFormattable, y el caso general los taparía.
            return valor switch
            {
                DateTime v        => v.ToString("O", CultureInfo.InvariantCulture),
                DateTimeOffset v  => v.ToString("O", CultureInfo.InvariantCulture),
                DateOnly v        => v.ToString("O", CultureInfo.InvariantCulture),
                TimeOnly v        => v.ToString("O", CultureInfo.InvariantCulture),
                TimeSpan v        => v.ToString("c", CultureInfo.InvariantCulture),
                IFormattable v    => v.ToString(null, CultureInfo.InvariantCulture),
                _                 => Convert.ToString(valor, CultureInfo.InvariantCulture)!
            };
        }

        // Reemplaza los caracteres especiales por su secuencia de escape, de modo que un
        // texto con comas, comillas o saltos de línea quede en una sola celda y una sola línea.
        private static string EscaparString(string texto)
        {
            StringBuilder resultado = new();

            foreach (char caracter in texto)
            {
                switch (caracter)
                {
                    case '\\': resultado.Append("\\\\"); break;
                    case '"':  resultado.Append("\\\""); break;
                    case '\n': resultado.Append("\\n");  break;
                    case '\r': resultado.Append("\\r");  break;
                    case '\t': resultado.Append("\\t");  break;
                    default:   resultado.Append(caracter); break;
                }
            }

            return resultado.ToString();
        }

        // ───────────────────────────── PARSEO ────────────────────────────────

        // Separa una línea en celdas respetando comillas y secuencias de escape.
        private static List<Celda> ParsearLinea(string linea, int numeroLinea)
        {
            List<Celda> celdas = new();
            StringBuilder valor = new();

            bool dentroDeString = false;   // dentro de comillas, la coma no separa celdas
            bool escapado = false;         // el carácter anterior fue una barra '\'
            bool huboComillas = false;     // la celda actual tuvo comillas (distingue "" de vacío)

            foreach (char caracter in linea)
            {
                if (escapado)
                {
                    valor.Append(caracter switch
                    {
                        'n' => '\n',
                        'r' => '\r',
                        't' => '\t',
                        _ => caracter
                    });

                    escapado = false;
                    continue;
                }

                if (caracter == '\\')
                {
                    escapado = true;
                    continue;
                }

                if (caracter == '"')
                {
                    dentroDeString = !dentroDeString;
                    huboComillas = true;
                    continue;
                }

                if (caracter == ',' && !dentroDeString)
                {
                    celdas.Add(new Celda(valor.ToString(), huboComillas));
                    valor.Clear();
                    huboComillas = false;
                    continue;
                }

                valor.Append(caracter);
            }

            // Al terminar no puede quedar una barra pendiente ni comillas abiertas.
            if (escapado)
                throw new CsvFormatException("La línea termina con una barra '\\' incompleta.", numeroLinea);

            if (dentroDeString)
                throw new CsvFormatException("Hay comillas sin cerrar.", numeroLinea);

            celdas.Add(new Celda(valor.ToString(), huboComillas));

            return celdas;
        }

        // ─────────────────────────── CONVERSIÓN ──────────────────────────────

        // Convierte el texto de una celda al tipo del parámetro del constructor.
        private static object? ConvertirValor(Celda celda, Type tipo, int numeroLinea, string columna)
        {
            Type? subyacente = Nullable.GetUnderlyingType(tipo);
            bool aceptaNull = AceptaNull(tipo);
            Type real = subyacente ?? tipo;

            // Sin comillas, la palabra null (o una celda vacía) es null.
            // Con comillas ("null" o "") es un texto normal.
            if (!celda.EntreComillas && (celda.Texto == NullTexto || celda.Texto.Length == 0))
            {
                if (aceptaNull)
                    return null;

                throw new CsvFormatException(
                    $"La celda es null pero {tipo.Name} no admite null.",
                    numeroLinea, columna);
            }

            string texto = celda.Texto;
            CultureInfo inv = CultureInfo.InvariantCulture;

            try
            {
                if (real == typeof(string))
                    return texto;

                if (real == typeof(char))
                {
                    if (texto.Length != 1)
                        throw new FormatException("Se esperaba exactamente un carácter.");
                    return texto[0];
                }

                if (real.IsEnum)
                {
                    bool esFlags = real.IsDefined(typeof(FlagsAttribute), false);

                    string mensajeEnum =
                        $"'{texto}' no es un valor válido de {real.Name}. " +
                        $"Valores: {string.Join(", ", Enum.GetNames(real))}.";

                    object resultado;

                    try
                    {
                        resultado = Enum.Parse(real, texto, false);
                    }
                    catch (Exception ex) when (ex is ArgumentException or OverflowException)
                    {
                        throw new FormatException(mensajeEnum, ex);
                    }

                    // Enum.Parse acepta números que no corresponden a ningún valor
                    // (por ejemplo "99"); aquí se rechazan, salvo en enums [Flags].
                    if (!esFlags && !Enum.IsDefined(real, resultado))
                        throw new FormatException(mensajeEnum);

                    return resultado;
                }

                // Tipos que Convert.ChangeType no soporta.
                if (real == typeof(DateTime))
                    return DateTime.Parse(texto, inv, DateTimeStyles.RoundtripKind);
                if (real == typeof(DateTimeOffset))
                    return DateTimeOffset.Parse(texto, inv, DateTimeStyles.RoundtripKind);
                if (real == typeof(DateOnly))
                    return DateOnly.Parse(texto, inv);
                if (real == typeof(TimeOnly))
                    return TimeOnly.Parse(texto, inv);
                if (real == typeof(TimeSpan))
                    return TimeSpan.Parse(texto, inv);
                if (real == typeof(Guid))
                    return Guid.Parse(texto);
                if (real == typeof(bool))
                    return bool.Parse(texto);

                return Convert.ChangeType(texto, real, inv);
            }
            catch (CsvFormatException)
            {
                throw;
            }
            catch (Exception ex) when (
                ex is FormatException or OverflowException or InvalidCastException or ArgumentException)
            {
                throw new CsvFormatException(
                    $"No se pudo convertir '{texto}' a {real.Name}: {ex.Message}",
                    numeroLinea, columna, ex);
            }
        }
    }
}
