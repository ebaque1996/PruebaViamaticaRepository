# Viamatica Backend API (.NET 9)

API RESTful desarrollada en .NET 9 bajo los principios de Clean Architecture para la gestión de usuarios y roles del sistema Viamatica.

---

## 🛠️ Tecnologías y Características

- **Framework:** .NET 9 Web API
- **Arquitectura:** Clean Architecture (Api, Application, Infrastructure, Domain)
- **Base de Datos:** SQL Server (Entity Framework Core)
- **Seguridad:** Hashing de contraseñas con BCrypt, Control de Roles y Estados (`ACT`, `PEN`, `INA`, `BLO`)
- **Documentación API:** Scalar / OpenAPI (`http://localhost:5000/scalar/v1`)

---

## 🛠️ Requisitos Previos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Instancia activa de [SQL Server](https://www.microsoft.com/sql-server/)

---

## ⚙️ Configuración Local (Sin Docker)

1. Configura la cadena de conexión en el archivo `Viamatica.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=ViamaticaDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

2. Aplicar migraciones / actualizar la base de datos:

```bash
dotnet ef database update --project Viamatica.Infrastructure --startup-project Viamatica.Api
```

---

## 🚀 Ejecución en Desarrollo

1. Restaurar dependencias:

```bash
dotnet restore
```

2. Ejecutar la API:

```bash
dotnet run --project Viamatica.Api
```

3. Abrir la documentación interactiva en el navegador:
   - **Scalar:** `http://localhost:5000/scalar/v1` o `http://localhost:5200/scalar/v1`

---

## 🐳 Docker (Standalone)

Si deseas construir y ejecutar únicamente el contenedor del backend:

```bash
docker build -t viamatica-backend -f Dockerfile .
docker run -d -p 5000:8080 --name backend-api viamatica-backend
```