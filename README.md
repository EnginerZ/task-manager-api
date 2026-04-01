# 🧠 Task Manager API - .NET

API REST desarrollada en **.NET** para la gestión de usuarios y tareas, con autenticación segura mediante **JWT**. Permite registrar usuarios, iniciar sesión y administrar tareas de forma protegida.

---

## 🚀 Características

* 🔐 Autenticación con JWT
* 🔒 Hash de contraseñas con BCrypt
* 👤 Registro y login de usuarios
* 📋 CRUD completo de tareas
* 🛡 Protección de endpoints con `[Authorize]`
* ✅ Validaciones de datos
* 🧼 Uso de DTOs (entrada/salida limpia)
* 🗄️ Persistencia con Entity Framework + SQL Server

---

## 🛠️ Tecnologías utilizadas

* .NET
* Entity Framework Core
* SQL Server
* JWT (JSON Web Tokens)
* BCrypt.Net

---

## 📦 Estructura del proyecto

```
Controllers/
├── UserController.cs
├── TaskController.cs

Models/
├── User.cs
├── TaskItem.cs

DTOs/
├── TaskDTO.cs
├── TaskResponseDTO.cs
├── UserDTO.cs

Data/
├── AppDbContext.cs
```

---

## ⚙️ Configuración

### 🔧 1. Clonar repositorio

```bash
git clone https://github.com/TU_USUARIO/TU_REPO.git
cd TU_REPO
```

---

### 🔧 2. Configurar conexión a base de datos

Editar `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=TaskDb;Trusted_Connection=True;"
}
```

---

### 🔧 3. Aplicar migraciones

```bash
dotnet ef database update
```

---

### 🔧 4. Ejecutar proyecto

```bash
dotnet run
```

---

### 🔧 5. Acceder a Swagger

```
https://localhost:XXXX/swagger
```

---

## 🔐 Autenticación

1. Registrar usuario
2. Iniciar sesión → obtener token
3. Usar token en Swagger:

```
Bearer TU_TOKEN
```

---

## 📌 Endpoints principales

### 👤 Usuarios

* `POST /api/user` → Registrar usuario
* `GET /api/user` → Mostrar
* `POST /api/user/login` → Login

---

### 📋 Tareas (requiere autenticación)

* `GET /api/task` → Obtener tareas
* `POST /api/task` → Crear tarea
* `PUT /api/task/{id}` → Actualizar tarea
* `DELETE /api/task/{id}` → Eliminar tarea

---

## 🧪 Ejemplos

### Registro

```json
{
  "email": "test@mail.com",
  "password": "123456"
}
```

---

### Login

```json
{
  "email": "test@mail.com",
  "password": "123456"
}
```

---

### Crear tarea

```json
{
  "title": "Aprender .NET",
  "description": "Construir API",
  "status": "Pendiente"
}
```

---

## 🧠 Flujo de la aplicación

1. Usuario se registra (password hasheado)
2. Usuario inicia sesión → recibe JWT
3. Token se usa para acceder a endpoints protegidos
4. Las tareas se asocian al usuario autenticado

---

## 🔒 Seguridad

* Contraseñas almacenadas con hash (BCrypt)
* Autenticación mediante JWT
* Endpoints protegidos con `[Authorize]`
* Validación de datos en backend

---

## 👨‍💻 Autor

Proyecto desarrollado como práctica de backend con .NET enfocado en buenas prácticas y seguridad.

