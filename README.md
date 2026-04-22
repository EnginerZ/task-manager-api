# TaskManagerAPI

REST API desarrollada en ASP.NET Core para la gestión de tareas por usuario, con autenticación JWT y arquitectura en capas.

---

## Tecnologías utilizadas

- ASP.NET Core 8
- Entity Framework Core
- SQL Server Express
- JWT (JSON Web Tokens)
- BCrypt.Net (hash de contraseñas)
- Swagger / OpenAPI

---

## Arquitectura

El proyecto sigue el patrón de separación de responsabilidades:

- **Controllers** → Reciben peticiones HTTP y devuelven respuestas
- **Services** → Contienen toda la lógica de negocio
- **Models** → Representan las entidades de la base de datos
- **DTOs** → Controlan los datos que entran y salen de la API
- **Data** → Contexto de Entity Framework

---

## Estructura del proyecto

TaskManagerAPI/
├── Controllers/
│   ├── TasksController.cs
│   └── UsersController.cs
├── Services/
│   ├── ITaskService.cs
│   ├── TaskService.cs
│   ├── IUserService.cs
│   └── UserService.cs
├── Models/
│   ├── TaskItem.cs
│   └── User.cs
├── DTOs/
│   ├── TaskDTO.cs
│   ├── TaskResponseDTO.cs
│   └── UserDTO.cs
├── Data/
│   └── AppDbContext.cs
├── Migrations/
├── appsettings.json
└── Program.cs

---

## Requisitos previos

- .NET 8 SDK
- SQL Server Express
- Visual Studio 2022 o VS Code

---

## Configuración

1. Clona el repositorio:
```bash
git clone https://github.com/TU_USUARIO/TaskManagerAPI.git
cd TaskManagerAPI
```

2. Configura la cadena de conexión en `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=TaskManagerDB;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "TU_CLAVE_SECRETA_AQUI"
  }
}
```

3. Aplica las migraciones:
```bash
dotnet ef database update
```

4. Ejecuta el proyecto:
```bash
dotnet run
```

5. Abre Swagger en: https://localhost:{PORT}/swagger

---

## Endpoints

### Usuarios

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| POST | /api/users | Registrar usuario | ❌ |
| POST | /api/users/login | Iniciar sesión | ❌ |
| GET | /api/users | Obtener todos los usuarios | ❌ |

### Tareas

| Método | Ruta | Descripción | Auth |
|--------|------|-------------|------|
| POST | /api/tasks | Crear tarea | ✅ |
| GET | /api/tasks | Obtener todas las tareas | ✅ |
| PUT | /api/tasks/{id} | Actualizar tarea | ✅ |
| DELETE | /api/tasks/{id} | Eliminar tarea | ✅ |

---

## Autenticación

La API usa JWT. Para acceder a endpoints protegidos:

1. Regístrate en `POST /api/users`
2. Inicia sesión en `POST /api/users/login`
3. Copia el token recibido
4. En Swagger haz click en **Authorize** e ingresa: Bearer TU_TOKEN_AQUI

---

## Historial de cambios

### v1.1.0
- Migración de lógica de negocio a capa de Services
- Creación de interfaces `ITaskService` e `IUserService`
- Clave JWT movida de código a `appsettings.json`
- Eliminado `UseAuthorization()` duplicado en `Program.cs`

### v1.0.0
- Implementación inicial de CRUD de tareas
- Autenticación con JWT
- Hash de contraseñas con BCrypt
- Documentación con Swagger

---

---
## 👨‍💻 Autor

Proyecto desarrollado como práctica de backend con .NET enfocado en buenas prácticas y seguridad.
---

