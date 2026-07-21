# Module 12 – Containerization using Docker

Covers every learning objective in **Module 12** of the DN 5.0 Deep
Skilling handbook: Docker concepts and features, basic commands, and
storage/networking/orchestration concepts — paired with real Dockerfiles
for the .NET (Module 6/7 pattern) and Angular (Module 8) projects built
earlier in this program.

I don't have Docker available in this sandbox, so none of the commands
below were executed here — see the Notes section at the end for exactly
what was and wasn't possible to verify, and how I compensated (checking
the real build output paths the Dockerfiles depend on, for instance).

## 1. What is Docker?

Docker packages an application together with everything it needs to
run — code, runtime, system libraries, configuration — into a single,
portable unit called a **container**. The core idea it solves: "it works
on my machine" stops being a problem, because the container *is* the
machine, as far as the application is concerned, wherever it runs.

**Containers vs. virtual machines** — the distinction that trips people
up most:

| | Virtual Machine | Container |
|---|---|---|
| Virtualizes | Hardware (runs a full guest OS) | The operating system (shares the host's kernel) |
| Startup time | Minutes | Seconds (often sub-second) |
| Size | GBs (full OS included) | MBs–low GBs (just the app + its dependencies) |
| Isolation | Very strong (separate kernel) | Strong, but shares the host kernel |
| Density | Fewer per host | Many more per host |

A container is **not** a lightweight VM — it's a regular process on the
host, just isolated using Linux kernel features (namespaces for
isolation, cgroups for resource limits) so it *behaves* like it has the
machine to itself.

**Key Docker features/concepts:**
- **Image** — a read-only template (built from a `Dockerfile`) containing
  the application and everything it needs.
- **Container** — a running (or stopped) instance of an image — the same
  relationship as a class and an object.
- **Dockerfile** — a text file of instructions describing how to build an
  image, layer by layer.
- **Registry** — a place images are stored and pulled from (Docker Hub is
  the public default; most teams also use a private registry like
  GitHub Container Registry, Azure Container Registry, or AWS ECR).
- **Docker Engine** — the background service (daemon) that actually
  builds images and runs containers; see §5.

## 2. Basic Docker Commands

```bash
docker pull <image>[:<tag>]        # download an image from a registry
docker images                      # list images stored locally
docker rmi <image>                 # remove a local image
docker ps                          # list running containers
docker ps -a                       # list ALL containers, including stopped ones
docker run <image>                 # create and start a new container from an image
docker start <container>           # start an existing (stopped) container
docker stop <container>            # gracefully stop a running container
docker rm <container>              # remove a stopped container
docker exec -it <container> <cmd>  # run a command inside an already-running container
docker logs <container>            # view a container's stdout/stderr
docker inspect <container|image>   # full JSON details about a container or image
```

A concrete sequence, from nothing to a running, inspectable container:
```bash
docker pull nginx:alpine
docker images                      # confirm it's there locally now
docker run -d --name my-nginx -p 8081:80 nginx:alpine
docker ps                          # confirm it's running, mapped to localhost:8081
docker exec -it my-nginx sh        # get a shell inside the running container
docker logs my-nginx
docker stop my-nginx
docker rm my-nginx
```

## 3. `docker run` in depth

`docker run` is really "pull if needed, create a container, and start
it" in one command. The flags you'll use constantly:

```bash
docker run <image>                       # run in the foreground, attached to your terminal
docker run -d <image>                    # detached — run in the background, return immediately
docker run --name my-container <image>   # give it a memorable name instead of a random one
docker run -it <image> sh                # interactive + a pseudo-TTY — for a shell session
docker run -p 8080:80 <image>            # publish port 80 in the container as 8080 on the host
docker run -e KEY=value <image>          # set an environment variable inside the container
docker run -v myvolume:/data <image>     # mount a named volume at /data — see §7
docker run --rm <image>                  # automatically remove the container when it exits
docker run --network my-network <image>  # attach to a specific network — see §8
```

Combined, a realistic one-liner:
```bash
docker run -d --name catalog-api --rm \
  -p 8080:8080 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  --network app-network \
  your-registry/catalog-api:latest
```

## 4. Docker Images and Dockerfiles

**Image layers** — every instruction in a Dockerfile (`FROM`, `RUN`,
`COPY`, etc.) creates a new, cached, read-only **layer**. Layers are
stacked on top of each other and shared between images where possible —
this is why pulling a second image that shares a base with one you
already have is fast (Docker only downloads the layers it doesn't
already have), and why *layer ordering* in a Dockerfile matters for
build speed (put things that change rarely, like dependency
restoration, before things that change often, like your source code, so
Docker can reuse the cached layer for the rare-change steps).

**Two real, complete Dockerfiles** live in `dockerfiles/` in this module —
both are genuine **multi-stage builds**, the standard pattern for
compiled/bundled apps: one stage has the full SDK/toolchain and builds
the app, a second, much smaller stage only has the runtime and the
already-built output. This keeps the final image small (no compiler,
no source code, no build-time dependencies in the shipped image).

**`dockerfiles/dotnet-web-api/Dockerfile`** (same pattern used for every
.NET service in Module 7):
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY CatalogService.csproj .
RUN dotnet restore CatalogService.csproj
COPY . .
RUN dotnet publish CatalogService.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "CatalogService.dll"]
```
Notice `COPY CatalogService.csproj .` and `dotnet restore` happen
*before* `COPY . .` (the rest of the source) — this means editing a
`.cs` file and rebuilding won't invalidate the (slow) restore layer,
only the (fast) publish layer, as long as the `.csproj` itself didn't
change.

**`dockerfiles/angular-spa/Dockerfile`** (new for this module — a
Node build stage, then an Nginx stage that only ships the static output):
```dockerfile
FROM node:22 AS build
WORKDIR /src
COPY package.json package-lock.json ./
RUN npm ci
COPY . .
RUN npx ng build --configuration production

