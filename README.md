# 📦 Clean Architecture – Minimal API Template (.NET 8)

A production-ready .NET 8 Minimal API template using Clean Architecture. Includes domain-driven design, Result-pattern, validation, Entity Framework Core, and structured exception handling.

## 🚀 Getting Started

``` bash
git clone https://github.com/hrnanhrnanhrnan/CleanArchTemplate.git
cd CleanArchTemplate
dotnet build
dotnet run --project CleanArchTemplate.Presentation
```

If you want to rename the template, just run the `rename-template.ps1` script in the root folder.
(⚠️ Just make sure to run the script from a terminal outside of your editor and not to have any editor or explorer open in the project. It will cause the top level folders and files to not be renamed)
Usage: 
```powershell
.\rename-template.ps1 -CurrentName "CleanArch" -NewName "MyApi" -ReInitializeGit
```
- `CurrentName` - Optional. Current name of the project. Will use the name of the root folder if not specificed.
- `NewName` - Mandatory. The wanted name of the project.
- `ReInitializeGit` - Optional. If current `.git` folder should be removed and a new one initiliazed. It will also run `git add .` and `git commit`.

API will be available at:
``` text
http://localhost:5284
```

## 🧱 Project Structure

``` text
├── CleanArchTemplate.Application     # Application layer: requests, handlers, services, interfaces, validation
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
- 🗃️ Entity Framework Core with SQLite by default
- 📥 FluentValidation for input validation
- 🔧 Structured exception handling

## 🛠️ Database Setup

By default, SQLite is used (`testdb.db` file in root). To update the database:

``` bash
dotnet ef database update --project CleanArchTemplate.Infrastructure --startup-project CleanArchTemplate.Presentation
```

Use PowerShell scripts in the `scripts/` folder for common operations:

- `add-migrations.ps1` use like this: `add-migrations.ps1 "MigrationName"`
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