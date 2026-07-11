# Module 6 – ASP.NET Core 8.0 Web API (C#)

A standalone, runnable ASP.NET Core 8 Web API covering every learning
objective in **Module 6** of the DN 5.0 Deep Skilling handbook. It uses
EF Core's **InMemory** provider, so it runs with just `dotnet run` — no
SQL Server setup required.

## Project layout

```
Module6WebApi.sln
src/Module6.WebApi/
  Program.cs                          pipeline: Serilog, EF Core, JWT auth, Swagger, CORS, middleware
  appsettings.json                    Jwt / ApiKey / Cors / Serilog settings
  Controllers/
    AuthController.cs                 POST /api/auth/login -> JWT
    BooksController.cs                full REST CRUD, JWT-protected writes
    ReportsController.cs              protected by API key instead of JWT
  Data/
    AppDbContext.cs                   EF Core InMemory + seed data
  Models/
    Book.cs
    Dtos/BookDtos.cs                  BookDto, CreateBookDto, UpdateBookDto
    Auth/AuthModels.cs                LoginRequest, LoginResponse
  Services/
    BookRepository.cs                 EF Core-backed repository
    UserStore.cs                      in-memory user store for the auth demo
    JwtTokenService.cs                issues JWTs
  Middleware/
    ExceptionHandlingMiddleware.cs    global exception handling -> JSON error responses
    ApiKeyMiddleware.cs               protects /api/reports/* with an API key header
  Filters/
    LogExecutionTimeFilter.cs         custom IActionFilter, logs timing for every action
```

## Where each learning objective is demonstrated

| Topic | Location |
|---|---|
| REST / Web API / Microservice concepts | See "REST vs. SOAP" and "Web API vs. Microservice" notes below |
| Action verbs (GET, POST, PUT, DELETE) | `BooksController.cs` |
| Attribute routing & query parameters | `[Route("api/[controller]")]`, `[HttpGet("{id:int}")]`, `GetAll([FromQuery] decimal? minPrice, ...)` in `BooksController.cs` |
| CRUD with EF Core | `BookRepository.cs` + `AppDbContext.cs` |
| Swagger / OpenAPI | `AddSwaggerGen(...)` in `Program.cs` — browse to `/swagger` |
| JWT-based authentication & authorization | `AuthController.cs`, `JwtTokenService.cs`, `AddAuthentication().AddJwtBearer(...)` in `Program.cs`, `[Authorize(Roles = "Admin")]` on write endpoints |
| Middleware & custom filters | `ExceptionHandlingMiddleware.cs`, `ApiKeyMiddleware.cs`, `LogExecutionTimeFilter.cs` |
| Global exception handling | `ExceptionHandlingMiddleware.cs` (first in the pipeline) |
| Logging with Serilog | `builder.Host.UseSerilog(...)` in `Program.cs`, `Serilog` section in `appsettings.json` |
| Securing APIs with API keys / OAuth-style bearer tokens | `ApiKeyMiddleware.cs` (API key) vs. JWT bearer (OAuth-style) — two different schemes shown side by side |
| CORS | `AddCors(...)` / `UseCors(...)` in `Program.cs`, `Cors:AllowedOrigins` in `appsettings.json` |
| API documentation and testing | Swagger UI (interactive docs + "Try it out"), curl examples below |

## How to run

```bash
cd src/Module6.WebApi
dotnet run
```

Then open **https://localhost:7060/swagger** (the launch profile opens this
automatically). The database is seeded in memory with 3 books on startup —
no external database needed.

## Trying it out

**1. Get a token (Admin can write, Reader is read-only by design — both can
read since GET endpoints are `[AllowAnonymous]`):**
```bash
curl -X POST https://localhost:7060/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"Admin@123"}'
```
Copy the `token` value from the response.

**2. Read books (no auth required):**
```bash
curl https://localhost:7060/api/books
curl "https://localhost:7060/api/books?minPrice=8&maxPrice=9"
curl https://localhost:7060/api/books/1
```

**3. Create a book (requires the Admin JWT from step 1):**
```bash
curl -X POST https://localhost:7060/api/books \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"title":"Brave New World","author":"Aldous Huxley","publishedYear":1932,"price":7.49}'
```

**4. Update / delete similarly, with the same `Authorization` header,**
using `PUT /api/books/{id}` and `DELETE /api/books/{id}`.

**5. Reports endpoint — protected by an API key, not JWT:**
```bash
curl https://localhost:7060/api/reports/inventory-value \
  -H "X-Api-Key: dev-reports-api-key-12345"
```
Omit the header (or send the wrong value) and you'll get a 401 — this is
handled entirely in `ApiKeyMiddleware.cs`, before the request ever reaches
the controller.

**In Swagger UI:** click the "Authorize" button, paste the JWT token (just
the token, Swashbuckle adds the `Bearer ` prefix for you), and every
subsequent "Try it out" call in the UI will include it automatically.

## REST vs. SOAP, Web API vs. Microservice

- **RESTful web service**: an HTTP API built around resources (`/api/books`,
  `/api/books/5`) and standard verbs (GET/POST/PUT/DELETE), typically using
  JSON. `BooksController` in this project is a REST API.
- **SOAP / WCF**: an older, XML-envelope-based protocol with a strict
  contract (WSDL). WCF (the classic .NET way to build SOAP services) is not
  part of modern ASP.NET Core — building a *new* SOAP service today
  generally means using the community-maintained **CoreWCF** project, and
  consuming an existing SOAP service from .NET 8 means adding a *Connected
  Service* / *WCF Web Service Reference* in Visual Studio, or generating a
  client from the WSDL with `dotnet-svcutil`. Because this requires a live
  WSDL endpoint to generate against, it isn't included as runnable code
  here — this section is the conceptual coverage the handbook module asks
  for.
- **Web API vs. Microservice**: a Web API is the *interface* — the HTTP
  surface an application exposes. A microservice is an *architectural
  choice* — a small, independently deployable service owning its own data,
  often exposing a Web API as its interface (and this Web API could also
  be one microservice among several, e.g. a "Catalog" service, in a larger
  system — see Module 7 for the full microservices architecture module).

## Notes

- The JWT signing key in `appsettings.json` is a development-only
  placeholder — never ship a real key in source control. In production,
  use `dotnet user-secrets` locally and a secret store (Azure Key Vault,
  AWS Secrets Manager, etc.) in deployed environments.
- I don't have the .NET 8 SDK available in this sandbox, so I couldn't run
  `dotnet run` or exercise these endpoints end-to-end myself. I did check
  every file's brace/paren balance by hand; please build and run it on
  your machine and let me know if anything needs fixing.
- The EF Core InMemory provider is used here purely so the project runs
  without any external setup. For a real application, swap
  `UseInMemoryDatabase(...)` for `UseSqlServer(...)` — the rest of
  `BookRepository` and the controllers don't need to change, which is
  itself a nice illustration of why coding against `IBookRepository`
  instead of `AppDbContext` directly is a good idea.
