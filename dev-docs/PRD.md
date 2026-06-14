<!-- Last updated: 2026-06-11 -->
<!-- Last change: Initial PRD creation -->

# Hillary's Hair Care - Product Requirements Document

## Problem Statement

Hillary runs a hair salon and needs a simple management tool to schedule appointments between customers and stylists, track which services were performed, and manage her roster of stylists and service menu. Today she manages this manually. The goal is a web app where she can see upcoming appointments at a glance, book new ones, and keep her records tidy when things change.

## Target Users

Hillary (the salon owner) is the sole user. There is no login or role system in scope. The app is an internal tool, not a customer-facing booking portal.

## Core Requirements

Each feature below corresponds to a GitHub issue. All API endpoints follow the Minimal API pattern used in LoncotesLibrary and CreekRiver: no controllers, endpoints defined directly in `Program.cs`, responses shaped with DTOs.

### Appointments
| Issue | Feature | Endpoint |
|-------|---------|----------|
| #1 | View all appointments | `GET /api/appointments` |
| #2 | View appointment details | `GET /api/appointments/{id}` |
| #3 | Create a new appointment | `POST /api/appointments` |
| #4 | Cancel an appointment | `PUT /api/appointments/{id}/cancel` (soft cancel via `IsCancelled = true`) |
| #5 | Edit services on an appointment | `PUT /api/appointments/{id}/services` |

### Customers
| Issue | Feature | Endpoint |
|-------|---------|----------|
| #6 | View all customers | `GET /api/customers` |
| #7 | Add a new customer | `POST /api/customers` |

### Stylists
| Issue | Feature | Endpoint |
|-------|---------|----------|
| #8 | View all stylists | `GET /api/stylists` |
| #9 | Add a new stylist | `POST /api/stylists` |
| #10 | Deactivate a stylist | `PUT /api/stylists/{id}/deactivate` (soft deactivate via `isActive = false`) |

### Services
| Issue | Feature | Endpoint |
|-------|---------|----------|
| #11 | View all services | `GET /api/services` |
| #12 | Add a new service | `POST /api/services` |

### Business rules
- Appointments with inactive stylists can still be viewed (historical data preserved)
- New appointments cannot be booked with an inactive stylist
- Cancelling sets `IsCancelled = true`; records are never deleted
- Deactivating a stylist sets `isActive = false`; historical appointments are preserved
- Appointment total cost = sum of prices of all linked services (computed at query time, not stored)
- Editing services on an appointment replaces the full set of `AppointmentService` join records

## Technical Stack

### Stack Decisions

| Layer | Choice | Rationale |
|-------|--------|-----------|
| API framework | ASP.NET Minimal APIs (.NET 10) | Course pattern; no controllers |
| ORM | Entity Framework Core | Course pattern for this book |
| Database | PostgreSQL | Course standard |
| Frontend | React (Vite) in `/client` | Course standard |
| Routing | react-router-dom | Course standard |

## Data Model

Four main entities with one join table:

```
Customer  --<  Appointment  >--  AppointmentService  >--  Service
                  |
               Stylist
```

- `Appointment` links one `Customer` and one `Stylist`
- `AppointmentService` is the many-to-many join between `Appointment` and `Service`
- Total cost is computed from `Service.Price` values on the join records

### Already built
- All five model classes (`Appointment`, `AppointmentService`, `Customer`, `Service`, `Stylist`)
- All five DTO classes (see implementation notes below)
- `HillarysHairDbContext` with full seed data
- Migrations applied

### Implementation notes (address before or during relevant feature)

1. **`AppointmentDTO` uses raw model types**: `Customer` and `Stylist` navigation properties are typed as their model classes instead of `CustomerDTO` and `StylistDTO`. This mirrors LoncotesLibrary's pattern of DTO-to-DTO nesting and avoids JSON circular reference errors. Fix before writing the appointments endpoint.

2. **`Appointment` model missing `AppointmentServices` navigation property**: Without `public List<AppointmentService> AppointmentServices { get; set; }`, EF Core cannot `.Include()` services on an appointment. Add this before implementing issues #1, #2, or #5.

## Scope

### In Scope (v1)
- All 12 GitHub issues
- React frontend with a view/component for each feature
- Full-stack feature flow: one feature at a time, build API endpoint then wire up the UI

### Out of Scope
- Authentication and login
- Customer self-service booking portal
- Appointment reminders or notifications
- Time conflict validation (two appointments at the same time for same stylist)
- Editing or deleting customers and services

## Success Criteria

- All 12 GitHub issues closed
- Each feature works end-to-end from the React UI through the API to the database
- Patterns from LoncotesLibrary/CreekRiver are applied consistently: DTO projection in LINQ, `Include`/`ThenInclude` for related data, `Results.Created`/`Results.Ok`/`Results.NoContent`/`Results.NotFound` for HTTP responses

## Learning Goals

This is an NSS Book 3 capstone project. The goal is independent application of what the book introduced, not just building a working app.

- Write EF Core LINQ queries with `Include`, `ThenInclude`, and `Select` projections onto DTOs
- Design and use a many-to-many relationship (`AppointmentService`) in queries and mutations
- Handle soft deletes and soft deactivation as a pattern (vs. hard deletes)
- Build a full-stack feature end-to-end without a walkthrough tutorial
- Use the feature-branch + PR workflow on a real project
