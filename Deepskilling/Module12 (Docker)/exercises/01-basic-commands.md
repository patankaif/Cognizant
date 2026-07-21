# Exercise 1 — Basic Docker Commands

Requires Docker installed and running (`docker --version` should print
something before you start).

## Tasks

1. Pull a small image and look at it:
   ```bash
   docker pull nginx:alpine
   docker images
   ```
   Note the `IMAGE ID`, `SIZE`, and `CREATED` columns.

2. Run it, detached, with a name and a port mapping:
   ```bash
   docker run -d --name my-nginx -p 8081:80 nginx:alpine
   docker ps
   ```
   Visit `http://localhost:8081` in a browser (or `curl` it) — you should
   see the default Nginx welcome page.

3. Get a shell *inside* the running container and look around:
   ```bash
   docker exec -it my-nginx sh
   ls /usr/share/nginx/html
   cat /etc/nginx/conf.d/default.conf
   exit
   ```

4. Check its logs:
   ```bash
   docker logs my-nginx
   ```
   Refresh the page in your browser a couple of times, then run
   `docker logs my-nginx` again — you should see new access log lines.

5. Stop it, then confirm it still exists (just not running):
   ```bash
   docker stop my-nginx
   docker ps           # empty (or doesn't show my-nginx)
   docker ps -a         # my-nginx shows up, status "Exited"
   ```

6. Start it again without recreating it, confirm it works, then remove it
   entirely:
   ```bash
   docker start my-nginx
   docker ps
   docker stop my-nginx
   docker rm my-nginx
   docker ps -a          # my-nginx is gone
   ```

7. Clean up the image too:
   ```bash
   docker rmi nginx:alpine
   ```

## Check yourself

- Explain the difference between `docker stop` and `docker rm` — which
  one can you undo with `docker start`, and which one is permanent?
- Explain why `docker ps` (no `-a`) didn't show `my-nginx` after you
  stopped it, but `docker images` still showed `nginx:alpine` after you
  removed the container.

## Stretch goal

Run a *second* container from the same image, with a different name and
a different host port (e.g. `-p 8082:80`), while the first is still
running. Confirm both work independently with `docker ps`. This is the
core idea behind running multiple replicas of the same service.
