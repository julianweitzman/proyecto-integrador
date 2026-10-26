# Repository Guidelines

## Estructura y organización

Este repositorio inicia el proyecto educativo «Juego Guerra» en C#.

- `Proyecto integrador/Proyecto integrador.interfaz/`: aplicación de consola; `Program.cs` contiene el punto de entrada. Su destino es `net8.0`.
- `Proyecto integrador/Librerias/DataBases/`: persistencia CSV, modelos, tablas y relaciones; destino `net8.0`.
- `Proyecto integrador/Librerias/Utilidades/`: entrada validada, menús y presentación de consola; destino `net8.0`.
- `Proyecto integrador/Librerias/Documentacion/`: documentación y ejemplos de las bibliotecas.
- `Proyecto Integrador.md`: consignas y requisitos del juego.

Los proyectos `Proyecto integrador.modelos`, `Proyecto integrador.logica`, `Proyecto integrador.comunicacion` y `Proyecto integrador.archivos` separan las responsabilidades del juego. `Proyecto integrador.pruebas` contiene la configuración de xUnit y referencia los cinco proyectos del juego. Consulta `README.md` para conocer sus dependencias. No edites archivos generados en `bin/` u `obj/`.

## Compilación y desarrollo

Instala el SDK de .NET 8 para compilar los proyectos `net8.0`. Ejecuta desde la raíz:

```powershell
dotnet build "Proyecto integrador/Proyecto integrador.interfaz/Proyecto integrador.interfaz.csproj"
dotnet run --project "Proyecto integrador/Proyecto integrador.interfaz/Proyecto integrador.interfaz.csproj"
dotnet build "Proyecto integrador/Librerias/DataBases/DataBases.csproj"
dotnet build "Proyecto integrador/Librerias/Utilidades/Utilidades.csproj"
```

Los comandos compilan cada componente y ejecutan la consola. La solución incluye los ocho proyectos y sus rutas están corregidas. Con un SDK compatible con `.slnx`, como .NET 10, puedes compilar todo con `dotnet build "Proyecto integrador.slnx"` y ejecutar las pruebas con `dotnet test "Proyecto integrador.slnx" --no-build`. Los proyectos mantienen el destino `net8.0`.

## Estilo y nombres

Usa cuatro espacios y llaves en líneas separadas, siguiendo los archivos existentes. Emplea `PascalCase` para clases y métodos, y `camelCase` para parámetros y campos privados. Conserva los nombres en español y los espacios de nombres existentes. Mantén las referencias anulables habilitadas y valida entradas. No hay configuración de formato o lint compartida; evita reformatear archivos ajenos al cambio.

## Verificación

El proyecto Pruebas utiliza xUnit; todavía no contiene casos de prueba ni hay un umbral de cobertura configurado. Compila los proyectos afectados y documenta la comprobación manual. Para cambios en CSV, comprueba lectura, escritura, datos inválidos y valores nulos con archivos temporales. Si incorporas pruebas, usa nombres descriptivos como `Metodo_Escenario_ResultadoEsperado` y documenta cómo ejecutarlas.

## Commits y solicitudes de cambios

El historial contiene `Initial commit` y `Primeros archivos para la ia`; no establece una convención formal. Escribe mensajes breves que describan el cambio. Cada PR debe indicar propósito, componentes afectados y verificaciones realizadas; enlaza la incidencia si existe. Adjunta capturas cuando cambie la presentación de consola y actualiza la documentación si modificas APIs públicas.

## Instrucciones para agentes

Responde siempre al usuario en español.

## Requisitos de Juego Guerra

Lee `Proyecto Integrador.md` antes de diseñar o implementar funcionalidades. Es la fuente de las consignas y distribuye el trabajo entre Imagen, TI, Taller de Programación y SSI.

- El juego se ejecuta en consola y debe permitir completar una partida entre dos computadoras de una red local mediante un archivo de texto compartido.
- Cada personaje tiene nombre, seis atributos (Fuerza, Velocidad, Resistencia, Inteligencia, Energía y Habilidad) y ASCII Art. Sus recursos visuales incluyen descripción e imagen original.
- Los atributos deben sumar exactamente 3.000 puntos. Cada valor debe estar entre 250 y 750, inclusive; al menos uno debe ser mayor o igual a 650 y otro menor o igual a 350. No se permite asignar 500 a los seis atributos.
- `Personaje` encapsula sus datos. `Carta` utiliza un `Personaje` por composición y delegación, evitando duplicar sus datos. El mazo administra incorporación, mezcla, reparto, extracción, retiro, cartas ganadas y cantidad disponible.
- La partida debe administrar selección de atributo, comparación, ganador de ronda, cartas involucradas, siguiente turno y finalización.
- TI debe definir reglas concretas, empates, UML, contratos de métodos, formatos de archivos de personajes y protocolo del archivo compartido. No presentes decisiones todavía pendientes como requisitos ya acordados; documenta las propuestas y respeta las especificaciones existentes.
- Distingue archivos iniciales de personajes del archivo de estado de partida. Contempla datos faltantes, duplicados o inválidos, mazos vacíos, turnos inesperados, permisos insuficientes, pérdida de conexión y acceso simultáneo.
- Imagen entrega nombre, descripción, imagen y ASCII Art legible en consola. SSI documenta organización de archivos, rutas locales y de red, carpeta compartida, permisos y pruebas entre ambas computadoras.

## Reutilización obligatoria de las bibliotecas