FROM nginx:1.27-alpine AS final
COPY --from=build /src/dist/module8-angular/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
```
I confirmed `dist/module8-angular/browser` is the exact output path by
actually running `npx ng build --configuration production` against the
real Module 8 project in this sandbox — Angular's newer "application"
builder nests client output under a `browser/` subfolder even without
SSR enabled, which is easy to get wrong if you're used to older Angular
versions that output straight into `dist/<project-name>/`.
`nginx.conf` (in the same folder) adds the one thing a Single Page
Application needs that Nginx doesn't do by default: falling back to
`index.html` for any path Angular's router owns, via
`try_files $uri $uri/ /index.html;`.

**Building and tagging:**
```bash
docker build -t your-registry/catalog-api:1.0.0 dockerfiles/dotnet-web-api
docker build -t your-registry/catalog-frontend:1.0.0 dockerfiles/angular-spa
docker history your-registry/catalog-api:1.0.0    # see every layer and its size
```

## 5. Docker Compose

For anything with more than one container, hand-typing `docker run`
commands gets old fast — **Docker Compose** describes a whole multi-container
application (services, networks, volumes) in one YAML file.

```bash
docker compose up -d          # build (if needed) and start every service
docker compose ps             # list this project's containers
docker compose logs -f api    # follow logs for one service
docker compose down           # stop and remove containers (keeps named volumes by default)
docker compose down -v        # also remove named volumes — careful, this deletes data
```

`compose/docker-compose.yml` in this module ties together three services
— a SQL Server database, a .NET API, and the Angular frontend — and is
specifically written to also demonstrate storage (§7) and networking
(§8), covered next. (The service `build.context` paths there point at
this module's example Dockerfiles, which don't include real application
source — point them at an actual project directory, such as
`Module7-MicroservicesArchitecture/src/CatalogService`, to actually build
and run it. Module 7's own `docker-compose.yml` is a fully working,
ready-to-run reference if you want to see this pattern actually build
end to end.)

## 6. Docker Engine

**Docker Engine** is the client-server application that makes all of
this work:
- **Docker daemon (`dockerd`)** — a background service that does the
  real work: building images, running/stopping containers, managing
  networks and volumes.
- **REST API** — the daemon exposes an API; every client talks to the
  daemon through it, not directly to containers.
- **Docker CLI (`docker`)** — the command-line client you've been
  using in every example above; it's a thin client that sends requests
  to the daemon's API and prints the response.

This client-server split is why `docker` commands work identically
whether the daemon is running on your laptop, a remote build server, or
inside CI — the CLI just needs to know which daemon to talk to.

## 7. Docker Storage

Containers are meant to be disposable — delete one and recreate it, and
by default **everything written inside its filesystem is gone**. That's
fine for a stateless API, but not for a database. Docker's storage
options solve this:

- **Named volumes** — storage managed by Docker itself, living outside
  any single container's filesystem. Survives `docker rm` of the
  container that used it. The right choice for most persistent data
  (databases, uploaded files).
  ```bash
  docker volume create my-data
  docker volume ls
  docker volume inspect my-data
  docker run -v my-data:/var/opt/mssql mcr.microsoft.com/mssql/server
  ```
- **Bind mounts** — map a specific path on the *host* machine directly
  into the container. Useful for local development (e.g. mounting your
  source code so a container sees live edits) but ties you to that
  exact host path, which usually makes it a poor choice for production.
  ```bash
  docker run -v /home/me/project:/app my-image
  ```
- **`--rm` containers with no volume** — fine for genuinely stateless,
  disposable work (a one-off build step, a short-lived test run).

`compose/docker-compose.yml` uses a named volume for exactly this
reason:
```yaml
services:
  sqlserver:
    volumes:
      - sqlserver-data:/var/opt/mssql   # named volume, survives container recreation
