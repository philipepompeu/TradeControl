# 📊 TradeControl

TradeControl is a study project focused on applying backend development best practices using **C# and ASP.NET Core 8.0**, targeting a professional architecture for RESTful APIs.

## 🚀 Purpose

The goal of this project was to consolidate backend skills using .NET by building a realistic financial portfolio API, simulating asset trades, positions, and reports.

---

## 🧱 Tech Stack

- **ASP.NET Core 8.0** (REST API with versioning)
- **PostgreSQL** via Docker
- **Entity Framework Core** (Code-First, Repository Pattern)
- **LINQ** for business calculations
- **HttpClient** integration with external B3 (Brazilian Stock Exchange) API
- **Swagger** for API documentation
- **Docker + Docker Compose**
- **Custom exception handling** with global middleware
- **Seed data generation** for users and assets

---

## 🔧 Project Highlights

### ✅ Implemented Features

| Feature                          | Description                                                                 |
|----------------------------------|-----------------------------------------------------------------------------|
| `GET /api/v1/users/{id}`         | Returns user's consolidated position by asset                               |
| `GET /api/v1/users/{id}/average-price?ticker=...` | Returns user's average price for a given asset              |
| `GET /api/v1/reports/brokerage-total` | Returns total brokerage fees across all users                  |
| `GET /api/v1/reports/top-positions`   | Returns top assets by value for each user (using in-memory LINQ processing) |

### 📦 Seed Data

- 2 test users
- 20+ assets representing top companies from B3
- Randomized trade operations for each user/asset

---

## 🧪 Concepts Practiced

- Clean and layered architecture (`Controllers → Services → Repositories`)
- Minimal Controllers, thin presentation layer
- LINQ: projections, aggregations, GroupBy with in-memory processing
- Dockerized environment with isolated database container
- `ASPNETCORE_URLS` + environment config for Docker compatibility

---

### Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/) or [Podman](https://podman.io/)

### Build and Run

```bash
docker-compose up --build
