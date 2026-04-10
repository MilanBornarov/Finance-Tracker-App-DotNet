# 💰 Finance Tracker App

A full-featured personal finance tracking web application built with **ASP.NET Core MVC** and **Microsoft SQL Server**. Users can manage their income and expenses, filter and sort transactions, and view a comprehensive monthly financial summary — all behind a secure authentication system.

---

## 🚀 Features

- **Authentication** — Register, Login, and Logout using ASP.NET Core Identity
- **Transaction Management** — Full CRUD for income and expense transactions
- **Filtering** — Filter by category, type (income/expense), date range, and keyword/description search
- **Sorting** — Sort transactions by price or date (ascending/descending)
- **Monthly Summary** — Filter by month to view:
  - ✅ Total Income
  - ❌ Total Expenses
  - 📊 Net Balance
- **Clean Architecture** — Repository pattern, Service layer, DTOs, ViewModels, and AutoMapper

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET Core) |
| Language | C# |
| Database | Microsoft SQL Server (SSMS) |
| ORM | Entity Framework Core |
| Auth | ASP.NET Core Identity |
| Mapping | AutoMapper |
| Pattern | Repository + Service + DTO + ViewModel |

---

## 📁 Project Structure
    📁 FinanceTrackerApp/
    ├── 📁 Areas/
    ├── 📁 Controllers/
    ├── 📁 Data/
    ├── 📁 DTOs/
    ├── 📁 Helpers/
    ├── 📁 Interfaces/
    ├── 📁 Mappings/
    ├── 📁 Migrations/
    ├── 📁 Models/
    ├── 📁 Properties/
    ├── 📁 Repositories/
    ├── 📁 Services/
    ├── 📁 ViewModels/
    ├── 📁 Views/
    ├── 📁 wwwroot/
    ├── 📄 appsettings.json
    ├── 📄 Program.cs
    └── 📄 ScaffoldingReadMe.txt
---

## ⚙️ Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server)
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup)

### Setup

1. **Clone the repository**
```bash
   git clone https://github.com/YOUR_USERNAME/finance-tracker-app.git
   cd finance-tracker-app
```

2. **Configure the database connection**

   Add your connection string via User Secrets (recommended):
```bash
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER;Database=FinanceTrackerDB;Trusted_Connection=True;"
```

   Or update `appsettings.json` directly (do not commit real credentials).

3. **Apply migrations**
```bash
   dotnet ef database update
```

4. **Run the app**
```bash
   dotnet run
```

5. Open your browser at `https://localhost:5001`
