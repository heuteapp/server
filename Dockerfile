# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Solution ve projeleri kopyala
COPY *.sln ./
COPY src/Api/HeuteApp.Api.csproj src/Api/
COPY src/Application/HeuteApp.Application.csproj src/Application/
COPY src/Core/HeuteApp.Core.csproj src/Core/
COPY src/Infrastructure/HeuteApp.Infrastructure.csproj src/Infrastructure/

# Bağımlılıkları restore et
RUN dotnet restore

# Tüm kaynak kodu kopyala
COPY ./src ./src

# Build ve publish
RUN dotnet publish src/Api/HeuteApp.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Publish edilen uygulamayı al
COPY --from=build /app/publish .

# Fly.io default port
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
EXPOSE 8080

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "HeuteApp.Api.dll"]