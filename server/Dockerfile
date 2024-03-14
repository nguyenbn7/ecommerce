# syntax = docker/dockerfile:1.2

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5078

ENV ASPNETCORE_URLS=http://+:5078

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Ecommerce.csproj", "./"]
RUN dotnet restore "Ecommerce.csproj"
COPY . .

# Render only
RUN --mount=type=secret,id=config_json,dst=/etc/secrets/config.json
RUN cp /etc/secrets/config.json ./appsettings.Production.json
RUN chown app ./appsettings.Production.json
# Render only

WORKDIR "/src/."
RUN dotnet build "Ecommerce.csproj" -c $configuration -o /app/build

USER app
FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Ecommerce.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.dll"]
