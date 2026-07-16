# syntax=docker/dockerfile:1

# ---------- Build ----------
# Le contexte de build est la RACINE de la solution (voir docker-compose.yml).
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1) Copie des csproj pour profiter du cache de restauration
COPY GestionnaireBibliotheque.slnx ./
COPY src/GestionnaireBibliotheque.Domain/GestionnaireBibliotheque.Domain.csproj                 src/GestionnaireBibliotheque.Domain/
COPY src/GestionnaireBibliotheque.Application/GestionnaireBibliotheque.Application.csproj       src/GestionnaireBibliotheque.Application/
COPY src/GestionnaireBibliotheque.Infrastructure/GestionnaireBibliotheque.Infrastructure.csproj src/GestionnaireBibliotheque.Infrastructure/
COPY src/GestionnaireBibliotheque.API/GestionnaireBibliotheque.API.csproj                       src/GestionnaireBibliotheque.API/
RUN dotnet restore src/GestionnaireBibliotheque.API/GestionnaireBibliotheque.API.csproj

# 2) Copie du reste du code et publication
COPY . .
RUN dotnet publish src/GestionnaireBibliotheque.API/GestionnaireBibliotheque.API.csproj \
    -c Release -o /app/publish /p:UseAppHost=false

# ---------- Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GestionnaireBibliotheque.API.dll"]
