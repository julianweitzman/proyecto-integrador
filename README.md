# Juego Guerra

Solución de consola en C# con proyectos destinados a .NET 8.

| Proyecto | Responsabilidad | Referencias de proyecto |
| --- | --- | --- |
| Interfaz | Entrada del jugador y presentación en consola. | Modelos, Logica, Comunicacion, Archivos y Utilidades |
| Modelos | Entidades, estado del juego y operaciones propias de los objetos. | Ninguna |
| Logica | Reglas, rondas, turnos y finalización de partidas. | Modelos |
| Comunicacion | Intercambio y coordinación mediante el archivo compartido. | Modelos |
| Archivos | Carga de personajes y recursos, y persistencia local. | Modelos y DataBases |
| Pruebas | Pruebas automatizadas con xUnit. | Los cinco proyectos del juego |

`DataBases` y `Utilidades` son bibliotecas existentes y también están incluidas en la solución. Los nuevos proyectos contienen únicamente su configuración inicial; todavía no hay reglas implementadas ni casos de prueba.

## Desarrollo

Desde la raíz del repositorio:

```powershell
dotnet build "Proyecto integrador.slnx"
dotnet test "Proyecto integrador.slnx" --no-build
dotnet run --project "Proyecto integrador/Proyecto integrador.interfaz/Proyecto integrador.interfaz.csproj"
```

Para abrir y compilar el formato `.slnx` mediante estos comandos se necesita un SDK compatible, por ejemplo .NET 10. Los proyectos mantienen como destino `net8.0`; para ejecutarlos se necesita el runtime de .NET 8. La primera restauración de Pruebas descarga sus paquetes de NuGet.

Las reglas y formatos pendientes se definirán según `Proyecto Integrador.md` y las especificaciones de TI.

## Verificación de la estructura inicial

La solución compiló en Windows con SDK 10.0.401 y destino .NET 8. Se observaron advertencias NU1900 por la consulta de vulnerabilidades de NuGet y CA1416 en el código existente de `Utilidades`. La ejecución de `dotnet test` quedó bloqueada porque el equipo no tenía instalado el runtime de .NET 8. No hay casos de prueba implementados todavía.
