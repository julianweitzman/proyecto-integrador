PROYECTO INTEGRADOR

# PROYECTO INTEGRADOR

## Juego Guerra

3.º año – Informática

### Presentación

El proyecto integrador Juego Guerra propone desarrollar un juego que combine programación, análisis de sistemas, administración de archivos y diseño visual.

A lo largo del proyecto, trabajarán en las distintas etapas de construcción del juego, aplicando los conocimientos y las herramientas de cada materia. El objetivo es lograr un producto integrado en el que las diferentes partes funcionen de manera coordinada.

### Organización del proyecto

El proyecto se desarrolla de manera articulada entre cuatro materias:

|  |  |
| --- | --- |
|  | Imagen  Diseño visual de los personajes y desarrollo de los recursos gráficos del juego. |
|  | TI — Análisis de sistemas  Definición del problema, análisis de los requerimientos y modelado de la solución. |
|  | Taller de Programación  Diseño e implementación de la lógica del juego mediante POO. |
|  | SSI — Sistemas de información  Administración de archivos de texto y utilización de recursos compartidos en red. |

### ¿Cómo utilizar este documento?

Este documento está organizado en pestañas, una por cada materia. En cada pestaña encontrarán las consignas, las actividades y las indicaciones correspondientes al trabajo que deberán realizar.

Es importante consultar las distintas pestañas, ya que las decisiones y los productos desarrollados en una materia pueden ser necesarios para avanzar en otra.

### Consideraciones generales

El proyecto requiere planificación, trabajo coordinado y coherencia entre las distintas partes. Las decisiones tomadas durante el análisis y el diseño deberán reflejarse en la implementación del juego.

Consulten las consignas de cada materia y tengan en cuenta las indicaciones de sus docentes durante todo el desarrollo del proyecto.

🟡 Imagen

# Proyecto integrador: Juego Guerra

## Imagen — Diseño visual del personaje

### 1. Propósito

En esta materia trabajarán en el diseño visual de un personaje original que formará parte del Juego Guerra.

El personaje ya podrá tener una imagen creada previamente. En ese caso, deberán recuperarla, revisarla y completarla para que cumpla con los requisitos del proyecto.

El objetivo es construir una identidad visual reconocible, coherente con la descripción del personaje y adecuada para su incorporación al juego.

### 2. Actividad 1 — Definición del personaje

Cada alumno deberá definir la identidad de su personaje.

Deberán establecer:

- Nombre: debe identificar al personaje de manera clara.
- Descripción: un texto breve que explique quién es, cuáles son sus características principales y qué lo distingue.
- Concepto visual: las características físicas, los elementos distintivos, la vestimenta, los colores y otros aspectos que permitan reconocerlo.

El personaje podrá pertenecer a un universo imaginario o ser completamente inventado. En todos los casos, deberá tener una identidad propia y una representación visual adecuada.

La descripción deberá ser coherente con la imagen elegida.

Entregable: nombre y descripción del personaje.

### 3. Actividad 2 — Diseño de la imagen

Cada alumno deberá crear o completar una imagen que represente visualmente a su personaje.

Podrá utilizar:

- Un dibujo propio.
- Una herramienta de generación de imágenes.
- Un programa de diseño o ilustración.
- Otra herramienta autorizada por el docente.

Si el personaje ya tiene una imagen, no será necesario crear otra desde cero. Deberán revisar si la imagen representa adecuadamente el concepto definido y si tiene la calidad necesaria para incorporarse al proyecto.

La imagen deberá:

- Permitir identificar al personaje.
- Mantener coherencia con su nombre y descripción.
- Tener una composición clara.
- Poder utilizarse como recurso visual dentro de la documentación del proyecto.

La imagen podrá reflejar las fortalezas o características del personaje. Sin embargo, la asignación de los valores de Fuerza, Velocidad, Resistencia, Inteligencia, Energía y Habilidad se realizará en TI.

Entregable: archivo de imagen del personaje.

### 4. Actividad 3 — Creación del ASCII Art

El juego se ejecutará en una consola, por lo que también será necesario contar con una representación del personaje mediante caracteres de texto.