volumes:
  sqlserver-data:
    driver: local
```
Run `docker compose down` (without `-v`) and recreate the `sqlserver`
service — the volume, and everything in the database, survives. Add
`-v` and it's gone. That distinction is worth internalizing early;
losing a volume by accident is a very common real-world mistake.

## 8. Docker Networking

By default, Docker creates a **bridge network** that containers can be
attached to, giving each container its own IP address on that network
and — critically — letting containers reach each other **by container
name**, acting as automatic DNS. This is the same mechanism Module 7's
microservices used to find each other (`http://catalog-service:8080`).

```bash
docker network ls                        # list networks (bridge, host, none, and any custom ones)
docker network create app-network        # create a custom bridge network
docker network inspect app-network       # see what's attached to it, and their IPs
docker run --network app-network ...     # attach a container to it at creation time
```

In `compose/docker-compose.yml`, all three services share one custom
network:
```yaml
networks:
  app-network:
    driver: bridge
```
and each service lists `networks: [app-network]`. Because of this, the
`api` service's connection string can simply say `Server=sqlserver` —
Docker's embedded DNS resolves that container name to the right IP
automatically, no hardcoded IP addresses or manual `/etc/hosts` editing
required. (Compose actually creates a default network for you even if
you don't declare one explicitly — declaring `app-network` here is done
for clarity and so it's easy to point other services at the same
network later, not because it's strictly required for two services in
the same compose file to see each other.)

## 9. Container Orchestration

Running one container by hand is easy. Running **hundreds**, across
**many machines**, keeping the right number of replicas alive, restarting
ones that crash, load-balancing traffic across them, and rolling out
updates without downtime — that's what **container orchestration**
solves.

**Why you need it eventually:** a single `docker run` (or even a single
Compose file on one host) has no answer for "what happens when this
machine dies", "how do I run 10 replicas of this service and spread load
across them", or "how do I deploy a new version with zero downtime."

**Docker vs. orchestration:** Docker builds and runs *individual*
containers. Kubernetes (or Docker Swarm, a simpler built-in alternative)
decides *where* those containers run, *how many* copies exist, restarts
them on failure, and exposes them as stable services — it's a layer on
top of, not a replacement for, the containers Docker builds.

**Kubernetes**, the dominant orchestrator today, was already used
hands-on in Module 7 (`k8s/*.yaml`) — the concepts map directly onto
what you just read:
- A Kubernetes **Pod** wraps one or more containers (usually one).
- A **Deployment** manages a set of identical Pod replicas, and handles
  rolling updates — see Module 7's `strategy: RollingUpdate` blocks.
- A Kubernetes **Service** gives a stable DNS name and load-balances
  across all healthy Pods behind it — the cluster-native version of the
  Docker bridge-network DNS trick from §8, just operating across an
  entire cluster of machines instead of one Docker host.

If you haven't already, Module 7's `README.md` "Service discovery"
section is worth re-reading now that this module has covered the Docker
fundamentals (images, storage, networking) that Kubernetes builds on top
of.

## Exercises

Hands-on versions of everything above live in `exercises/`:

1. `exercises/01-basic-commands.md` — pull, run, ps, exec, logs, stop, rm
2. `exercises/02-building-images.md` — write a Dockerfile, build it, tag
   it, inspect its layers
3. `exercises/03-docker-compose.md` — bring up the multi-service example,
   inspect it, tear it down
4. `exercises/04-storage-and-networking.md` — prove a named volume
   survives container recreation; prove two containers can reach each
   other by name on a shared network

See `cheatsheet.md` for a condensed, printable command reference.

## Notes

- I don't have Docker available in this sandbox, so I could not actually
  run any `docker`/`docker compose` command to verify these end to end.
  What I *could* verify, and did: I rebuilt the real Module 8 Angular
  project (`npm ci` + `npx ng build --configuration production`) to
  confirm the exact output path (`dist/module8-angular/browser`) the
  Angular Dockerfile's `COPY --from=build` step depends on — a detail
  that's easy to get wrong and would otherwise only surface as a
  confusing empty-website bug at container runtime. I also validated
  `compose/docker-compose.yml` with a YAML parser.
- `dockerfiles/dotnet-web-api/Dockerfile` is copied unchanged from
  Module 7's `CatalogService/Dockerfile`, which was already part of a
  project I fully built and verified compiles — reusing it here means
  this Module 12 example isn't a fresh, unverified guess.
- Please build and run these on a machine with Docker installed and let
  me know if anything needs adjusting — particularly the SQL Server
  healthcheck command in `compose/docker-compose.yml`, which I wrote
  from documentation rather than having a way to confirm it against a
  running SQL Server container here.
