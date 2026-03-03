# Avalonia Code Generator

A CLI tool that generates Entity Framework Core boilerplate from an existing database — models, DbContext, and more.

---

## Features

- Generates EF Core entity classes from database tables
- Generates `AppDbContext` with all `DbSet<T>` properties
- Supports nullable and primary key annotations (`[Key]`, `[Required]`)
- No `.edmx` files — works directly against a live database connection

## Upcoming Features

- View and ViewModel generation
- SQL script generation

---

## Supported Databases

| Database   | Status      |
|------------|-------------|
| SQLite     | ✅ Supported |
| SQL Server | ✅ Supported |

---

## Requirements

- .NET 10.0+

---

## Configuration

Create a `config.json` file in the same directory as the executable:

```json
{
  "DbType": "sqlite",
  "ConnectionString": "Data Source=mydb.db",
  "GeneratorOutputPath": "Output",
  "Namespace": "MyApp.Data.Model",
  "LogDirectory": "log"
}
```

| Field                 | Description                              |
|-----------------------|------------------------------------------|
| `DbType`              | `sqlite` or `sqlserver`                  |
| `ConnectionString`    | ADO.NET connection string                |
| `GeneratorOutputPath` | Folder where generated files are written |
| `Namespace`           | Root namespace for generated classes     |
| `LogDirectory`        | Folder where log files are written       |

---

## Usage

```bash
dotnet tool run CodeGenerator
```

---

## Output

```
Output/
├── AppDbContext.cs
└── Models/
    ├── Customer.cs
    ├── Order.cs
    └── Product.cs
```