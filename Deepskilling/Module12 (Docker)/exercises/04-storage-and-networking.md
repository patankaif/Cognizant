# Exercise 4 — Storage and Networking

## Part A — prove a named volume survives container recreation

```bash
docker volume create demo-data

docker run --rm -v demo-data:/data alpine sh -c "echo 'hello from container 1' > /data/message.txt"

docker run --rm -v demo-data:/data alpine cat /data/message.txt
```

The second container is a completely different container (note `--rm` —
the first one is fully gone by the time the second one starts), yet it
can read the file the first one wrote. The data lived in the **volume**,
not in either container.

Now prove the opposite — that a container's own filesystem does *not*
survive:
```bash
docker run --rm alpine sh -c "echo 'this will vanish' > /tmp/gone.txt && cat /tmp/gone.txt"
docker run --rm alpine cat /tmp/gone.txt
```
The second command should fail (`No such file or directory`) — nothing
was mounted, so nothing persisted between the two separate containers.

Clean up:
```bash
docker volume rm demo-data
```

## Part B — bind mount for local development

```bash
mkdir bind-demo && echo "v1" > bind-demo/version.txt
docker run --rm -v "$(pwd)/bind-demo:/data" alpine cat /data/version.txt
echo "v2" > bind-demo/version.txt   # edit the HOST file directly
docker run --rm -v "$(pwd)/bind-demo:/data" alpine cat /data/version.txt
```
Notice the container saw the edit immediately — a bind mount is a live
window into a specific host path, not a copy.

## Part C — two containers finding each other by name on a shared network

```bash
docker network create demo-network

docker run -d --name demo-web --network demo-network nginx:alpine

docker run --rm --network demo-network alpine sh -c "apk add --no-cache curl -q && curl -s http://demo-web"
```
The second container reached the first **by container name**
(`demo-web`), with no IP address anywhere in the command — that's
Docker's embedded DNS on the custom network at work. This is exactly the
mechanism Module 7's microservices relied on
(`http://catalog-service:8080`).

Now prove it *doesn't* work without a shared network:
```bash
docker run -d --name demo-web-2 nginx:alpine        # no --network, uses the default bridge
docker run --rm alpine sh -c "apk add --no-cache curl -q && curl -s --max-time 3 http://demo-web-2"
```
This should time out or fail to resolve — two containers on the default
bridge network, started without an explicit shared user-defined network,
cannot reliably resolve each other by name (the legacy default bridge
network doesn't provide automatic DNS between containers the way a
user-defined network does).

Clean up:
```bash
docker rm -f demo-web demo-web-2
docker network rm demo-network
```

## Check yourself

- In your own words: why does a named volume survive `docker rm`, but a
  container's own writable layer doesn't?
- Why did the two containers on a *custom* network resolve each other by
  name, but the ones on the *default* bridge network didn't?

## Stretch goal

Run `docker network inspect demo-network` (before cleaning it up) and
find the IP addresses Docker assigned each container — confirm they're
on the same subnet, and that this matches what `docker inspect
demo-web` shows under `NetworkSettings`.
