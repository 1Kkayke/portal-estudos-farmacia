# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["PortalEstudos.API.csproj", "."]
RUN dotnet restore "PortalEstudos.API.csproj"

COPY . .
RUN dotnet build "PortalEstudos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PortalEstudos.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=publish /app/publish .
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "PortalEstudos.API.dll"]
