# ManagementSystem - N-Tier Architecture

This is an advanced management system built with .NET Core WebAPI using layered architecture and modern development practices.

## 🔧 Technologies Used
- **ASP.NET Core 8 WebAPI**
- **Entity Framework Core**
- **AutoMapper**
- **FluentValidation**
- **Serilog** (Logs to both Console and MSSQL)
- **MemoryCache & Redis** for caching
- **Identity Framework** (User, Role, Authorization)
- **Unit of Work & Repository Pattern**
- **Swagger for API Testing**
- **Soft Delete** using `DataStatus` enum
- **Dependency Resolvers**

## 📁 Project Structure
- `ENTITIES`: Entity models, enums, interfaces
- `DAL`: Context, Configurations, Repositories, UnitOfWork
- `BLL`: DTOs, Managers, Validations, Dependency Injections
- `WebAPI`: Controllers, Middleware, Program.cs

## 🧪 Features
- Full CRUD for Products, Categories, Users, Roles
- Role-based Authorization (Admin/User)
- Login, Password Change functionality
- Logging and Exception Handling
- Caching with Memory and Redis (Sliding & Absolute Expiration)
- FluentValidation integrated per DTO

## 🔒 Admin Roles
| Role | Description |
|------|-------------|
| Admin | Full access |
| User | Read-only access |

---

> Developed as a full-stack backend project to demonstrate clean coding principles, logging, caching, security, and scalability.
