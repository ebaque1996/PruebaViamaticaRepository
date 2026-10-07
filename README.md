# Proyecto Viamatica - Sistema Web (Fullstack)

Este repositorio contiene la solución completa compuesta por un **Backend en .NET 9** y un **Frontend en Angular**, ambos orquestados mediante **Docker Compose** y conectados a un motor de base de datos **SQL Server**.

---

## ??? Tecnologías Utilizadas

- **Backend:** .NET 9 (Web API, Clean Architecture), Entity Framework Core / SQL Server, Scalar/OpenAPI.
- **Frontend:** Angular, Nginx (Servidor para producción en contenedor).
- **Orquestación y Contenedores:** Docker, Docker Compose.
- **Base de Datos:** SQL Server.

---

## ?? Estructura del Proyecto

/
+-- backend/            # Proyecto C# .NET 9 (API, Application, Infrastructure)
+-- frontend/           # Proyecto Angular
+-- docker-compose.yml  # Archivo de orquestación de Docker
+-- README.md           # Guía principal del proyecto

---

## ?? Requisitos Previos

Antes de comenzar, asegúrate de tener instalado en tu equipo:

1. [Docker Desktop](https://www.docker.com/products/docker-desktop/) (con Docker Engine activo).
2. [Git](https://git-scm.com/).
3. **SQL Server Local** con autenticación mixta habilitada y escucha activa en el puerto `1433` (TCP/IP).

---

## ?? Configuración Previa a la Ejecución

1. **Configurar Base de Datos SQL Server:**
   - Asegúrate de que el usuario `sa` (o tu usuario configurado) tenga permisos de acceso.
   - Habilita el protocolo **TCP/IP** en el puerto `1433` en *SQL Server Configuration Manager*.
   - Agrega una regla de entrada en el Firewall de Windows para permitir el puerto TCP `1433`.

2. **Revisar Cadena de Conexión:**
   Verifica que la cadena de conexión en `docker-compose.yml` coincida con las credenciales de tu SQL Server local:
   `ConnectionStrings__DefaultConnection: "Server=host.docker.internal,1433;Database=ViamaticaDB;User Id=sa;Password=TuPasswordSeguro123!;TrustServerCertificate=True;Encrypt=False;"`

---

## ? Ejecución Rápida con Docker Compose

1. Clona el repositorio:
   git clone https://github.com/tu-usuario/tu-repositorio.git
   cd tu-repositorio

2. Construye y levanta los contenedores:
   docker-compose up --build -d

3. Accede a las aplicaciones desde tu navegador:
   - **Frontend (Angular):** http://localhost:4200
   - **Backend API (Documentación Scalar/OpenAPI):** http://localhost:5000/scalar/v1

4. Para detener la aplicación:
   docker-compose down
   
## ?? Pruebas con Postman

En la carpeta `/postman` encontrarás la colección completa de endpoints y las variables de entorno.
1. Abre Postman.
2. Haz clic en **Import** y selecciona los archivos `.json` de la carpeta `/postman`.