# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Hillary's Hair Care is a full-stack salon management application built with:
- **Backend**: ASP.NET Core 9.0 Web API with Entity Framework Core
- **Database**: PostgreSQL with Npgsql provider
- **Frontend**: React 19 + Vite (in `client/` directory)
- **Routing**: React Router DOM v7

## Architecture

### Database Context Issue
**IMPORTANT**: There is a mismatch between the DbContext class name and its usage in Program.cs:
- The DbContext file is named `HHCDbContext.cs` with class `HillarysHairCareDbContext`
- However, Program.cs:14 references `CreekRiverDbContext` (wrong name)
- Connection string is configured as `"CreekRiverDbConnectionString"` in appsettings

This suggests the project was scaffolded from a "Creek River" template and needs to be updated to use the correct context name throughout.

### Data Models
The application manages a salon business with these core entities:
- **Stylist**: Hair stylists employed at the salon
- **Customer**: Clients who book appointments
- **Service**: Hair services offered (cuts, colors, etc.)
- **Appointment**: Scheduled appointments between customers and stylists

Each model has a corresponding DTO in `Models/DTOs/` for API responses.

### Entity Framework Configuration
- Uses Npgsql for PostgreSQL database connectivity
- Legacy timestamp behavior enabled (Program.cs:11) to handle datetimes without timezone data
- Connection string stored in user secrets (ID: `8d03dce7-3673-4aad-bddf-c2342b21b002`)

## Common Commands

### Backend (.NET API)
```bash
# Restore dependencies and build
dotnet restore
dotnet build

# Run the API (from project root)
dotnet run

# Run with watch mode for development
dotnet watch run

# Entity Framework migrations
dotnet ef migrations add MigrationName
dotnet ef database update
dotnet ef migrations remove  # Remove last migration

# Manage user secrets (connection string, etc.)
dotnet user-secrets set "CreekRiverDbConnectionString" "Host=localhost;Port=5432;Username=postgres;Password=yourpassword;Database=HillarysHairCare"
dotnet user-secrets list
```

### Frontend (React)
```bash
cd client

# Install dependencies
npm install

# Run development server
npm run dev

# Build for production
npm run build

# Lint code
npm run lint

# Preview production build
npm run preview
```

## Development Notes

### API Endpoints
No API endpoints are currently defined in Program.cs. When adding endpoints, use minimal API style:
- Map endpoints after `app.UseHttpsRedirection()` (Program.cs:24)
- Use dependency injection to access `HillarysHairCareDbContext`
- Return DTOs, not raw entity models

### Database Connection
- PostgreSQL connection string must be configured via user secrets
- The connection string key is `"CreekRiverDbConnectionString"` (needs renaming to match project)
- Development database is not tracked in git

### Client-Server Communication
- React app runs on Vite dev server (default: http://localhost:5173)
- .NET API runs on configured ports (check Properties/launchSettings.json)
- Configure CORS in Program.cs when connecting frontend to backend

### Model/DTO Pattern
- Database entities live in `Models/` directory
- DTOs live in `Models/DTOs/` subdirectory
- Always return DTOs from API endpoints, never expose EF entities directly
- DTOs should include navigation properties as needed for the UI
