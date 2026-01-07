# Payments API

## 📌 Descripción general

Esta solución implementa una **API REST en .NET 8** para el registro y consulta de pagos de servicios básicos (agua, electricidad y telecomunicaciones).

El objetivo de esta prueba técnica es demostrar:
- Buen diseño de API
- Separación de responsabilidades
- Validaciones de negocio
- Persistencia en base de datos relacional
- Uso de buenas prácticas de Clean Code

La arquitectura sigue el flujo:

Controller → Gateway → Service → Base de Datos (SQL Server)

---

## 🧱 Arquitectura del proyecto

La solución está organizada de la siguiente manera:

PaymentsApi
│
├── Controllers        → Endpoints HTTP
├── DTOs               → Contratos de entrada y salida
├── Models             → Entidades del dominio
├── Enums              → Enumeraciones del dominio
├── Gateways           → Reglas de negocio y validaciones
├── Services           → Persistencia (NHibernate)
├── Mappings           → Mapeos NHibernate (ByCode)
├── Infrastructure     → Configuración NHibernate
├── Database
│   └── payments.sql   → Script SQL para crear la tabla
└── README.md

---

## 🔐 Principios aplicados

- Clean Code
- Single Responsibility Principle (SRP)
- Separación de capas
- Validaciones centralizadas
- Controladores delgados
- Persistencia desacoplada del dominio

---

## ⚙️ Requisitos del entorno

- .NET 8 SDK
- Visual Studio 2022 o VS Code
- SQL Server (LocalDB o SQL Server Express)
- No se utiliza Docker en esta versión

---

## 🔌 Configuración de la cadena de conexión

Editar el archivo `appsettings.json` y agregar la sección `ConnectionStrings`.

### Opción recomendada (LocalDB)

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=PaymentsDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}

---

## 🗄️ Creación de la base de datos

```sql
CREATE DATABASE PaymentsDb;
```

---

## 📂 Creación de la tabla Payments

Archivo: `Database/payments.sql`

```sql
CREATE TABLE Payments (
    PaymentId UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    ServiceProvider NVARCHAR(200) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);
```

---

## ▶️ Ejecución del proyecto

1. Restaurar paquetes NuGet
2. Ejecutar el proyecto
3. Abrir Swagger en:
https://localhost:{puerto}/swagger

---

## 📘 Endpoints

### POST /api/payments
Registra un pago con validaciones de negocio.

### GET /api/payments?customerId={GUID}
Obtiene los pagos de un cliente.

---

## 🛡️ Seguridad

- Validaciones automáticas
- Reglas de negocio en Gateway
- Protección contra SQL Injection
- HTTPS habilitado

---

## ✅ Estado de la prueba

✔ Requisitos cumplidos  
✔ Arquitectura limpia  
✔ Persistencia funcional  
✔ Swagger operativo  
