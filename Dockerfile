FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# 1. Copiar archivos de proyecto (csproj) para restaurar dependencias
# Como no tienes carpeta src, buscamos las carpetas directamente en la raíz
COPY ["Toothy.API/Toothy.API.csproj", "Toothy.API/"]
COPY ["Toothy.Application/Toothy.Application.csproj", "Toothy.Application/"]
COPY ["Toothy.Domain/Toothy.Domain.csproj", "Toothy.Domain/"]
COPY ["Toothy.Infrastructure/Toothy.Infrastructure.csproj", "Toothy.Infrastructure/"]

# 2. Restaurar dependencias
RUN dotnet restore "Toothy.API/Toothy.API.csproj"

# 3. Copiar todo el resto del código fuente
COPY . .

# 4. Compilar en modo Release
WORKDIR "/app/Toothy.API"
RUN dotnet build "Toothy.API.csproj" -c Release -o /app/build

# 5. Publicar (Generar los archivos finales)
FROM build AS publish
RUN dotnet publish "Toothy.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ETAPA 2: Runtime (Imagen final ligera para ejecutar)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Toothy.API.dll"]