# Etapa 1: Construcción (SDK .NET 9)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia todo el contenido del backend (incluyendo .Api, .Application, .Infrastructure)
COPY . .

# Restaura las dependencias apuntando al proyecto API dentro de su carpeta
RUN dotnet restore "Viamatica.Api/Viamatica.Api.csproj"

# Compila y publica la API
RUN dotnet publish "Viamatica.Api/Viamatica.Api.csproj" -c Release -o /app/publish

# Etapa 2: Ejecución (ASP.NET 9)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Viamatica.Api.dll"]