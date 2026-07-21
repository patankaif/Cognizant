# Module 12 – Containerization using Docker

Covers every learning objective in **Module 12** of the DN 5.0 Deep
Skilling handbook: Docker concepts and features, basic Docker commands,
and Docker storage/networking/orchestration.

**Start with `DOCKER_GUIDE.md`** — that's the main content. It's paired
with two real, multi-stage Dockerfiles (one for a .NET Web API, reused
from the already-verified Module 7 project; one new one for the Module 8
Angular app, built on top of Nginx) and a Docker Compose example that
specifically demonstrates named-volume persistence and container-to-container
networking by name.

## Contents

```
DOCKER_GUIDE.md                    main guide — concepts, commands, storage, networking, orchestration
cheatsheet.md                      condensed, printable command reference
dockerfiles/
  dotnet-web-api/Dockerfile        multi-stage .NET build, reused from Module 7 (already verified there)
  angular-spa/
    Dockerfile                    multi-stage: Node build stage -> Nginx serve stage
    nginx.conf                    SPA routing fallback (try_files ... /index.html)
    .dockerignore                 keeps node_modules out of the build context
compose/
  docker-compose.yml               SQL Server + API + frontend, named volume, custom bridge network
exercises/
  01-basic-commands.md
  02-building-images.md
  03-docker-compose.md
  04-storage-and-networking.md
```

## Verification

I don't have Docker installed in this sandbox, so no `docker` command
itself was run here. What I did verify:

- Rebuilt the real Module 8 Angular project (`npm ci` +
  `npx ng build --configuration production`) to confirm the exact output
  path (`dist/module8-angular/browser`) that the Angular Dockerfile's
  `COPY --from=build` step depends on.
- `dockerfiles/dotnet-web-api/Dockerfile` is copied unchanged from
  Module 7's `CatalogService/Dockerfile`, which was already part of a
  project verified to compile.
- `compose/docker-compose.yml` was validated with a YAML parser.

Please run these on a machine with Docker installed and let me know if
anything needs adjusting — particularly the SQL Server healthcheck
command, which I wrote from documentation without a way to test it here.
