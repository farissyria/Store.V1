# Store.V1 - E-Commerce API (Clean Architecture)
<div align="center">
  
**⚠️ READ THIS FIRST ⚠️**

*The repository is named `Store.V1`, but this is **NOT version 1.0** of the software.*  
*This is the complete, production-ready e-commerce API (v2.0+).*  
*The "V1" is legacy naming and does not indicate version number.*

</div>
A robust, scalable RESTful API for e-commerce store management built with **.NET Core**, **SQL Server**, and **Clean Architecture** principles.

## 🏗️ Clean Architecture Overview

This project follows **Clean Architecture** (also known as Onion Architecture) to achieve separation of concerns, maintainability, and testability.
<table>
<tr>
<td align="center" style="background:#e8f5e9; padding:20px; border-radius:10px">
<b>🟢 PRESENTATION LAYER</b><br>
API / Controllers
</td>
</tr>
<tr>
<td align="center" style="background:#e1f5fe; padding:20px; border-radius:10px">
<b>🔵 APPLICATION LAYER</b><br>
DTOs / Services / Mapping
</td>
</tr>
<tr>
<td align="center" style="background:#f3e5f5; padding:20px; border-radius:10px">
<b>🟣 DOMAIN LAYER (Core)</b><br>
Entities / Interfaces 
</td>
</tr>
<tr>
<td align="center" style="background:#fff3e0; padding:20px; border-radius:10px">
<b>🟠 INFRASTRUCTURE LAYER</b><br>
Data/DbContext / Repositories
</td>
</tr>
</table>

 ### Dependency Rule
Dependencies point inward. The inner layers (Domain) have no dependencies on outer layers (Infrastructure, Presentation).

## 🚀 Technologies Used

- **.NET Core 9** - Backend framework
- **Clean Architecture** - Separation of concerns
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Primary database
- **ASP.NET Core Web API** - RESTful API
- **JWT Authentication** - Secure authentication
- **Swagger/OpenAPI** - API documentation
- **AutoMapper** - Object mapping
- **Visual Studio 2022** - IDE


## 📋 Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with:
  - ASP.NET and web development workload
  - .NET desktop development workload
- [.NET Core SDK 9.0 ](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express/Developer)
- [Git](https://git-scm.com/)

## 🔧 Installation & Setup

### 1. Clone the repository
```bash
git clone https://github.com/farissyria/Store.V1.git
cd Store.V1
