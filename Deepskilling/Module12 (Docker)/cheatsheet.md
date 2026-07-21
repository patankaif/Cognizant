# Docker Cheatsheet

## Images
```bash
docker pull <image>[:tag]        # download from a registry
docker images                    # list local images
docker rmi <image>               # remove a local image
docker build -t <name>:<tag> .   # build an image from a Dockerfile in the current directory
docker history <image>           # show layers and their sizes
docker tag <image> <new-name>    # add another tag to an existing image
```

## Containers
```bash
docker run <image>                        # create + start (foreground)
docker run -d <image>                      # create + start (detached)
docker run --name <n> -p <host>:<container> <image>   # named + port-mapped
docker run -it <image> sh                  # interactive shell
docker run -v <volume>:<path> <image>      # with a volume
docker run --network <net> <image>         # on a specific network
docker run --rm <image>                    # auto-remove on exit

docker ps                       # list running containers
docker ps -a                    # list all containers, including stopped
docker start <container>        # start a stopped container
docker stop <container>         # gracefully stop
docker restart <container>      # stop + start
docker rm <container>           # remove a stopped container
docker rm -f <container>        # force-remove, even if running

docker exec -it <container> sh  # shell into a running container
docker logs <container>         # view logs
docker logs -f <container>      # follow logs live
docker inspect <container>      # full JSON details
```

## Volumes
```bash
docker volume create <name>
docker volume ls
docker volume inspect <name>
docker volume rm <name>
```

## Networks
```bash
docker network create <name>
docker network ls
docker network inspect <name>
docker network rm <name>
```

## Docker Compose
```bash
docker compose up -d              # build (if needed) + start everything
docker compose up -d --build      # force a rebuild first
docker compose ps                 # list this project's containers
docker compose logs -f <service>  # follow logs for one service
docker compose down               # stop + remove containers (keeps named volumes)
docker compose down -v            # also remove named volumes
docker compose exec <service> sh  # shell into a running compose service
```

## Cleanup
```bash
docker container prune   # remove all stopped containers
docker image prune       # remove dangling (untagged) images
docker volume prune      # remove unused volumes
docker system prune      # remove all of the above at once
docker system prune -a   # ...and also unused (not just dangling) images
```
