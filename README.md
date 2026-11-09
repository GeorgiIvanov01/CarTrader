# CarTrader

## To start the application

1. Set your database connection string in `CarTrader/appsettings.Development.json` under `ConnectionStrings:DefaultConnection`.
2. Make sure that `CarTrader.Web` is set as the startup project in Visual Studio.
3. Update the database using the existing Entity Framework Core migrations.
4. Register an account.
5. Start browsing vehicles.

The application uses SQL Server and the default development configuration expects a local SQL Server Express instance:

```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Your ConnectionStrings"
  }
}
```

Replace this connection string when your SQL Server instance. Do not commit credentials or other sensitive connection details to source control.

### Update the database

From the solution directory, run:

```powershell
dotnet ef database update --project Data/CarTrader.Data.csproj --startup-project CarTrader/CarTrader.Web.csproj
```

Alternatively, in Visual Studio, open Package Manager Console, select `CarTrader.Data` as the default project, and run:

Update-Database

### Run the application

In Visual Studio, select the `https` or `http` launch profile and start `CarTrader.Web`.

The configured development URLs are:

- HTTPS: `https://localhost:7148`
- HTTP: `http://localhost:5249`

## Overview

CarTrader is an ASP.NET Core Razor Pages and MVC web application for browsing and managing vehicle listings. Users can explore available vehicles, view details, save favorites, and manage their own listings after signing in.

The solution targets **.NET 10** and uses Entity Framework Core with SQL Server for persistence and ASP.NET Core Identity for authentication.

## Features

- Browse all available vehicles.
- View detailed vehicle information.
- Register and log in using ASP.NET Core Identity.
- Create vehicle listings.
- View, edit, and delete listings owned by the current user.
- Save vehicles to a favorites list and remove them later.
- Use seeded vehicle categories and sample vehicle data after the database is migrated.
- Use responsive styling based on Bootstrap and custom site CSS.

## Solution structure

| Project | Purpose |
| --- | --- |
| `CarTrader/` | Web application containing the ASP.NET Core host, controllers, Razor views, Identity pages, static assets, and configuration. |
| `Data/` | Entity Framework Core `DbContext`, entities, enums, seed data, and database migrations. |
| `Services/` | Application service layer and service contracts for vehicle and favorites operations. |
| `CarTrader.ViewModels/` | View models used by the web application for vehicle-related forms and pages. |
| `CarTrader.GCommon/` | Shared validation constants and common functionality. |

## Main technologies

- .NET 10
- ASP.NET Core
- Razor Pages and ASP.NET Core MVC
- ASP.NET Core Identity
- Entity Framework Core 10
- SQL Server
- Bootstrap
- jQuery validation

## Database and seed data

The database context is `Data/CarTraderDbContext.cs`. It contains the Identity tables and the application entities:

- `Vehicle`
- `Category`
- `UserVehicle`

The repository includes an initial migration in `Data/Migrations/`. Applying the migration creates the database schema and adds the seeded categories and sample vehicles. The application also contains a seeded Identity user, but the password is stored as a hash; use the application's registration flow.

## Authentication and authorization

Vehicle browsing and details are available to anonymous visitors. Creating, editing, deleting, and managing favorites requires authentication. Users can edit or delete only their own vehicle listings.

For development convenience, account confirmation is disabled and the password policy is relaxed in `CarTrader/Program.cs`. Review these settings and use a stronger production configuration before deploying the application.

## Development notes

- Keep the connection string in `appsettings.Development.json` appropriate for the local SQL Server environment.
- Apply new Entity Framework Core migrations whenever the data model changes.
- Keep `CarTrader.Web` as the startup project because it supplies the application configuration and hosting environment used by Entity Framework Core.
- Do not commit passwords, production connection strings, or other secrets.
