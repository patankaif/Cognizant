# Exercise 2 — Building Images

## Setup

```bash
mkdir docker-exercise-2 && cd docker-exercise-2
```

## Tasks

1. Create a minimal static site:
   ```bash
   mkdir site
   echo "<h1>Hello from my own image</h1>" > site/index.html
   ```

2. Write a `Dockerfile` (single-stage, to start simple):
   ```dockerfile
   FROM nginx:1.27-alpine
   COPY site/ /usr/share/nginx/html
   EXPOSE 80
   ```

3. Build and tag it:
   ```bash
   docker build -t my-site:1.0 .
   docker images | grep my-site
   ```

4. Run it and confirm it serves your file:
   ```bash
   docker run -d --name my-site -p 8083:80 my-site:1.0
   curl http://localhost:8083
   ```

5. Change `site/index.html`, then rebuild:
   ```bash
   docker build -t my-site:1.1 .
   docker history my-site:1.1
   ```
   Look at `docker history` output — notice each Dockerfile instruction
   became its own layer, with its own size.

6. Stop and remove the container, run a fresh one from the new tag:
   ```bash
   docker stop my-site && docker rm my-site
   docker run -d --name my-site -p 8083:80 my-site:1.1
   curl http://localhost:8083
   ```

## Part B — multi-stage build (the pattern used everywhere in this program)

Look at `dockerfiles/angular-spa/Dockerfile` in this module. It's two
`FROM` statements: a `build` stage with the full Node toolchain, and a
`final` stage that only has Nginx plus the already-built static files
copied from the first stage via `COPY --from=build`.

1. Build it for real, if you have a copy of the Module 8 Angular project
   available:
   ```bash
   cd Module8-Angular
   docker build -f ../Module12-Docker/dockerfiles/angular-spa/Dockerfile \
                -t catalog-frontend:1.0 .
   ```
2. Compare `docker images` sizes: how much smaller is the final image
   than it would be if it *also* shipped Node, npm, and every
   `node_modules` package? (You can estimate this by running
   `docker run --rm node:22 du -sh /` vs. the final `nginx:1.27-alpine`
   base — the difference is the point of a multi-stage build.)

## Check yourself

- Why did rebuilding after only changing `site/index.html` in Part A
  still work correctly, but why would changing something *earlier* in
  the Dockerfile (like the base image tag) invalidate more of the cache?
- In the multi-stage build, explain in your own words why the final
  image doesn't contain Node.js at all, even though building the app
  absolutely required it.

## Stretch goal

Add a `.dockerignore` file (see `dockerfiles/angular-spa/.dockerignore`
for a real example) excluding `node_modules` from the build context, and
compare how long `docker build` takes with and without it. Sending a
huge `node_modules` folder to the Docker daemon as build context — even
though it's never used, since `npm ci` reinstalls everything anyway — is
one of the most common accidental slowdowns in real Docker builds.