A partir de la imagen original, deberán crear una versión en ASCII Art que pueda mostrarse en la consola.

Podrán utilizar una herramienta de conversión a ASCII Art y realizar los ajustes necesarios para obtener un resultado adecuado.

La representación deberá:

- Guardar relación con la imagen original.
- Permitir reconocer, en la medida de lo posible, al personaje.
- Poder mostrarse correctamente en una consola.
- Tener dimensiones razonables para no dificultar la visualización del juego.
- Evitar caracteres o formatos que produzcan una representación ilegible.

Deberán comprobar el resultado en una consola o en una vista que reproduzca sus condiciones de visualización.

Entregable: archivo de texto con el ASCII Art, listo para incorporarse al juego.

### 5. Actividad 4 — Preparación de los recursos visuales

Una vez finalizados los diseños, deberán organizar los recursos para que puedan ser utilizados por el resto del equipo.

La entrega deberá incluir:

- Nombre del personaje.
- Descripción breve.
- Imagen original.
- Archivo de texto con el ASCII Art.

Los archivos deberán tener nombres claros y estar organizados de manera que puedan identificarse fácilmente.

El formato definitivo y la ubicación de los archivos que utilizará el programa se acordarán con TI y Taller de Programación. No deberán inventar una estructura de datos propia para el archivo técnico que leerá la aplicación.

### 6. Entrega final de Imagen

Cada alumno deberá entregar la ficha visual de su personaje, con el nombre, la descripción, la imagen y el ASCII Art.

Durante la presentación deberá explicar las decisiones visuales tomadas y justificar cómo los distintos elementos contribuyen a construir la identidad del personaje.

### Objetivo de la actividad

El objetivo es que cada personaje tenga una identidad visual propia y que sus recursos puedan incorporarse al Juego Guerra.

La materia Imagen se ocupa de cómo se ve y cómo se representa el personaje. La definición estratégica de sus atributos corresponde a TI, mientras que la implementación de los recursos dentro del programa corresponde a Taller de Programación.

🟢 TI

# Proyecto integrador: Juego Guerra

## TI — Análisis de Sistemas

### 1. Propósito

En esta materia trabajarán en el análisis y diseño del sistema que permitirá implementar el juego Juego Guerra.

El objetivo es transformar una descripción del problema en una solución documentada, definiendo las reglas del juego, la información que deberá administrar el programa, las responsabilidades de sus componentes y las condiciones necesarias para verificar su correcto funcionamiento.

Algunos estudiantes asumirán el rol de Analistas y tendrán la responsabilidad adicional de coordinar el análisis, elaborar las especificaciones y acompañar a sus compañeros durante el desarrollo en Taller de Programación.

### 2. Actividad 1 — Análisis y diseño de los personajes

Cada alumno deberá analizar las características de su personaje y definir la distribución de puntos que utilizará durante los enfrentamientos.

Todos los personajes competirán bajo las mismas reglas. Por eso, deberán respetarse los siguientes requisitos:

- Cada personaje tendrá los siguientes seis atributos:

|  |  |
| --- | --- |
| Atributo | Descripción |
| Fuerza | Representa la potencia física o capacidad de ataque del personaje. |
| Velocidad | Representa la rapidez de movimiento y reacción. |
| Resistencia | Representa la capacidad para soportar ataques o situaciones adversas. |
| Inteligencia | Representa la capacidad de análisis y estrategia. |
| Energía | Representa el poder o capacidad energética del personaje. |
| Habilidad | Representa la destreza, precisión y dominio de sus capacidades. |

- Todos estos atributos serán utilizados posteriormente durante los combates.

- La suma de los seis atributos deberá ser exactamente de 3.000 puntos. Cada atributo tendrá un valor mínimo de 250 y un máximo de 750 puntos.

Además, se deberán cumplir las siguientes condiciones:

- Al menos un atributo deberá tener 650 puntos o más.
- Al menos un atributo deberá tener 350 puntos o menos.
- No se permitirá asignar 500 puntos a los seis atributos.

