FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5078

ENV ASPNETCORE_URLS=http://+:5078

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Ecommerce.csproj", "./"]
RUN dotnet restore "Ecommerce.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Ecommerce.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Ecommerce.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
# Render only
RUN --mount=type=secret,id=appsettings_prod_json,dst=/etc/secrets/appsettings.Production.json
RUN ls /etc/secrets
RUN cat /etc/secrets/appsettings.Production.json
# RUN chown app appsettings.Production.json
USER app
ENTRYPOINT ["dotnet", "Ecommerce.dll"]
