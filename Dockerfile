FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Environment build config arguments
ARG BUILD_ENV="Development"
ARG MODE="Debug"

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Test
RUN dotnet test
# Build and publish a release
RUN dotnet publish -c ${MODE} -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App

ENV ASPNETCORE_ENVIRONMENT=${BUILD_ENV}

COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "Api.dll"]