El objetivo no es distribuir los puntos al azar, sino tomar una decisión de diseño fundamentada. Aumentar el valor de un atributo implica disponer de menos puntos para los restantes. Cada alumno deberá analizar cómo esa decisión influye en las fortalezas, las debilidades y la estrategia de su personaje.

Para ello deberán:

1. Comprender qué representa cada atributo.
2. Proponer distintas distribuciones posibles.
3. Comparar las alternativas y evaluar sus ventajas y desventajas.
4. Seleccionar una distribución y justificar la decisión.
5. Verificar que la distribución cumpla todas las restricciones.
6. Identificar el atributo principal y el atributo de menor valor.

La imagen y la descripción visual del personaje se trabajarán en Arte. En TI deberán considerar esas características como contexto para fundamentar las decisiones sobre los atributos, sin que la apariencia determine automáticamente los puntajes.

[Entregable:](#h.lrjqegnhor3)[ficha de análisis del personaje con los seis valores, el total, el atributo principal, el atributo de menor valor y una justificación de la estrategia elegida.](#h.lrjqegnhor3)

### 3. Actividad 2 — Relevamiento de requerimientos

Analicen el funcionamiento esperado del juego e identifiquen las necesidades que deberá satisfacer la aplicación.

Deberán documentar:

- El objetivo del juego.
- Los participantes y sus responsabilidades.
- Las reglas de los enfrentamientos.
- La administración de cartas y mazos.
- Las condiciones para ganar una ronda y una partida.
- La información que debe conservarse durante la ejecución.
- Las necesidades de comunicación entre las dos computadoras.
- Las situaciones de error que el sistema debe contemplar.

Distingan los requerimientos funcionales de las condiciones técnicas necesarias para que el sistema pueda ejecutarse.

Entregable: documento de requerimientos y reglas del juego.

### 4. Actividad 3 — Modelo de clases

A partir de los requerimientos, identifiquen las clases necesarias para representar el problema.

Para cada clase deberán determinar:

- Su responsabilidad dentro del sistema.
- Los atributos que necesita.
- Los métodos que debe ofrecer.
- Los parámetros y valores de retorno de cada método.
- Las relaciones con las demás clases.
- La información que debe mantener encapsulada.
- Las operaciones que deben delegarse en otros objetos.

El modelo deberá contemplar, como mínimo, la representación de los personajes, las cartas, los mazos y los elementos necesarios para administrar una partida. La identificación y justificación de las clases adicionales será responsabilidad del equipo de análisis.

Elaboren un diagrama de clases UML que represente la solución propuesta.

Entregable: diagrama de clases UML y justificación de las principales decisiones de diseño.

### 5. Actividad 4 — Especificación de métodos

Para cada método relevante, documenten su comportamiento esperado.

La especificación deberá indicar:

- Nombre y responsabilidad.
- Parámetros de entrada.
- Valor de retorno, si corresponde.
- Precondiciones.
- Resultado esperado.
- Validaciones y situaciones excepcionales.

Utilicen los diagramas de SassI-Schneiderman cuando corresponda representar la lógica de los algoritmos.

Las especificaciones deben ser suficientemente precisas para que un compañero pueda implementar el método sin tener que inventar las reglas del problema.

Entregable: especificaciones de métodos y diagramas correspondientes.

### 6. Actividad 5 — Formato de los archivos de personajes

Cada alumno proporcionará un archivo de texto con los datos de su personaje. El sistema deberá poder interpretar esos datos y utilizarlos para construir un objeto Personaje.

El equipo de análisis deberá definir:

- La estructura del archivo.
- Los datos obligatorios y su formato.
- La representación del nombre y de los seis atributos.
- La forma de incorporar o referenciar la descripción y el ASCII Art, según las decisiones del diseño.
- Las reglas para validar los valores de los atributos.
- El tratamiento de datos faltantes, duplicados o incorrectos.
- El comportamiento esperado cuando no se puede leer un archivo.

El formato deberá ser común para todos los personajes, de modo que el programa pueda procesarlos de manera uniforme.

No alcanza con definir qué datos se necesitan: deben especificar cómo se reconocerá cada dato dentro del archivo.

Entregable: especificación del formato del archivo y ejemplos válidos e inválidos.

### 7. Actividad 6 — Archivo de partida compartida

El juego deberá permitir que dos computadoras participen de una misma partida utilizando un archivo compartido en la red local.

Analicen qué información necesita conocer cada jugador y qué información debe intercambiarse durante la partida.

Definan:

- Qué datos deben almacenarse.
- Cuándo se actualiza el archivo.
- Cuándo debe consultar su contenido cada computadora.
- Cómo se identifica el estado de la partida y el turno correspondiente.
- Cómo se registra la acción de cada jugador.
- Qué ocurre cuando el archivo no está disponible.
- Cómo se evitan inconsistencias cuando dos procesos intentan acceder al recurso.

La solución deberá ser coherente con las posibilidades técnicas trabajadas en SSI y con las operaciones que se implementarán en Taller de Programación.

Tengan presente que el archivo compartido durante la partida cumple una función diferente de los archivos individuales que contienen los datos iniciales de los personajes.

Entregable: especificación del contenido y del funcionamiento del archivo compartido.

### 8. Actividad 7 — Casos de prueba

Diseñen pruebas que permitan verificar que el sistema cumple con los requerimientos.

Incluyan casos normales, situaciones límite y errores posibles, por ejemplo:

- Personaje con todos sus datos válidos.
- Distribución de puntos cuyo total no sea 3.000.
- Atributo fuera del rango permitido.
- Distribución que no cumpla las condiciones de especialización.
- Archivo de personaje incompleto.
- Mazo sin cartas disponibles.
- Empate en un enfrentamiento.
- Jugador que se queda sin cartas.
- Archivo compartido inaccesible.
- Información de una partida que no corresponde al turno esperado.

Para cada caso indiquen las condiciones iniciales, los datos de prueba y el resultado esperado.

Utilicen análisis de caja negra y caja blanca según corresponda.

Entregable: plan de pruebas con resultados esperados.

### 9. Responsabilidades de los Analistas

Los estudiantes designados como Analistas deberán coordinar la elaboración de los entregables, mantener actualizada la documentación y comunicar las decisiones al equipo de Taller de Programación.

También deberán analizar las observaciones de los programadores. Si una especificación es ambigua, incompleta o técnicamente inviable, deberán revisar el diseño y documentar los cambios.

La función de Analista no reemplaza la participación individual en las actividades de aprendizaje y programación. Todos los estudiantes deberán comprender el funcionamiento del sistema y poder explicar las decisiones tomadas.

### 10. Entrega final de TI

La entrega incluirá:

- Las fichas de análisis de los personajes.
- El documento de requerimientos y reglas del juego.
- El diagrama de clases UML.
- Las especificaciones de métodos y los diagramas de algoritmos correspondientes.
- La definición del formato de los archivos de personajes.
- El diseño del archivo de partida compartida.
- El plan de pruebas.

Durante la defensa deberán justificar las decisiones de análisis y diseño, explicar las estrategias de asignación de puntos y demostrar cómo la solución propuesta responde a los requerimientos del juego.

#### Modelo de Ficha de análisis:

Al finalizar esta etapa deberán entregar una ficha con la siguiente información:

Nombre del personaje:

---

Descripción:

---

---

---

Fuerza: __________

Velocidad: __________

Resistencia: __________

Inteligencia: __________

Energía: __________

Habilidad: __________

Total: __________ / 3.000

Atributo principal: __________________

Atributo más bajo: __________________

Imagen del personaje:

⚫ Taller

# Proyecto integrador: Juego Guerra

## Taller de Programación — C#

### 1. Propósito

En Taller deberán implementar en C# el sistema diseñado en TI.

El trabajo consiste en transformar las especificaciones en clases, métodos y funcionalidades que puedan probarse e integrarse hasta obtener una aplicación funcional entre dos computadoras conectadas a una red local.

La implementación deberá respetar las responsabilidades definidas para cada clase y los contratos establecidos por los Analistas.

### 2. Etapa 1 — Implementación de Personaje

A partir del diseño elaborado en TI, implementen la clase que represente a un personaje.

La clase deberá almacenar la información definida en el análisis, incluyendo el nombre, los seis atributos numéricos y la representación ASCII Art.

Deberán implementar el constructor y las operaciones de consulta que se hayan especificado.

Verifiquen que los objetos puedan representar correctamente los personajes creados por los alumnos y que la información se mantenga encapsulada.

Entregable: clase Personaje implementada y pruebas de funcionamiento.

### 3. Etapa 2 — Lectura de archivos de personajes

Implementen el mecanismo para leer los archivos de texto entregados por los alumnos.

El programa deberá interpretar la estructura acordada en TI, validar los datos y construir los objetos correspondientes.

Contemplen los errores previstos en el análisis, como archivos inexistentes, información incompleta o valores inválidos.

La lectura del archivo deberá estar organizada de acuerdo con las responsabilidades definidas en el diseño. Eviten concentrar toda la lógica de lectura, validación y creación de objetos en un único método sin una justificación adecuada.

Entregable: funcionalidad de carga de personajes y pruebas con archivos válidos e inválidos.

### 4. Etapa 3 — Implementación de Carta

Implementen la clase que represente una carta del juego.

La carta deberá utilizar un objeto Personaje para obtener la información que necesita mostrar o utilizar durante un enfrentamiento.

Apliquen composición y delegación cuando corresponda al diseño. Eviten duplicar en Carta los datos que ya pertenecen a Personaje.

Entregable: clase Carta y pruebas de su relación con Personaje.

### 5. Etapa 4 — Administración del mazo

Implementen la clase responsable de administrar las cartas de un mazo.

De acuerdo con las especificaciones de TI, deberá permitir realizar las operaciones necesarias para:

- Incorporar cartas.
- Mezclar las cartas.
- Repartirlas entre los jugadores.
- Obtener la carta que participa de un enfrentamiento.
- Retirar cartas utilizadas.
- Incorporar las cartas ganadas.
- Consultar la cantidad de cartas disponibles.

Las operaciones deberán contemplar las situaciones límite previstas en el análisis.

Entregable: clase de administración del mazo y pruebas de sus operaciones.

### 6. Etapa 5 — Implementación de las reglas

Implementen las funcionalidades necesarias para desarrollar una partida.

El sistema deberá permitir seleccionar el atributo con el que se enfrentan las cartas, comparar los valores, determinar al ganador de la ronda, administrar las cartas involucradas y establecer quién tiene el siguiente turno.

La aplicación deberá reconocer las condiciones de finalización y comunicar el resultado de la partida.

Entregable: funcionalidades de juego integradas y pruebas de los distintos escenarios.

### 7. Etapa 6 — Comunicación entre computadoras

Implementen el mecanismo de intercambio de información definido en TI, utilizando el archivo de texto compartido configurado en SSI.

El programa deberá poder consultar y actualizar el estado de la partida de acuerdo con las reglas de intercambio acordadas.

Contemplen los errores de acceso y las situaciones de concurrencia previstas en el diseño. La implementación no deberá asumir que el archivo siempre está disponible o que una operación de escritura necesariamente termina correctamente.

Entregable: comunicación implementada y pruebas realizadas con dos computadoras.

### 8. Etapa 7 — Integración

Integren las clases y funcionalidades desarrolladas por los distintos integrantes del equipo.

Verifiquen que los componentes respeten las interfaces y responsabilidades definidas. Si una especificación presenta un problema, comuníquenlo a los Analistas antes de modificar unilateralmente el diseño.

La aplicación deberá poder iniciarse en ambas computadoras y completar una partida utilizando los personajes entregados por los alumnos.

Entregable: aplicación integrada y código fuente organizado.

### 9. Evaluación

Se evaluará la correcta implementación de las especificaciones, el uso de programación orientada a objetos, el encapsulamiento, la composición, la delegación, la modularización, la validación de datos, la calidad de las pruebas y la integración del sistema.

Durante la defensa, cada estudiante deberá explicar las clases y los métodos en los que trabajó, justificar las decisiones de implementación y demostrar que comprende cómo se relacionan los componentes.

🟣 SSI

# Proyecto integrador: Juego Guerra

## SSI — Sistemas y recursos compartidos

### 1. Propósito

En SSI deberán preparar y verificar el entorno tecnológico necesario para que el juego pueda utilizar archivos de texto y compartir información entre dos computadoras conectadas a una red local.

El objetivo es comprender cómo interviene el sistema operativo en el almacenamiento de datos, la administración de archivos, los permisos y el acceso a recursos compartidos.

### 2. Actividad 1 — Organización de archivos

Analicen los archivos que utilizará el proyecto y distingan sus finalidades.

El sistema tendrá archivos de texto proporcionados por los alumnos con los datos de sus personajes y utilizará un archivo compartido para intercambiar información durante una partida.

Deberán reconocer:

- La ubicación de los archivos.
- Sus nombres y extensiones.
- La diferencia entre rutas absolutas y relativas.
- La diferencia entre un archivo local y uno ubicado en un recurso compartido.
- La importancia de mantener una organización consistente de carpetas y archivos.

Entregable: esquema de organización de archivos y carpetas.

### 3. Actividad 2 — Archivos de texto

Trabajen con archivos de texto y analicen cómo se guarda y recupera información mediante un editor y las herramientas del sistema operativo.

Utilicen archivos de prueba para observar cómo se representan los datos, qué ocurre al modificar su contenido y por qué es importante respetar un formato acordado.

Relacionen esta actividad con los archivos de personajes que se incorporarán al juego.

Entregable: archivos de prueba y explicación de su estructura y ubicación.

### 4. Actividad 3 — Compartir una carpeta

Configuren una carpeta compartida en una computadora y accedan a ella desde una segunda computadora conectada a la red local.

Deberán:

- Identificar las computadoras involucradas.
- Configurar el recurso compartido.
- Acceder mediante la ruta de red correspondiente.
- Verificar que se pueda leer el contenido.
- Probar la creación o modificación de archivos cuando los permisos lo permitan.
- Identificar y resolver problemas básicos de acceso.

Documenten la configuración necesaria para reproducir el procedimiento.

Entregable: carpeta compartida operativa e instrucciones de configuración.

### 5. Actividad 4 — Usuarios y permisos

Analicen qué permisos son necesarios para que el programa pueda leer y actualizar el archivo compartido.

Distingan los permisos de acceso al recurso compartido de los permisos que administra el sistema de archivos.

Prueben qué sucede cuando el usuario no tiene permisos suficientes y documenten el comportamiento observado.

Entregable: registro de las pruebas de acceso y de los permisos necesarios.

### 6. Actividad 5 — Disponibilidad y acceso simultáneo

Analicen situaciones problemáticas que pueden presentarse al utilizar un archivo compartido.

Por ejemplo:

- La computadora que comparte el recurso está apagada.
- Se pierde la conexión de red.
- El archivo no existe.
- El usuario no tiene permiso para modificarlo.
- Dos procesos intentan acceder al archivo en momentos cercanos.
- Un proceso intenta leer mientras el otro está escribiendo.

Expliquen qué consecuencias podrían tener estas situaciones para el juego y qué medidas deberían contemplarse desde el diseño y la programación.

Entregable: informe breve de riesgos y medidas preventivas.

### 7. Actividad 6 — Prueba del entorno del juego

Una vez integrada la aplicación, configuren el entorno definitivo y verifiquen que ambas computadoras puedan acceder al recurso compartido.

Comprueben que el archivo utilizado por la partida se encuentre en la ubicación acordada y que las cuentas utilizadas tengan los permisos necesarios.

Realicen pruebas de acceso y registren los problemas detectados, diferenciando los errores de configuración de los posibles errores del programa.

Entregable: registro de configuración y pruebas realizadas.

### 8. Evaluación y defensa

Se evaluará la comprensión de archivos, rutas, permisos y recursos compartidos; la correcta configuración del entorno; la capacidad para diagnosticar problemas de acceso y la documentación de los procedimientos realizados.

Durante la defensa, deberán explicar cómo se comparte una carpeta entre dos computadoras, cómo accede el programa al recurso y qué limitaciones tiene el uso de archivos compartidos para coordinar una partida.