Antes de implementar una funcionalidad, revisa el código de `Proyecto integrador/Librerias/` y los tres documentos de `Librerias/Documentacion/`. **Usa las funcionalidades existentes siempre que cubran la necesidad. No crees implementaciones paralelas de entrada validada, menús, formato de consola, persistencia CSV o relaciones que estas bibliotecas ya resuelven.** Agrega referencias de proyecto cuando sean necesarias para utilizarlas.

Si falta una capacidad, identifica y documenta la limitación concreta. Implementa únicamente lo que falta, integrándolo con las bibliotecas; evita reemplazarlas o copiar su código. Antes de terminar, verifica que cada funcionalidad nueva aproveche las APIs existentes que correspondan.

### DataBases: entidades, tablas y persistencia

- Cada entidad implementa `IEntidad<T>` y su método estático `Configurar(ConstructorEntidad<T>)`. Declara `.Archivo(...)`, `.Columna(...)` y relaciones mediante lambdas de acceso directo, incluso a miembros privados.
- Construye el modelo con `new ConstructorModelo().Agregar<Entidad>().Construir()`. La construcción valida columnas, tipos, constructores y relaciones. Debe existir un constructor con un parámetro por columna, cuyos nombres coincidan con los miembros sin distinguir mayúsculas y cuyos tipos sean idénticos.
- Usa `Modelo.Cargar<T>()`, `CargarTodo()`, `Tabla<T>()`, `Guardar<T>()` y `GuardarTodo()`. Cargar un archivo inexistente devuelve una tabla vacía; `Cargar<T>(exigirArchivo: true)` exige su existencia. Guardar reescribe la tabla completa.
- `Tabla<T>` admite `Add`, `Remove`, índices, `Count`, `AgregarRango` y LINQ; rechaza elementos nulos.
- Configura relaciones con `.Uno(...)` o `.Muchos(...)`, `.Con(...)`, `.ApuntaA(...)` y, si corresponde, `.Por<TTabla>(...)`. Resuélvelas desde propiedades de navegación con `Modelo.Uno<T>(this)` y `Modelo.Muchos<T>(this)`.
- `ListaRelacionada<T>` es una copia del resultado de consulta. Para modificar relaciones configura `.AlAgregar(...)` y `.AlQuitar(...)`; el programador actualiza claves foráneas y tablas intermedias. Agregar a una relación no incorpora automáticamente un objeto nuevo a su tabla.
- `CsvManager` es interno: utiliza su funcionalidad a través de `Modelo`. Admite texto, caracteres, booleanos, números, fechas, horas, `Guid`, enums y tipos anulables compatibles.
- El formato es propio: coma como separador, encabezado obligatorio, cultura invariante, escapes con barra inversa y un registro por línea. Distingue `null`, texto vacío y el texto `"null"`. No asumas compatibilidad directa con Excel ni con CSV estándar.
- Distingue `CsvConfiguracionException` de `CsvFormatException` (con línea y columna). Maneja también errores de entrada/salida y permisos.
- La escritura usa archivo temporal y reemplazo, pero el `lock` solo coordina hilos del mismo proceso. **No proporciona coordinación entre computadoras.** El protocolo de turnos y concurrencia de red debe implementarse explícitamente. Tampoco hay transacciones entre tablas ni seguridad entre hilos en `Modelo` y `Tabla<T>`.

### Utilidades: interacción y presentación de consola

- Usa `Funciones.PedirString`, `PedirBool`, `PedirInt`, `PedirDouble`, `PedirChar`, los métodos de fechas y horas, y `LeerClave` para las entradas correspondientes. `PedirBool` acepta `y/n`; los números validan rangos y `PedirDouble` rechaza valores no finitos.
- Usa `PedirOpcionMenu`, `PedirOpcionMultipleMenu`, `PedirEnum<T>` y `PedirEnumFlags<T>` para seleccionar opciones. Los índices empiezan en cero. Los menús admiten flechas, confirmación con Enter y selección múltiple con Espacio; con entrada o salida redirigida usan opciones numeradas.
- Usa `DecoracionConsola` para mensajes de éxito, error, advertencia e información, marcos, separadores, listas, tablas y progreso. Conserva los nombres reales de las APIs, incluido `ImprimirMesajeDeError`.
- Usa `MyConsole` para escribir y leer cuando el contenido deba conservarse al redibujar menús. Registra texto, colores y posiciones; escribir directamente con `Console` omite ese historial. No imprimas desde otros hilos mientras un menú esté activo.
- Usa `Funciones.WaitAndClean()` al cambiar de pantalla: espera una tecla cuando corresponde, limpia la pantalla y borra el historial. `MyConsole.ReinicializarRegistros()` permite borrar solo el historial.
- Maneja `EndOfStreamException` para finalizar ordenadamente si se cierra la entrada. Usa `LeerClave` en lugar del alias obsoleto `LeerNumeroClave`; desactiva el historial durante la lectura de la contraseña.
- Considera las limitaciones de coordenadas absolutas al redimensionar o desplazar la consola y las posibles desalineaciones por emojis o caracteres anchos.

## Validación de las funcionalidades del juego

Comprueba personajes válidos, sumas incorrectas, límites de atributos, especialización, archivos incompletos, mazos vacíos, empates y jugadores sin cartas. Para el intercambio compartido, comprueba archivo inaccesible, permisos, turno incorrecto y concurrencia; la integración final requiere pruebas con dos computadoras.

La documentación de DataBases informa pruebas previas parciales en Linux; la de Utilidades indica que su autor no la compiló ni ejecutó. No presentes esos antecedentes como pruebas realizadas en esta sesión ni como validación del entorno Windows o de red.
