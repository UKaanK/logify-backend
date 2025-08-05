# 1. Build aşaması: SDK imajı ile derleme
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Solution ve proje dosyalarını kopyala
COPY *.sln .
COPY logifly.api/*.csproj ./logifly.api/
COPY logifly.application/*.csproj ./logifly.application/
COPY logifly.domain/*.csproj ./logifly.domain/
COPY logifly.infrastructure/*.csproj ./logifly.infrastructure/
COPY logifly.persistence/*.csproj ./logifly.persistence/

# Bağımlılıkları indir
RUN dotnet restore

# Kaynak kodları kopyala
COPY . .

# API projesi için yayınla
WORKDIR /app/logifly.api
RUN dotnet publish -c Release -o out /p:UseAppHost=false

# 2. Runtime aşaması: Sadece runtime içeren küçük imaj
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Yayınlanan dosyaları build aşamasından kopyala
COPY --from=build /app/logifly.api/out ./

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "logifly.api.dll"]
