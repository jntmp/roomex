# RoomEx Api :earth_americas::straight_ruler:

This is a sample Api to expose an endpoint to measure global distances by geo coordinates.

## Assumptions

I built this under the assumption that it would be a microservice, hosted in a cloud/k8s cluster.
Therefore:
* No need for HTTPS redirection. We could handle SSL termination at the API gateway, routing HTTP traffic to the backend service.
* Logs to console are fine. We can employ a separate fluentd type service to scrape console output, and ship them to Grafana.
* Exposed health check endpoint for k8s probes
* Exposed HTTP metrics to ship to Grafanam, for dashboards/alerts.
* Application is already dockerized, so CI setup would be minimal.

## Run with Docker
#### Build
```
docker build -t roomex-api -f Dockerfile .
```
#### Run
```
docker run -it -p 8123:80 --rm -e ASPNETCORE_ENVIRONMENT=Development roomex-api
```

#### Swagger
http://localhost:8123/swagger

## Run Without docker
```
dotnet run --project Api/Api.csproj 
```
#### Test
```
dotnet test
```
#### Swagger
http://localhost:5039/swagger

## Endpoints
#### Functional
```
/Distance?Start.Latitude=1&Start.Longitude=2&End.Latitude=3&End.Longitude=4&Locale=en-GB
```
#### Non-functional
```
/metrics
/health

```

#### Parameters

* Latitude must be between -90 and 90
* Longitude must be between -180 and 180
* Locale is optional, and defaults to `en-US` but can also be `en-GB` or `en-ZA`


