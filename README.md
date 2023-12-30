# roomex

Build
```
docker build -t roomex-api -f Dockerfile .
```

Run
```
docker run -it --rm roomex-api
```

CI Build
```
docker build \
	--build-arg BUILD_ENV=Production \
	--build-arg MODE=Release \
	-t roomex-api -f Dockerfile .
```