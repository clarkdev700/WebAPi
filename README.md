# WebAPi – Book Library REST API

A **.NET 5 Web API** for managing a book library. It exposes REST endpoints to create and query **Authors** and **Books**, backed by a SQL Server database through Entity Framework Core.

---

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [API Endpoints](#api-endpoints)
- [Entities](#entities)
- [Data Transfer Objects (DTOs)](#data-transfer-objects-dtos)
- [Database & Seeding](#database--seeding)
- [Technologies & Dependencies](#technologies--dependencies)
- [Getting Started](#getting-started)
- [Running Tests](#running-tests)

---

## Features

| Feature | Details |
|---|---|
| Author management | List all authors, get a single author (with or without their books), create a new author |
| Book management | List all books, get a single book by ID |
| Relational data | Authors can have many books; cascade delete is configured |
| Input validation | Data annotations enforce required fields, max lengths and character rules |
| Object mapping | AutoMapper profiles convert between entities and DTOs |
| Database migrations | EF Core code-first migrations with automatic schema creation |
| Seed data | 6 pre-loaded authors and 10 pre-loaded books |
| Logging | NLog integration with daily log files |
| API documentation | Swagger / OpenAPI UI available in development |
| Unit tests | MSTest + Moq tests for the `AuthorController` |

---

## Architecture

The project follows a layered, **Repository Pattern** design:

```
Controllers   ──▶   IRepository<T>   ──▶   DbContext (EF Core)   ──▶   SQL Server
     │                                                                      │
     └── DTOs (AutoMapper)                                          Seed data
```

- **Controllers** – receive HTTP requests, validate input, return DTOs.
- **Repository interfaces / implementations** – abstract all database access (`AuthorRepository`, `BookRepository`).
- **DTOs** – decouple the API contract from the EF Core entity model.
- **AutoMapper profiles** – define mappings between entities and DTOs.
- **DbContext** – `BookContext` contains the `Authors` and `Books` DbSets and triggers database creation / seeding on first run.

---

## API Endpoints

### Authors – `/api/author`

| Method | Route | Description | Success | Error |
|--------|-------|-------------|---------|-------|
| `GET` | `/api/author` | Get all authors (with their books) | `200 OK` | – |
| `GET` | `/api/author/{id}` | Get one author. Pass `?includeBook=true` to include their books | `200 OK` | `404 Not Found` |
| `POST` | `/api/author` | Create a new author (body: `AuthorCreationDto`) | `201 Created` | `400 Bad Request` |

### Books – `/api/book`

| Method | Route | Description | Success | Error |
|--------|-------|-------------|---------|-------|
| `GET` | `/api/book` | Get all books | `200 OK` | – |
| `GET` | `/api/book/{id}` | Get one book by ID | `200 OK` | `404 Not Found` |

---

## Entities

### Author

| Property | Type | Constraints |
|----------|------|-------------|
| `Id` | `int` | PK, auto-increment |
| `Name` | `string` | Required, max 50 chars, letters only |
| `LastName` | `string` | Required, max 50 chars, letters only |
| `Books` | `ICollection<Book>` | Navigation property |

### Book

| Property | Type | Constraints |
|----------|------|-------------|
| `Id` | `int` | PK, auto-increment |
| `Title` | `string` | Required, max 70 chars |
| `PageNumber` | `int` | – |
| `AuthorId` | `int` | FK → Author (cascade delete) |

---

## Data Transfer Objects (DTOs)

| DTO | Direction | Fields |
|-----|-----------|--------|
| `AuthorDto` | Response | `Id`, `Name`, `LastName`, `Books` |
| `AuthorCreationDto` | Request (POST) | `Name`, `LastName`, `Books` |
| `BookDto` | Response | `Id`, `Title`, `PageNumber` |
| `BookCreationDto` | Request | `Title`, `PageNumber`, `AuthorId` |

---

## Database & Seeding

- **Provider**: SQL Server (LocalDB by default)
- **Connection string** (see `appsettings.json`):
  ```
  Server=(localdb)\mssqllocaldb;Database=BookDB;Trusted_Connection=True;
  ```
- The database schema is created automatically on first run (`Database.EnsureCreated()`).
- **Seed data** includes:
  - 6 authors: William Shakespeare, Eric Dupuis, Philips Iham, Clark Emmerson, Samuel Konate, Alain Lebelier
  - 10 books: Hamlet, King Lear, Othello, Le book noir, L'Afrique, First day at Paris, Le marchand, My Dream, Le renard et le corbeau, Sa majeste

---

## Technologies & Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| .NET | 5.0 | Target framework |
| Entity Framework Core | 5.0.14 | ORM / database access |
| EF Core SQL Server | 5.0.12 | SQL Server provider |
| EF Core Tools | 5.0.12 | Migrations |
| AutoMapper | 10.1.1 | Object-to-object mapping |
| AutoMapper DI extension | 8.1.1 | AutoMapper + DI integration |
| Swashbuckle (Swagger) | 5.6.3 | OpenAPI / Swagger UI |
| Newtonsoft.Json | 13.0.1 | JSON serialization |
| NLog | 4.7.13 | Logging |
| NLog.Web.AspNetCore | 4.14.0 | ASP.NET Core logging integration |
| MSTest | 2.2.3 | Unit testing framework |
| Moq | 4.17.1 | Mocking for unit tests |
| coverlet | 3.0.2 | Code coverage |

---

## Getting Started

### Prerequisites

- [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- SQL Server LocalDB (included with Visual Studio) **or** a SQL Server instance

### Run the API

```bash
# Restore dependencies
dotnet restore WebAPi.sln

# Run the API (database is created automatically on first run)
dotnet run --project WebAPi/WebAPi.csproj
```

The API will be available at `https://localhost:5001` / `http://localhost:5000`.  
Open `https://localhost:5001/swagger` to explore the endpoints in the Swagger UI.

### Apply Migrations manually (optional)

The database is auto-created on startup. To apply migrations explicitly:

```bash
dotnet ef database update --project WebAPi/WebAPi.csproj
```

---

## Running Tests

```bash
dotnet test WebApiTest/WebApiTest.csproj
```

The test project (`WebApiTest`) contains unit tests for `AuthorController` using **MSTest** and **Moq**:

- `GetAuthor_ReturnNotFound_GivenMissingID` – verifies a 404 response when the author does not exist.
- `GetAuthor_ReturnAuthor_GivenCorrectId` – verifies a 200 response with correct author data.
- `CreateAuthor_ReturnBadRequest_ForInvalidAuthorDto` – stub (marked inconclusive).
- `CreateAuthor_Return201_For_validAuthorDto` – stub (marked inconclusive).
