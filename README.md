# 📦 Clean Architecture – Minimal API Template (.NET 8)

A production-ready .NET 8 Minimal API template using Clean Architecture. Includes domain-driven design, validation, CQRS with MediatR, Entity Framework Core, and structured exception handling.

## 🚀 Getting Started

``` bash
git clone https://github.com/your-username/CleanArchTemplate.git
cd CleanArchTemplate
dotnet build
dotnet run --project CleanArchTemplate.Presentation
```

API will be available at:


## 🧱 Project Structure

``` text
├── CleanArchTemplate.Application     # Application layer: use cases, interfaces, validation
│   ├── Common/
│   ├── Users/
├── CleanArchTemplate.Domain          # Domain layer: entities, base classes
├── CleanArchTemplate.Infrastructure  # Infrastructure layer: EF Core, repositories
├── CleanArchTemplate.Presentation    # API layer: endpoints, exception handling, DI setup
├── sample-requests                   # HTTP requests for testing
├── scripts                           # PowerShell helper scripts (migrations, scaffolding)
├── CleanArchTemplate.sln            # Solution file
```

## 📦 Key Features

- ✅ Minimal API with Clean Architecture
- 🧱 Domain-Driven Design with separation of concerns
- 📬 CQRS using MediatR
- 🗃️ Entity Framework Core with SQLite by default
- 📥 FluentValidation for input validation
- 🌐 Swagger for API documentation
- 🔧 Structured exception handling
- 🧪 Ready for unit testing

## 🛠️ Database Setup

By default, SQLite is used (`testdb.db` file in root). To update the database:

``` bash
dotnet ef database update --project CleanArchTemplate.Infrastructure --startup-project CleanArchTemplate.Presentation
```

Use PowerShell scripts in the `scripts/` folder for common operations:

- `add-migrations.ps1`
- `remove-migrations.ps1`
- `update-database.ps1`
- `scaffolding.ps1`

## ✉️ Sample Requests

Test the API using `.http` files under `sample-requests/`:

- `createUser.http`
- `getUserById.http`
- `removeUser.http`
- `updateUser.http`

Use the [REST Client extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) in VS ``` to send them directly.

## 🔐 User Secrets (Optional)

If switching to SQL Server, store connection string securely:

``` bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
```

## 🧪 Run Tests

``` bash
dotnet test
```