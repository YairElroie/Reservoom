# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

```bash
dotnet build
dotnet run
```

### EF Core Migrations

```bash
# Add a new migration
dotnet ef migrations add <MigrationName>

# Apply migrations manually (also runs automatically on startup)
dotnet ef database update
```

## Architecture

This is a WPF desktop app (.NET 9.0-windows) using the **MVVM pattern** with `Microsoft.Extensions.Hosting` for DI. All wiring happens in `App.xaml.cs`.

### Data flow

```
View → Command → Store → Model → Service (interface) → Database implementation
```

- **Models** (`Models/`) — pure domain logic. `Hotel` owns a `ReservationBook`, which delegates to injected service interfaces.
- **Services** (`Services/`) — three interfaces with database implementations: `IReservationProvider`, `IReservationCreator`, `IReservationConflictValidator`. All hit the database via `ReservoomDbContextFactory`.
- **DTOs** (`DTOs/`) — EF Core entities, kept separate from domain models. `ReservationDTO` is what EF maps; domain `Reservation` is what the rest of the app uses.
- **Stores** (`Stores/`) — singleton application state. `HotelStore` caches reservations in memory and fires `ReservationMade` events. `NavigationStore` holds the active `ViewModelBase` and fires `CurrentViewModelChanged`.
- **ViewModels** (`ViewModels/`) — bind to Views via `DataContext`. `MainViewModel` exposes `CurrentViewModel` driven by `NavigationStore`. `ReservationListingViewModel` and `MakeReservationViewModel` are the two pages.
- **Commands** (`Commands/`) — all commands extend `CommandBase` (implements `ICommand`). Async operations extend `AsyncCommandBase`, which guards `CanExecute` while executing.
- **Navigation** — `NavigationService<TViewModel>` sets `NavigationStore.CurrentViewModel`. The previous ViewModel is disposed automatically. `MainWindow.xaml` uses `DataTemplate`s keyed by ViewModel type to swap views.

### DI registration

All services, stores, ViewModels, and the `MainWindow` are registered in `App.xaml.cs`. `ReservationListingViewModel` is registered as `Transient` with a `Func<>` factory so it can be resolved fresh on each navigation. Migrations run once at startup via `dbContext.Database.Migrate()`.

### Database

The active database provider is configured in `appSettings.json`. Both SQLite and SQL Server are supported:

**SQLite (default):**
```json
{
  "DatabaseProvider": "Sqlite",
  "ConnectionStrings": {
    "Default": "Data Source=reservoom.db"
  }
}
```

**SQL Server:**
```json
{
  "DatabaseProvider": "SqlServer",
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=Reservoom;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

`ReservoomDbContextFactory` reads `DatabaseProvider` and calls `.UseSqlite()` or `.UseSqlServer()` accordingly. Migrations are applied automatically on startup via `dbContext.Database.Migrate()`.

> **Note:** If you switch providers, delete the existing migrations and regenerate them with the new provider active in `appSettings.json`:
> ```bash
> dotnet ef migrations remove
> dotnet ef migrations add Initial
> ```
