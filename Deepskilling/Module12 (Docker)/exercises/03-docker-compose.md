# Exercise 3 — Docker Compose

## Option A — the simplest possible multi-service example

## Setup

```bash
mkdir docker-exercise-3 && cd docker-exercise-3
cat > docker-compose.yml << 'EOF'
services:
  web:
    image: nginx:alpine
    ports:
      - "8084:80"
    depends_on:
      - api

  api:
    image: mcr.microsoft.com/dotnet/samples:aspnetapp
    ports:
      - "8085:8080"
EOF
```

## Tasks

1. Bring it up and check status:
   ```bash
   docker compose up -d
   docker compose ps
   ```
2. View logs for just one service:
   ```bash
   docker compose logs -f api
   ```
   (Ctrl+C to stop following.)
3. Confirm both are reachable:
   ```bash
   curl http://localhost:8084
   curl http://localhost:8085
   ```
4. Tear it down:
   ```bash
   docker compose down
   docker compose ps      # empty
   ```

## Option B — the real multi-service example from this module

`compose/docker-compose.yml` in this module wires together a SQL Server
database, a .NET API, and the Angular frontend, with a named volume and
a custom network. Its `build.context` paths point at this module's
example Dockerfiles (which don't include real application source) — to
actually run it, point those contexts at a real project, e.g.:

```yaml
api:
  build:
    context: ../../Module7-MicroservicesArchitecture/src/CatalogService
```

Then:
```bash
cd compose
docker compose up -d --build
docker compose ps
docker compose logs -f api
docker compose down
```

## Check yourself

- What's the difference between `depends_on` in Compose and actually
  waiting for a dependency to be *ready* (not just *started*)? Look at
  how `compose/docker-compose.yml` uses `depends_on: sqlserver: condition:
  service_healthy` together with a `healthcheck:` block — why is a plain
  `depends_on: [sqlserver]` (with no healthcheck) not quite good enough
  for a database?
- Run `docker compose down` (no `-v`) after Option B, then
  `docker compose up -d` again — is the database data still there? Now
  try `docker compose down -v` and bring it up again — what changed?

## Stretch goal

Scale a service to multiple replicas:
```bash
docker compose up -d --scale api=3
docker compose ps
```
Note that this requires the service to *not* have a fixed host port
mapping (or Compose will fail, since three containers can't all bind to
the same host port) — a good illustration of why real-world scaling
usually happens behind a load balancer or, more commonly, inside a
proper orchestrator like Kubernetes (§9 of the main guide) rather than
plain Compose.
