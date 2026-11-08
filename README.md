# Sistema de Gestión de Biblioteca — ACA



**CUN**  
**PROGRAMA DE INGENIERÍA DE SISTEMAS**  
**ASIGNATURA:** Programación Avanzada  
**PROYECTO:** ACA   

**Presentado por:**  
LUIS KALED CORCHO BERMUDEZ

**Docente:**  
VERONICA CASTRO

<div align="center">

[![Estado del Proyecto](https://shields.io)](https://github.com)
[![Versión](https://shields.io)](https://github.com)
[![Licencia](https://shields.io)](https://github.com)

</div>


---

**Sistema de Gestión de Biblioteca**  
Proyecto desarrollado como evidencia del curso de **Programación Avanzada**, aplicando Programación Orientada a Objetos (POO), diseño de bases de datos relacionales en SQL Server, arquitectura en N-Capas (Presentación, Lógica de Negocio y Acceso a Datos), desarrollo en C# con Windows Forms, validación de datos, manejo de excepciones y control de versiones con Git y GitHub.

**Programa:** Ingeniería de Sistemas  
**Grupo:** Programación Avanzada 2026  
**Tecnologías:** C#, .NET Framework / .NET 6, Windows Forms, SQL Server 2022, ADO.NET, Git, GitHub  

---

### TABLA DE CONTENIDO

1. Introducción
2. Objetivos
   - 2.1 Objetivo General
   - 2.2 Objetivos Específicos
3. Planteamiento del Problema
   - 3.1 Descripción del Problema
   - 3.2 Justificación
   - 3.3 Alcance del Sistema
4. Análisis de Requerimientos
   - 4.1 Actores del Sistema
   - 4.2 Requerimientos Funcionales
   - 4.3 Requerimientos No Funcionales
   - 4.4 Reglas de Negocio
5. Casos de Uso
   - 5.1 Descripción General de Casos de Uso
   - 5.2 Especificación de Casos de Uso
6. Diagrama de Clases y Aplicación de POO
   - 6.1 Identificación de Clases y Responsabilidades
   - 6.2 Aplicación de Principios de POO
7. Modelo Entidad-Relación (MER)
   - 7.1 Descripción del Esquema Lógico
   - 7.2 Relaciones y Cardinalidades
8. Diccionario de Datos
   - 8.1 Convenciones y Tipos de Datos
   - 8.2 Especificación de Tablas Relacionales
9. Arquitectura del Sistema
   - 9.1 Descripción General de la Arquitectura por Capas
   - 9.2 Componentes por Capa y Flujo de Información
10. Explicación de los Módulos Desarrollados
    - 10.1 Módulo de Navegación e Inicio
    - 10.2 Módulos CRUD (Libros, Autores, Editoriales, Usuarios)
    - 10.3 Módulo de Préstamos y Devoluciones
    - 10.4 Módulo de Consultas y Reportes
11. Conexión a SQL Server y Script de Base de Datos
    - 11.1 Gestión de Conexión y Consultas Parametrizadas
    - 11.2 Script SQL Completo de Creación e Inserción de Datos
12. Pruebas de Funcionamiento
    - 12.1 Estrategia de Pruebas
    - 12.2 Matriz de Casos de Prueba
13. Conclusiones y Recomendaciones
    - 13.1 Conclusiones
    - 13.2 Recomendaciones
14. Referencias Bibliográficas
15. Anexos
    - Anexo A: Repositorio GitHub y Estructura del Proyecto
    - Anexo B: Estructura del Archivo README.md

---

## 1. Introducción

El desarrollo de software profesional exige la convergencia entre arquitecturas modulares, almacenamiento relacional consistente e interfaces gráficas intuitivas. El presente documento técnico detalla el diseño, arquitectura e implementación del **Sistema de Gestión de Biblioteca**, una aplicación de escritorio desarrollada para optimizar los procesos de administración bibliográfica, registro de usuarios, control de préstamos y procesamiento de devoluciones en instituciones educativas.

La solución ha sido construida utilizando el lenguaje de programación **C#** sobre la plataforma **Windows Forms**, conectada a un motor de base de datos relacional **SQL Server 2022** mediante la tecnología **ADO.NET**. Para garantizar la mantenibilidad, escalabilidad y legibilidad del código, el sistema aplica una **arquitectura por capas** que separa formalmente la interfaz de usuario, la lógica de negocio y las operaciones de acceso a datos, implementando además los pilares de la Programación Orientada a Objetos (POO), manejo estructurado de excepciones y consultas SQL parametrizadas.

---

## 2. Objetivos

### 2.1 Objetivo General

Desarrollar una aplicación de escritorio integral para la gestión de bibliotecas utilizando C#, Windows Forms y SQL Server, aplicando una arquitectura por capas y los principios de la Programación Orientada a Objetos para administrar libros, autores, editoriales, usuarios y préstamos de manera segura y consistente.

### 2.2 Objetivos Específicos

- Diseñar e implementar el modelo de base de datos relacional en SQL Server 2022 con sus respectivas restricciones de clave primaria, clave foránea y unicidad.
- Construir una arquitectura por capas (Presentación, Lógica de Negocio y Acceso a Datos) que desacople la interfaz de usuario de las consultas SQL.
- Implementar formularios interactivos en Windows Forms para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en las entidades principales del sistema.
- Desarrollar la lógica de negocio para el control de inventario de libros, validación de préstamos activos, cálculo de días de entrega y actualización automática de existencias.
- Aplicar patrones de diseño orientados a objetos, encapsulamiento de datos mediante DTOs/Entidades y consultas SQL parametrizadas para prevenir inyecciones SQL.
- Documentar el proyecto y gestionar el código fuente mediante el sistema de control de versiones Git y GitHub.

---

## 3. Planteamiento del Problema

### 3.1 Descripción del Problema

Muchas instituciones educativas y bibliotecas comunitarias enfrentan serias dificultades operativas debido al registro manual o desestructurado de su catálogo bibliográfico y del historial de préstamos. Esta falta de automatización genera diversos inconvenientes:

- Pérdida o extravío de ejemplares por falta de trazabilidad sobre qué usuario posee un determinado libro.
- Inconsistencia en la información del catálogo (libros sin autor o editorial asignados correctamente).
- Errores de digitación e información duplicada en registros de usuarios y documentos de identidad.
- Imposibilidad de conocer en tiempo real la cantidad de existencias disponibles para préstamo.
- Ausencia de reportes consolidados para la toma de decisiones administrativas.

Por tanto, se requiere una solución informática centralizada que automatice las operaciones diarias y garantice la integridad de los datos almacenados.

### 3.2 Justificación

La implementación del Sistema de Gestión de Biblioteca proporciona beneficios directos al personal bibliotecario y a los usuarios. Al digitalizar el control de préstamos y devoluciones, se reduce el tiempo de atención en ventanilla, se elimina la redundancia de datos y se asegura que cada préstamo esté respaldado por un usuario existente y un ejemplar disponible. Desde la perspectiva del desarrollo de software, este proyecto consolida el aprendizaje práctico de la arquitectura por capas, el acceso a datos empresarial con ADO.NET y el uso de entornos IDE profesionales como Visual Studio.

### 3.3 Alcance del Sistema

El siguiente cuadro delimita las funcionalidades incluidas y excluidas en esta versión del proyecto:

| Módulo / Aspecto | Incluido en el Alcance | Fuera del Alcance |
| :--- | :--- | :--- |
| **Gestión Bibliográfica** | CRUD completo de Libros, Autores y Editoriales. | Reserva de libros en línea vía web. |
| **Gestión de Usuarios** | Registro, consulta, edición y eliminación de usuarios. | Autenticación biométrica o tarjetas RFID. |
| **Préstamos y Devoluciones** | Registro de préstamos, cambio de estado a "Devuelto", control de existencias. | Pasarela de pagos en línea para multas. |
| **Plataforma de Ejecución** | Aplicación de escritorio para Windows (Windows Forms). | Aplicación móvil iOS/Android. |
| **Persistencia de Datos** | Base de datos relacional local/red en SQL Server. | Almacenamiento no relacional NoSQL. |

---

## 4. Análisis de Requerimientos

### 4.1 Actores del Sistema

- **Bibliotecario / Administrador:** Usuario encargado de operar la aplicación, gestionar el catálogo bibliográfico, registrar nuevos usuarios, autorizar préstamos y procesar devoluciones.

### 4.2 Requerimientos Funcionales

| Código | Requerimiento | Descripción Detallada |
| :--- | :--- | :--- |
| **RF01** | Gestión de Libros | Permite registrar, listar, editar, eliminar y buscar libros por código/ISBN, título o autor. |
| **RF02** | Gestión de Autores | Permite administrar el catálogo de autores (Nombre, Apellido). |
| **RF03** | Gestión de Editoriales | Permite administrar la información de editoriales asociadas a los libros. |
| **RF04** | Gestión de Usuarios | Permite el registro y actualización de datos de usuarios (Nombre, Apellido, Documento, Teléfono, Correo). |
| **RF05** | Registrar Préstamos | Permite asociar un usuario registrado con un libro disponible, almacenando la fecha de préstamo y cambiando su estado. |
| **RF06** | Registrar Devoluciones | Permite procesar la entrega de un libro prestado, actualizando la fecha real de devolución, el estado del registro y las existencias. |
| **RF07** | Consultas y Reportes | Proporciona listas consolidadas de libros prestados, libros disponibles, usuarios activos e historial de transacciones. |
| **RF08** | Navegación Principal | Formulario contenedor o menú principal para alternar entre los distintos módulos de la aplicación. |

### 4.3 Requerimientos No Funcionales

| Código | Requerimiento | Criterio de Aceptación |
| :--- | :--- | :--- |
| **RNF01** | Usabilidad | Interfaz gráfica intuitiva en Windows Forms con controles estándar (`DataGridView`, `ComboBox`, `TextBox`). |
| **RNF02** | Integridad de Datos | Implementación de claves primarias, claves foráneas y restricciones de unicidad (`UNIQUE`) en SQL Server. |
| **RNF03** | Seguridad en Consultas | Uso exclusivo de `SqlCommand` con parámetros (`SqlParameter`) para evitar inyección de código SQL. |
| **RNF04** | Arquitectura | Separación física o lógica en 3 capas: Presentación, Lógica de Negocio y Acceso a Datos. |
| **RNF05** | Manejo de Excepciones | Captura de errores mediante bloques `try-catch` y presentación de mensajes claros con `MessageBox`. |
| **RNF06** | Mantenibilidad | Código fuente estructurado siguiendo convenciones de nombres en C# y documentado con comentarios XML (`///`). |

### 4.4 Reglas de Negocio

- **RN01 (Unicidad de Documento):** No se permite registrar dos usuarios con el mismo número de documento de identidad.
- **RN02 (Unicidad de ISBN):** Cada libro debe poseer un ISBN o código único en el sistema.
- **RN03 (Campos Obligatorios):** Todos los formularios deben validar que los datos requeridos no estén vacíos antes de guardar.
- **RN04 (Verificación de Existencias):** No se puede registrar un préstamo si el libro seleccionado cuenta con 0 existencias disponibles.
- **RN05 (Control de Stock en Préstamo):** Al registrar un préstamo, la cantidad de existencias del libro debe disminuir en 1 unidad.
- **RN06 (Control de Stock en Devolución):** Al procesar la devolución de un libro, las existencias del ejemplar deben incrementarse en 1 unidad.
- **RN07 (Integridad Referencial):** No se permite eliminar un autor o editorial si existen libros vinculados a dicho registro.
- **RN08 (Estado del Préstamo):** Un préstamo recién creado adopta el estado "Prestado". Al entregarse el libro, el estado pasa a "Devuelto".

---

## 5. Casos de Uso

### 5.1 Descripción General de Casos de Uso

La aplicación centra su interacción en tres casos de uso fundamentales administrados por el Bibliotecario:

- **CU01: Gestionar Libros** (Registrar, listar, editar y eliminar obras en el catálogo).
- **CU02: Registrar Préstamo** (Asignar un ejemplar disponible a un usuario registrado).
- **CU03: Registrar Devolución** (Recibir el ejemplar, actualizar la fecha y restaurar el stock).

### 5.2 Especificación de Casos de Uso

#### CU01: Gestionar Libros (CRUD)

- **Actor Principal:** Bibliotecario / Administrador.
- **Precondiciones:** El usuario tiene acceso a la aplicación y la conexión a SQL Server está activa.
- **Flujo Principal:**
  1. El bibliotecario selecciona la opción "Gestión de Libros" en el menú principal.
  2. El sistema consulta la base de datos y muestra la lista de libros en un `DataGridView`.
  3. El bibliotecario ingresa los datos del libro (ISBN, Título, Autor, Editorial, Categoría, Año, Existencias) y presiona "Guardar".
  4. El sistema valida los datos obligatorios y la unicidad del ISBN.
  5. El sistema inserta el registro mediante la capa DAO y actualiza la lista visual.
- **Flujos Alternativos:**
  - *ISBN Duplicado:* El sistema muestra una alerta de error notificando la restricción de unicidad y cancela la operación.
  - *Campos Vacíos:* El sistema resalta los campos requeridos y detiene el registro.

#### CU02: Registrar Préstamo de Libro

- **Actor Principal:** Bibliotecario.
- **Precondiciones:** El usuario prestamista y el libro existen en la base de datos, y el libro tiene existencias mayores a cero.
- **Flujo Principal:**
  1. El bibliotecario abre el módulo de Préstamos.
  2. Selecciona el usuario mediante su número de documento o lista desplegable.
  3. Selecciona el libro por su ISBN o Título.
  4. El sistema verifica que las existencias del libro sean mayores a 0.
  5. El bibliotecario confirma la operación con la fecha actual.
  6. El sistema guarda el préstamo con estado "Prestado", reduce en 1 las existencias del libro y muestra confirmación.

#### CU03: Registrar Devolución de Libro

- **Actor Principal:** Bibliotecario.
- **Precondiciones:** Existe un registro de préstamo activo con estado "Prestado".
- **Flujo Principal:**
  1. El bibliotecario selecciona el préstamo activo en la lista de préstamos pendientes.
  2. Presiona el botón "Registrar Devolución".
  3. El sistema asigna la fecha de devolución actual y cambia el estado del préstamo a "Devuelto".
  4. El sistema incrementa en 1 unidad el stock de existencias del libro correspondiente en la base de datos.
  5. Muestra un mensaje de operación exitosa y refresca la interfaz.

---

## 6. Diagrama de Clases y Aplicación de POO

### 6.1 Identificación de Clases y Responsabilidades

El diseño orientado a objetos del sistema organiza las entidades de dominio en el namespace `AcaBiblioteca.Entidades`:

| Clase | Atributos Principales | Responsabilidad en el Sistema |
| :--- | :--- | :--- |
| `Libro` | `ISBN`, `Titulo`, `IdAutor`, `IdEditorial`, `Categoria`, `Anio`, `Existencias` | Representa la información bibliográfica de una obra. |
| `Autor` | `IdAutor`, `Nombre`, `Apellido` | Representa al creador o escritor de una obra bibliográfica. |
| `Editorial` | `IdEditorial`, `Nombre` | Representa la empresa o casa editora del libro. |
| `Usuario` | `IdUsuario`, `Nombre`, `Apellido`, `Documento`, `Telefono`, `Correo` | Representa al usuario afiliado que solicita préstamos. |
| `Prestamo` | `IdPrestamo`, `IdUsuario`, `ISBN`, `FechaPrestamo`, `FechaDevolucion`, `Estado` | Modela la transacción de préstamo y seguimiento de devolución. |

### 6.2 Aplicación de Principios de POO

- **Encapsulamiento:** Todas las propiedades de las clases se definen con getters y setters explícitos (`public string Titulo { get; set; }`), protegiendo el estado interno.
- **Constructores:** Cada clase cuenta con un constructor vacío por defecto y constructores sobrecargados con parámetros para facilitar su instanciación limpia desde las capas superiores.
- **Abstracción y Separación de Responsabilidades:** Las clases de entidad solo contienen datos. La lógica de acceso a datos reside exclusivamente en clases especializadas (`LibroDAO`, `PrestamoDAO`).

```csharp
// Ejemplo de Encapsulamiento y Clase Entidad en C#
namespace AcaBiblioteca.Entidades
{
    public class Libro
    {
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public int IdAutor { get; set; }
        public int IdEditorial { get; set; }
        public string Categoria { get; set; }
        public int Anio { get; set; }
        public int Existencias { get; set; }

        public Libro() { }

        public Libro(string isbn, string titulo, int idAutor, int idEditorial, string categoria, int anio, int existencias)
        {
            ISBN = isbn;
            Titulo = titulo;
            IdAutor = idAutor;
            IdEditorial = idEditorial;
            Categoria = categoria;
            Anio = anio;
            Existencias = existencias;
        }
    }
}
```

---

## 7. Modelo Entidad-Relación (MER)

### 7.1 Descripción del Esquema Lógico

El modelo de datos relacional se compone de 5 tablas principales normalizadas hasta la Tercera Forma Normal (3FN), eliminando la redundancia de autores y editoriales y garantizando la consistencia mediante relaciones por clave foránea.

- **Tablas Principales:** `Autores`, `Editoriales`, `Libros`, `Usuarios`, `Prestamos`.

### 7.2 Relaciones y Cardinalidades

- **AUTORES (1) a LIBROS (N):** Un autor puede escribir múltiples libros (`FK_Libros_Autores`).
- **EDITORIALES (1) a LIBROS (N):** Una editorial puede publicar múltiples libros (`FK_Libros_Editoriales`).
- **USUARIOS (1) a PRESTAMOS (N):** Un usuario registrado puede realizar múltiples préstamos en el tiempo (`FK_Prestamos_Usuarios`).
- **LIBROS (1) a PRESTAMOS (N):** Un libro identificado por su ISBN puede ser prestado en múltiples ocasiones (`FK_Prestamos_Libros`).

---

## 8. Diccionario de Datos

### 8.1 Convenciones

- **PK:** Clave Primaria (*Primary Key*).
- **FK:** Clave Foránea (*Foreign Key*).
- **UQ:** Restricción de Valor Único (*Unique Constraint*).
- **IDENTITY(1,1):** Campo autoincremental en SQL Server.

### 8.2 Especificación de Tablas Relacionales

#### Tabla: `Autores`

| Campo | Tipo de Dato | Nulo | Clave | Descripción |
| :--- | :--- | :--- | :--- | :--- |
| `IdAutor` | `INT IDENTITY(1,1)` | No | PK | Identificador único autoincremental del autor. |
| `Nombre` | `VARCHAR(100)` | No | - | Nombre del autor. |
| `Apellido` | `VARCHAR(100)` | No | - | Apellidos del autor. |

#### Tabla: `Editoriales`

| Campo | Tipo de Dato | Nulo | Clave | Descripción |
| :--- | :--- | :--- | :--- | :--- |
| `IdEditorial` | `INT IDENTITY(1,1)` | No | PK | Identificador único autoincremental de la editorial. |
| `Nombre` | `VARCHAR(150)` | No | - | Nombre comercial de la casa editorial. |

#### Tabla: `Libros`

| Campo | Tipo de Dato | Nulo | Clave | Descripción |
| :--- | :--- | :--- | :--- | :--- |
| `ISBN` | `VARCHAR(20)` | No | PK | Código internacional o identificador único del libro. |
| `Titulo` | `VARCHAR(200)` | No | - | Título de la obra bibliográfica. |
| `IdAutor` | `INT` | No | FK | Clave foránea referenciada a `Autores(IdAutor)`. |
| `IdEditorial` | `INT` | No | FK | Clave foránea referenciada a `Editoriales(IdEditorial)`. |
| `Categoria` | `VARCHAR(100)` | Sí | - | Género o categoría literaria. |
| `Anio` | `INT` | Sí | - | Año de publicación del ejemplar. |
| `Existencias` | `INT` | No | - | Cantidad de ejemplares disponibles en inventario. |

#### Tabla: `Usuarios`

| Campo | Tipo de Dato | Nulo | Clave | Descripción |
| :--- | :--- | :--- | :--- | :--- |
| `IdUsuario` | `INT IDENTITY(1,1)` | No | PK | Identificador único autoincremental del usuario. |
| `Nombre` | `VARCHAR(100)` | No | - | Nombres del usuario registrado. |
| `Apellido` | `VARCHAR(100)` | No | - | Apellidos del usuario. |
| `Documento` | `VARCHAR(30)` | No | UQ | Documento de identidad (Único). |
| `Telefono` | `VARCHAR(30)` | Sí | - | Número telefónico de contacto. |
| `Correo` | `VARCHAR(150)` | Sí | - | Correo electrónico institucional o personal. |

#### Tabla: `Prestamos`

| Campo | Tipo de Dato | Nulo | Clave | Descripción |
| :--- | :--- | :--- | :--- | :--- |
| `IdPrestamo` | `INT IDENTITY(1,1)` | No | PK | Identificador del registro de préstamo. |
| `IdUsuario` | `INT` | No | FK | Clave foránea referenciada a `Usuarios(IdUsuario)`. |
| `ISBN` | `VARCHAR(20)` | No | FK | Clave foránea referenciada a `Libros(ISBN)`. |
| `FechaPrestamo` | `DATE` | No | - | Fecha en la que se efectúa el préstamo. |
| `FechaDevolucion` | `DATE` | Sí | - | Fecha real en la que el usuario devuelve el libro. |
| `Estado` | `VARCHAR(20)` | No | - | Estado del registro ('Prestado' / 'Devuelto'). |

---

## 9. Arquitectura del Sistema

### 9.1 Descripción General de la Arquitectura por Capas

La aplicación está diseñada bajo el patrón de **Arquitectura en N-Capas**, asegurando un bajo acoplamiento y una alta cohesión entre componentes:

1. **Capa de Presentación:** Windows Forms (`FrmPrincipal`, `FrmLibros`, `FrmUsuarios`, `FrmPrestamos`).
2. **Capa de Lógica de Negocio:** Controllers / Services (`LibroController`, `PrestamoController`).
3. **Capa de Acceso a Datos:** DAOs y helper de conexión (`ConexionBD`, `LibroDAO`, `PrestamoDAO`).
4. **Capa de Base de Datos:** Motor Relacional SQL Server 2022 (`AcaBiblioteca`).

### 9.2 Componentes por Capa y Flujo de Información

- **Capa de Presentación (`AcaBiblioteca.Presentacion`):** Captura las interacciones del usuario, realiza validaciones visuales iniciales y muestra alertas mediante `MessageBox`.
- **Capa de Lógica de Negocio (`AcaBiblioteca.Negocio`):** Procesa reglas de negocio (ej. verificar que `Existencias > 0` antes de prestar) y coordina las transacciones.
- **Capa de Acceso a Datos (`AcaBiblioteca.Datos`):** Encargada de gestionar la cadena de conexión a SQL Server mediante `SqlConnection`, ejecutar comandos parametrizados con `SqlCommand` y transformar registros en objetos C#.
- **Capa de Base de Datos:** Instancia local de SQL Server 2022 (`AcaBiblioteca`).

---

## 10. Explicación de los Módulos Desarrollados

### 10.1 Módulo de Navegación e Inicio (`FrmPrincipal`)

El formulario principal actúa como contenedor MDI o menú de control centralizado. Dispone de una barra de botones superior o lateral que permite ingresar a los módulos de Libros, Autores, Editoriales, Usuarios, Préstamos y Reportes.

### 10.2 Módulos CRUD (Libros, Autores, Editoriales, Usuarios)

Cada formulario CRUD dispone de los siguientes controles:
- `DataGridView`: Muestra la lista actualizada de registros desde la base de datos.
- `TextBox` / `ComboBox`: Permiten la captura de información con instrucciones claras.
- Botones de Acción: **Nuevo**, **Guardar**, **Editar**, **Eliminar** y **Limpiar**.

### 10.3 Módulo de Préstamos y Devoluciones (`FrmPrestamos`)

- **Proceso de Préstamo:** El bibliotecario selecciona el usuario y el libro deseado de listas desplegables cargadas dinámicamente. Al presionar "Registrar Préstamo", la capa de negocio ejecuta una transacción que inserta la fila en `Prestamos` y decrementa las `Existencias` en `Libros`.
- **Proceso de Devolución:** Muestra una grilla con los préstamos cuyo estado es "Prestado". Al presionar "Devolver", el sistema actualiza `FechaDevolucion = GETDATE()`, cambia el estado a "Devuelto" y suma 1 al stock del libro.

### 10.4 Módulo de Consultas y Reportes (`FrmReportes`)

Proporciona consultas consolidadas como:
- Listado de libros en estado crítico de existencias (`Existencias <= 2`).
- Reporte de préstamos activos con datos del usuario e ISBN del libro.
- Historial completo de devoluciones efectuadas.

---

## 11. Conexión a SQL Server y Script de Base de Datos

### 11.1 Gestión de Conexión y Consultas Parametrizadas

La clase `ConexionBD` centraliza la cadena de conexión hacia SQL Server, utilizando autenticación integrada de Windows:

```csharp
// Clase de Conexión a Base de Datos en C# (ADO.NET)
using System;
using System.Data.SqlClient;

namespace AcaBiblioteca.Datos
{
    public class ConexionBD
    {
        private static readonly string cadenaConexion = 
            "Server=.\\SQLEXPRESS;Database=AcaBiblioteca;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
```

```csharp
// Ejemplo de Consulta Parametrizada en LibroDAO.cs
using System.Data.SqlClient;
using AcaBiblioteca.Entidades;

namespace AcaBiblioteca.Datos
{
    public class LibroDAO
    {
        public bool InsertarLibro(Libro libro)
        {
            string query = "INSERT INTO Libros (ISBN, Titulo, IdAutor, IdEditorial, Categoria, Anio, Existencias) " +
                           "VALUES (@ISBN, @Titulo, @IdAutor, @IdEditorial, @Categoria, @Anio, @Existencias)";

            using (SqlConnection conn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ISBN", libro.ISBN);
                cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@IdAutor", libro.IdAutor);
                cmd.Parameters.AddWithValue("@IdEditorial", libro.IdEditorial);
                cmd.Parameters.AddWithValue("@Categoria", libro.Categoria);
                cmd.Parameters.AddWithValue("@Anio", libro.Anio);
                cmd.Parameters.AddWithValue("@Existencias", libro.Existencias);

                conn.Open();
                int filasAfectadas = cmd.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
    }
}
```

### 11.2 Script SQL Completo (`AcaBiblioteca.sql`)

A continuación se presenta el script ejecutable completo para la creación de la base de datos, tablas, restricciones e inserción de datos iniciales de prueba:

```sql
-- =========================================================
-- SCRIPT DE CREACIÓN DE BASE DE DATOS Y TABLAS - ACA BIBLIOTECA
-- Motor: SQL Server 2022
-- =========================================================

CREATE DATABASE AcaBiblioteca;
GO

USE AcaBiblioteca;
GO

-- 1. Tabla Autores
CREATE TABLE Autores (
    IdAutor INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL
);
GO

-- 2. Tabla Editoriales
CREATE TABLE Editoriales (
    IdEditorial INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(150) NOT NULL
);
GO

-- 3. Tabla Libros
CREATE TABLE Libros (
    ISBN VARCHAR(20) PRIMARY KEY,
    Titulo VARCHAR(200) NOT NULL,
    IdAutor INT NOT NULL,
    IdEditorial INT NOT NULL,
    Categoria VARCHAR(100),
    Anio INT,
    Existencias INT NOT NULL,
    CONSTRAINT FK_Libros_Autores FOREIGN KEY (IdAutor) REFERENCES Autores(IdAutor),
    CONSTRAINT FK_Libros_Editoriales FOREIGN KEY (IdEditorial) REFERENCES Editoriales(IdEditorial)
);
GO

-- 4. Tabla Usuarios
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Documento VARCHAR(30) NOT NULL UNIQUE,
    Telefono VARCHAR(30),
    Correo VARCHAR(150)
);
GO

-- 5. Tabla Prestamos
CREATE TABLE Prestamos (
    IdPrestamo INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    ISBN VARCHAR(20) NOT NULL,
    FechaPrestamo DATE NOT NULL,
    FechaDevolucion DATE NULL,
    Estado VARCHAR(20) NOT NULL,
    CONSTRAINT FK_Prestamos_Usuarios FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario),
    CONSTRAINT FK_Prestamos_Libros FOREIGN KEY (ISBN) REFERENCES Libros(ISBN)
);
GO

-- =========================================================
-- INSERCIÓN DE DATOS DE PRUEBA (DML)
-- =========================================================

INSERT INTO Autores (Nombre, Apellido) VALUES 
('Gabriel', 'García Márquez'),
('Julio', 'Cortázar'),
('Isabel', 'Allende'),
('George', 'Orwell'),
('Miguel', 'de Cervantes'),
('Jane', 'Austen'),
('Mario', 'Vargas Llosa'),
('Jorge Luis', 'Borges'),
('Antoine', 'de Saint-Exupéry'),
('Ernest', 'Hemingway');
GO

INSERT INTO Editoriales (Nombre) VALUES 
('Planeta'),
('Alfaguara'),
('Penguin Random House'),
('Debolsillo'),
('Anagrama'),
('Seix Barral'),
('Tusquets Editores');
GO

INSERT INTO Libros (ISBN, Titulo, IdAutor, IdEditorial, Categoria, Anio, Existencias) VALUES
('9780307474728', 'Cien años de soledad', 1, 1, 'Novela', 1967, 5),
('9780451524935', '1984', 4, 3, 'Distopía', 1949, 4),
('9780060934347', 'Don Quijote de la Mancha', 5, 1, 'Novela', 1605, 3),
('9780141439518', 'Orgullo y prejuicio', 6, 4, 'Novela romántica', 1813, 5),
('9788437604947', 'Rayuela', 2, 2, 'Novela', 1963, 2),
('9788401352836', 'La casa de los espíritus', 3, 1, 'Realismo mágico', 1982, 4),
('9788420471839', 'La ciudad y los perros', 7, 6, 'Novela', 1963, 3),
('9788420633114', 'Ficciones', 8, 7, 'Cuentos', 1944, 2),
('9780156012195', 'El principito', 9, 3, 'Literatura infantil', 1943, 6),
('9780684830490', 'El viejo y el mar', 10, 5, 'Novela', 1952, 3),
('9780307389732', 'El amor en los tiempos del cólera', 1, 1, 'Novela', 1985, 4);
GO

INSERT INTO Usuarios (Nombre, Apellido, Documento, Telefono, Correo) VALUES 
('Ana', 'Pérez', '1001001001', '3001234567', 'ana@gmail.com'),
('Carlos', 'Gómez', '1001001002', '3012345678', 'carlos@gmail.com'),
('María', 'Rodríguez', '1001001003', '3023456789', 'maria@gmail.com'),
('Laura', 'Martínez', '1001001004', '3034567890', 'laura.martinez@gmail.com'),
('Andrés', 'López', '1001001005', '3045678901', 'andres.lopez@gmail.com'),
('Sofía', 'Hernández', '1001001006', '3056789012', 'sofia.hernandez@gmail.com'),
('Daniel', 'Torres', '1001001007', '3067890123', 'daniel.torres@gmail.com'),
('Mariana', 'Ramírez', '1001001008', '3078901234', 'mariana.ramirez@gmail.com'),
('Felipe', 'Castro', '1001001009', '3089012345', 'felipe.castro@gmail.com'),
('Valentina', 'Morales', '1001001010', '3090123456', 'valentina.morales@gmail.com');
GO

INSERT INTO Prestamos (IdUsuario, ISBN, FechaPrestamo, FechaDevolucion, Estado) VALUES 
(1, '9780307474728', GETDATE(), NULL, 'Prestado');
GO
```

---

## 12. Pruebas de Funcionamiento

### 12.1 Estrategia de Pruebas

Se aplicaron pruebas funcionales de caja negra sobre la interfaz gráfica y pruebas de integración sobre la capa de acceso a datos para validar el cumplimiento de las Reglas de Negocio.

### 12.2 Matriz de Casos de Prueba

| ID | Funcionalidad | Entrada / Acción | Resultado Esperado | Resultado Obtenido | Estado |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **CP01** | Registrar Libro | Inserción de libro con datos válidos e ISBN nuevo. | Libro guardado en BD y mostrado en la grilla. | Guardado exitoso y grilla actualizada. | **Aprobado** |
| **CP02** | Registrar Libro | Intentar guardar un libro con ISBN existente. | Error de validación impidiendo la duplicidad (RN02). | Alerta de ISBN duplicado mostrada. | **Aprobado** |
| **CP03** | Registrar Usuario | Registro de usuario con documento existente. | Bloqueo por restricción `UNIQUE` en documento (RN01). | Mensaje de documento registrado previo. | **Aprobado** |
| **CP04** | Registrar Préstamo | Registrar préstamo de libro con `Existencias > 0`. | Préstamo guardado como "Prestado" y stock descontado en 1. | Estado "Prestado" y existencias actualizadas. | **Aprobado** |
| **CP05** | Registrar Préstamo | Intentar prestar un libro con `Existencias = 0`. | Bloqueo por regla de disponibilidad sin ejemplares (RN04). | Mensaje "Sin existencias disponibles". | **Aprobado** |
| **CP06** | Registrar Devolución | Seleccionar préstamo activo y presionar "Devolver". | Estado cambiado a "Devuelto", fecha asignada y stock + 1. | Devolución procesada y stock restaurado. | **Aprobado** |
| **CP07** | Eliminar Autor | Intentar eliminar un autor con libros asignados. | Bloqueo por restricción de clave foránea `FK_Libros_Autores`. | Mensaje de violación de clave foránea. | **Aprobado** |
| **CP08** | Inyección SQL | Ingresar `' OR '1'='1` en caja de búsqueda. | Búsqueda segura mediante `SqlParameter` sin alteración SQL. | Búsqueda tratada como texto literal. | **Aprobado** |

---

## 13. Conclusiones y Recomendaciones

### 13.1 Conclusiones

- **Mantenibilidad mediante Capas:** La separación entre la interfaz en Windows Forms, la lógica de negocio y el acceso a datos mediante ADO.NET permitió construir una aplicación limpia, donde las modificaciones en las consultas SQL no afectan la experiencia visual del usuario.
- **Garantía de Integridad Relacional:** La definición rigurosa de claves primarias, claves foráneas y restricciones de unicidad en SQL Server previene inconsistencias de datos y asegura que no existan préstamos huérfanos.
- **Dominio de la POO:** La implementación de encapsulamiento mediante propiedades C# y la instanciación de clases DTO facilitan el transporte de información entre los formularios y la base de datos.
- **Seguridad en la Persistencia:** La utilización de consultas SQL parametrizadas elimina completamente la vulnerabilidad a ataques de Inyección SQL.

### 13.2 Recomendaciones

- **Impresión de Comprobantes:** Incorporar una librería de generación de documentos para emitir recibos físicos de préstamos y devoluciones.
- **Migración a ORM:** Evaluar en futuras versiones la adopción de Entity Framework Core para simplificar aún más la capa de acceso a datos.
- **Módulo de Mantenimiento:** Implementar respaldos automáticos de la base de datos `AcaBiblioteca` desde la interfaz de la aplicación.

---

## 14. Referencias Bibliográficas

- **Contreras Castañeda, M. Á.** (2016). *Desarrollo de aplicaciones web multiplataforma*. Ministerio de Educación y Formación Profesional de España.
- **Hernández Bejarano, M.** (2020). *Ciclo de vida de desarrollo ágil de software seguro*. Fundación Universitaria Los Libertadores.
- **Mark J. Price.** (2021). *C# 10 and .NET 6 Modern Cross-Platform Development: Build Apps, Websites, and Services with ASP.NET Core 6, Blazor, and EF Core 6 Using Visual Studio 2022 and Visual Studio Code*. Packt Publishing.
- **Microsoft Learn.** (2024). *Documentación oficial de C#, Windows Forms y ADO.NET*. Microsoft Corporation.
- **Vegas Gertrudix, J. M.** (2022). *Java 17 y C#: programación avanzada y desarrollo orientado a objetos*. RA-MA Editorial.

---

## 15. Anexos

### Anexo A: Repositorio GitHub y Estructura del Proyecto

El código fuente completo y el script SQL se organizan en el repositorio oficial bajo la siguiente estructura de proyecto en Visual Studio:

```text
AcaBiblioteca.sln
│
├── AcaBiblioteca.Presentacion/
│   ├── FrmPrincipal.cs
│   ├── FrmLibros.cs
│   ├── FrmUsuarios.cs
│   └── FrmPrestamos.cs
│
├── AcaBiblioteca.Negocio/
│   ├── LibroController.cs
│   └── PrestamoController.cs
│
├── AcaBiblioteca.Datos/
│   ├── ConexionBD.cs
│   ├── LibroDAO.cs
│   └── PrestamoDAO.cs
│
├── AcaBiblioteca.Entidades/
│   ├── Libro.cs
│   ├── Usuario.cs
│   └── Prestamo.cs
│
└── SqlScript/
    └── AcaBiblioteca.sql
```

