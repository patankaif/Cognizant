# Module 7 – Microservices Architecture using ASP.NET Core Web API

A working three-service microservices system covering every learning
objective in **Module 7** of the DN 5.0 Deep Skilling handbook: two
independent services (each owning its own data), an API gateway in front
of them, inter-service communication, health checks, structured logging,
Docker containerization, and Kubernetes deployment manifests.

## Architecture

```
                     ┌────────────────┐
   client  ───────▶  │   ApiGateway   │   (YARP reverse proxy, port 5080/8080)
                     └───────┬────────┘
                 /catalog/*  │  /orders/*
              ┌──────────────┴───────────────┐
              ▼                               ▼
   ┌─────────────────────┐        ┌──────────────────────┐
   │   CatalogService     │        │    OrdersService      │
   │   (owns Books data)  │◀───────│  (owns Orders data,   │
   │   port 5081/8080     │  HTTP  │   calls Catalog for   │
   │                       │        │   price/stock checks) │
   └─────────────────────┘        └──────────────────────┘
```

Each service owns its own data (no shared database) and exposes its own
Web API — this is the core microservices principle the earlier
"monolith" comparison in the handbook is contrasting against. `OrdersService`
never touches Catalog's data directly; it only talks to it over HTTP,
exactly as it would talk to a service written in a completely different
language or maintained by a different team.

## Project layout

```
Module7Microservices.sln
docker-compose.yml
src/
  CatalogService/            owns Book data (EF Core InMemory)
    Controllers/BooksController.cs
    Data/CatalogDbContext.cs
    Program.cs, Dockerfile
  OrdersService/              owns Order data, depends on CatalogService
    Controllers/OrdersController.cs
    Services/CatalogServiceClient.cs   typed HttpClient - inter-service call
    HealthChecks/CatalogServiceHealthCheck.cs
    Program.cs, Dockerfile
  ApiGateway/                 YARP reverse proxy - single entry point
    Program.cs, appsettings.json (routes/clusters), Dockerfile
k8s/
  catalog-service.yaml        Deployment + Service, rolling update strategy
  orders-service.yaml
  api-gateway.yaml            exposed as LoadBalancer
ci-cd/
  github-actions-docker-build.yml   build, push images, then rolling deploy
```

## Where each learning objective is demonstrated

| Topic | Location |
|---|---|
| Microservices architecture pattern | The 3-service layout itself — see diagram above |
| Advantages/challenges, comparison with Monolith | See "Microservices vs. Monolith" section below |
| Inter-service communication (HTTP) | `CatalogServiceClient.cs` in OrdersService — `IHttpClientFactory`-backed typed client |
| Service discovery pattern | Docker Compose (`docker-compose.yml`, containers resolve each other by service name, e.g. `http://catalog-service:8080`) and Kubernetes (`k8s/*.yaml`, Services get cluster DNS names) — see "Service Discovery" section below |
| Database-per-service pattern | `CatalogDbContext` (Catalog's own InMemory DB) vs. the in-memory `Orders` list in `OrdersController` — no shared database |
| Monitoring and logging in microservices | Serilog in every service (`Program.cs`, tagged with `Service` property), `/health` endpoint on every service, `CatalogServiceHealthCheck.cs` (OrdersService reports Catalog's health as part of its own) |
| Deployment strategies (rolling updates) | `k8s/*.yaml` — `strategy.type: RollingUpdate` with `maxSurge`/`maxUnavailable` |
| CI/CD pipelines | `ci-cd/github-actions-docker-build.yml` |
| Docker / Kubernetes automation | `Dockerfile` per service, `docker-compose.yml`, `k8s/*.yaml` |

## How to run

**Option 1 — Docker Compose (closest to production topology):**
```bash
docker compose up --build
```
Then:
```bash
curl http://localhost:5080/catalog/api/books
curl -X POST http://localhost:5080/orders/api/orders \
  -H "Content-Type: application/json" \
  -d '{"bookId":1,"quantity":2}'
curl http://localhost:5080/orders/api/orders
```
All traffic goes through the gateway on port 5080; it routes `/catalog/*`
to CatalogService and `/orders/*` to OrdersService, resolving each by its
Docker Compose service name (`catalog-service`, `orders-service`) — this
is Docker's built-in DNS-based service discovery.

**Option 2 — run each service locally with `dotnet run`:**
```bash
cd src/CatalogService && dotnet run &     # http://localhost:5081
cd src/OrdersService  && dotnet run &     # http://localhost:5082
cd src/ApiGateway     && dotnet run &     # http://localhost:5080
```
The default `appsettings.json` in each project already points at these
local ports, so no configuration changes are needed for local dev.

**Option 3 — Kubernetes** (after building and pushing images, update the
`image:` fields in `k8s/*.yaml` to your registry):
```bash
kubectl apply -f k8s/catalog-service.yaml
kubectl apply -f k8s/orders-service.yaml
kubectl apply -f k8s/api-gateway.yaml
```

## Service discovery

Three different mechanisms are illustrated, from simplest to most dynamic:

1. **Local dev** — hardcoded `localhost` ports in `appsettings.json` (no
   real discovery, fine for a single machine).
2. **Docker Compose** — containers on the same Compose network resolve
   each other by container/service name automatically (`catalog-service`,
   `orders-service`) — Docker's embedded DNS server handles this. See the
   `Services__CatalogServiceBaseUrl` and `ReverseProxy__Clusters__...`
   environment variable overrides in `docker-compose.yml`.
3. **Kubernetes** — every `Service` object gets a stable DNS name inside
   the cluster (`catalog-service`, `orders-service`), and `kube-proxy`
   load-balances across all healthy pod replicas behind it. This is the
   production-grade version of the same idea — see the `env` values in
   `k8s/orders-service.yaml` and `k8s/api-gateway.yaml`.

In a larger or polyglot system you might instead use a dedicated service
registry (Consul, Eureka) with client-side discovery, but for services
already running in Docker/Kubernetes, platform-native DNS-based discovery
(what's used here) is the most common and simplest approach.

## Microservices vs. Monolith

| | Monolith | Microservices (this project) |
|---|---|---|
| Deployment | One deployable unit | Each service (Catalog, Orders, Gateway) deploys independently |
| Data | Usually one shared database | Each service owns its data — Catalog's `CatalogDbContext` is invisible to Orders |
| Scaling | Scale the whole app together | Scale `orders-service` replicas independently of `catalog-service` (see `replicas:` in the k8s manifests) |
| Technology | Single stack | Each service could use a different language/framework — OrdersService only needs Catalog's HTTP contract, not its code |
| Failure isolation | One bug can bring down everything | A CatalogService outage degrades OrdersService (it can't create new orders) but doesn't crash it — this is exactly what `CatalogServiceHealthCheck` surfaces |
| Complexity | Simpler to build and reason about initially | More moving parts: network calls, service discovery, distributed monitoring, eventual consistency |

## Notes

- I don't have Docker, Kubernetes, or the .NET SDK available in this
  sandbox, so I couldn't build the images or run `docker compose up`
  myself. I validated every `.cs` file's brace/paren balance and every
  JSON/YAML file's syntax by parsing them programmatically, but please
  run this end-to-end on your machine and let me know if anything needs
  adjusting.
- `OrdersService` stores orders in a static in-memory list (like the
  earlier modules) purely to keep the demo dependency-free; a real
  service would use its own database (EF Core + SQL Server, as in
  Module 5), separate from Catalog's.
- The `your-registry/...` image names in the Kubernetes manifests and the
  `your-org/...` namespace in the GitHub Actions workflow are placeholders
  — update them to your actual container registry before deploying.
