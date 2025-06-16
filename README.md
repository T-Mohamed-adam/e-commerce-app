# 🛒 E-Commerce API (ASP.NET Core)

This is a feature-rich **E-Commerce API** developed using **ASP.NET Core**. It includes modern architectural patterns and real-world features such as user authentication, product management, background services for automated tasks, and integration with third-party services using `HttpClient`.

---

## 🧰 Tech Stack

- **Backend Framework**: ASP.NET Core (.NET 7/8)
- **Database**: SQL Server / PostgreSQL / SQLite
- **ORM**: Entity Framework Core
- **Authentication**: JWT Bearer Tokens
- **Email Service**: SMTP (with `MailKit`)
- **Background Tasks**: `IHostedService` / `BackgroundService`
- **HTTP Client**: `HttpClientFactory` for external service calls
- **Dependency Injection**: Built-in .NET Core DI
- **API Documentation**: Swagger / Swashbuckle

---

## 📂 Project Structure

## 🚀 Key Features

### ✅ RESTful API
- CRUD for Products, Categories, Users, Orders
- Secure Admin/User roles
- Pagination & Filtering

### 🔒 Authentication & Authorization
- JWT Token-based
- Role-based access control
- Token refresh mechanism

### 📧 Email Automation
- Send registration & order confirmation emails
- SMTP integration via `MailKit`
- Email templates support
- Triggered via background service or controller

### 🛠️ Background Services
- Order invoice generation
- Scheduled email reminders
- Long-running services using `BackgroundService`

### 🌐 External API Integration
- Currency exchange rates / shipping APIs using `HttpClientFactory`
- Error handling and retries with Polly

### 🧪 Unit & Integration Tests
- xUnit or NUnit with Moq
- Repository pattern (optional)
