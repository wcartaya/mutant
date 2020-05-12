# Mutant
Proyecto que detecta si un humano es mutante basándose en su secuencia de ADN

[TOC]
## Enunciado:
Magneto quiere reclutar la mayor cantidad de mutantes para poder luchar
contra los X-Men.
Hay que desarrollar un proyecto que detecte si un humano es mutante basándose en su secuencia de ADN.
Para eso hay que crear un programa con un método o función con la siguiente firma :

bool isMutant(String[] dna);

Se recibe como parámetro un array de Strings que representan cada fila de una tabla de (NxN) con la secuencia del ADN. Las letras de los Strings solo pueden ser:  (A,T,C,G), las cuales representa cada base nitrogenada del ADN.

![Imagen de ejemplo](https://github.com/wcartaya/mutant/readme/ejemplo.png)

Un humano es mutante, si se encuentra más de una secuencia de cuatro letras iguales, de forma oblicua, horizontal o vertical.

Ejemplo (Caso mutante):
String[] dna = {"ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"};
En este caso el llamado a la función isMutant(dna) devuelve “true”.

## Requerimientos
Hay que desarrollar el algoritmo de la manera más eficiente posible.

> Opcional 2:
> Crear una API REST, hostear esa API en un cloud computing libre (Google App Engine,
> Amazon AWS, etc), crear el servicio “/mutant/” en donde se pueda detectar si un humano es
> mutante enviando la secuencia de ADN mediante un HTTP POST con un Json el cual tenga el
> siguiente formato:
> POST → /mutant/
> {
> “dna”:["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]
> }
> En caso de verificar un mutante, debería devolver un HTTP 200-OK, en caso contrario un
> 403-Forbidden


> Opcional 3:
> Anexar una base de datos, la cual guarde los ADN’s verificados con la API.
> Solo 1 registro por ADN.
> Exponer un servicio extra “/stats” que devuelva un Json con las estadísticas de las
> verificaciones de ADN: {“count_mutant_dna”:40, “count_human_dna”:100: “ratio”:0.4}
> Tener en cuenta que la API puede recibir fluctuaciones agresivas de tráfico (Entre 100 y 1
> millón de peticiones por segundo).

##Tecnología
- Framework .Net Core 3
- Lenguaje C#
- ORM Entity Framework Core & Dapper
- Base de Datos: SQLAzure
- Control de Versiones: GitHub.
- Documentación de la API: Swagger

##Arquitectura

![[Arquitectura]](https://github.com/wcartaya/mutant/readme/arquitectura.png)

##Desarrollo

### IsMutant
Para evitar recorrer muchas veces la matriz (NxN), se separaron en diferentes métodos la busqueda de cada una de las secuencias posibles: Lineal (hotizontal y vertical) y Oblicua (diagonales positivas y negativas).
Cada posibilidad se corre en una tarea asíncrona de manera paralela y se espera el resultado sólo cuando hace falta.
Ejemplo:
```
Task<int> CountVerticalSecuenceTask = CountVerticalSecuence(dna, index);

Task<int> CountHorizontalSecuenceTask = CountHorizontalSecuence(dna, index);

return await CountVerticalSecuenceTask + await CountHorizontalSecuenceTask;

```

Adicionalmente se trata de salir de cada ciclo lo antes posible: Si se consigue el objetivo o si ya no hay pocibilidad de alcanzar la secuencia (incluso antes de llegar al final del arreglo).
Para buscar las secuencias lineales se recorre la primera fila/columna (un sólo recorrido o indice de 0 hasta N-1).
![[Imagen lineales]](https://github.com/wcartaya/mutant/readme/lineales.png)
Para buscar las secuencias oblicuas se recorre sólo la mitad de la primera fila/columna (un sólo recorrido o indice de 0 hasta N/2).
![Imagen Diagonales](https://github.com/wcartaya/mutant/readme/diagonales.png)

### Stats
Para agilizar la consulta en la base de datos se utiliza el micro ORM Dapper, ya que es muy liviando y rápido. Es cierto que no oferece toda la funcionalidad y robustez que EntityFramework Core pero para aspectos puntuales cómo éste suele ser mas efectivo.

##Rendimiento y Confiabilidad
Se sigue la guía de diseño:    [.Net Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/), que incluye:
[Instrucciones de nomenclatura](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)
[Instrucciones de diseño de tipos](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/type)
[Instrucciones de diseño de miembros](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/member)
[Diseño de extensibilidad](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/designing-for-extensibility)
[Instrucciones de uso](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/usage-guidelines)
[Patrones de diseño comunes](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/common-design-patterns)

Se implementan prácticas recomendadas de rendimiento con ASP.NET Core.

**Usar la versión más reciente del ASP.NET Core:** Cada nueva versión de ASP.NET Core incluye mejoras de rendimiento. ASP.NET Core 3,0 agrega muchas mejoras que reducen el uso de memoria y mejoran el rendimiento. Si el rendimiento es una prioridad, considere la posibilidad de actualizar a la versión actual de ASP.NET Core.

**Minimizar excepciones**: Las excepciones deben ser poco frecuentes. Iniciar y detectar excepciones es lento con respecto a otros patrones de flujo de código. Por este motivo, las excepciones no se deben utilizar para controlar el flujo de programa normal.

**Evitar llamadas de bloqueo:** ASP.NET Core aplicaciones deben diseñarse para procesar muchas solicitudes simultáneamente. Las API asincrónicas permiten que un pequeño grupo de subprocesos controle miles de solicitudes simultáneas sin esperar a que se bloqueen las llamadas. En lugar de esperar a que se complete una tarea sincrónica de ejecución prolongada, el subproceso puede funcionar en otra solicitud.

**Minimizar las asignaciones de objetos grandes:** El recolector de elementos no utilizados de .net Core administra la asignación y liberación de memoria automáticamente en ASP.net Core aplicaciones. La recolección automática de elementos no utilizados normalmente significa que los desarrolladores no tienen que preocuparse de cómo o Cuándo se libera la memoria. No obstante, la limpieza de los objetos sin referencia supone tiempo de CPU, por lo que los desarrolladores deben minimizar la asignación de objetos en rutas de acceso de código activas.
**Optimizar el acceso a datos y la e/s:** Las interacciones con un almacén de datos y otros servicios remotos suelen ser las partes más lentas de una aplicación ASP.NET Core. Leer y escribir datos de forma eficaz es fundamental para lograr un buen rendimiento.


