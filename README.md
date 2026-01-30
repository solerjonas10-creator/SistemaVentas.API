Sistema de Ventas API
Este proyecto es una solución de backend desarrollada en C# / .NET para la gestión de ventas, clientes y productos.

🚀 Características y Requerimientos
La API implementa un sistema robusto basado en los siguientes pilares técnicos:

CRUD Completo: Gestión de Productos, Clientes y Ventas.

Persistencia de Datos: Uso de Entity Framework Core conectado a una base de datos Oracle Express 21C.

Validación Avanzada: Implementación de reglas de negocio mediante Fluent Validation.

Seguridad: Endpoints protegidos mediante JWT (JSON Web Tokens).

Observabilidad: Registro de eventos (logging) de cada petición utilizando Serilog/Seq.

Documentación: Interfaz interactiva de Swagger/OpenAPI para exploración de endpoints.

Arquitectura: Manejo global de excepciones mediante Middleware personalizado.

🛠️ Stack Tecnológico
Lenguaje: C# (.NET 6/8)
ORM: Entity Framework Core 
Base de Datos: Oracle Express 21C 
Validación: Fluent Validation 
Logging: Serilog / Seq 
Documentación: Swagger 

📋 Configuración del Proyecto
Requisitos Previos
Tener instalado Oracle Express 21C.
Configurar la estructura de tablas siguiendo los Models creados
Instalacion de paquetes Nuget Incluidos en .csproj
(Opcional) Instalar Seq para la visualización de logs en tiempo real.
(Opcional) Instalar Postman o similares para pruebas de methods

Las capturas de pantalla de las pruebas realizadas en Postman se encuentran en la carpeta /ScreenShots.
Se validaron los flujos de creación, lectura, actualización y borrado para cada entidad.

Los Ids de las tablas USUARIOS, VENTAS Y DETALLE_VENTAS en la base de datos se manejan por Sequences autoincrementables,
las demas tablas tienen Ids manuales que debe proporcionar el usuario.

Desarrollado por:
Jonas Soler
