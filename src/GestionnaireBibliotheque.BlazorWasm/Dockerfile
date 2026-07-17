# syntax=docker/dockerfile:1

# ---------- Build ----------
# Le contexte de build est la RACINE de la solution (voir docker-compose.yml).
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1) csproj d'abord (cache de restauration) — le projet Blazor n'a aucune référence projet.
COPY src/GestionnaireBibliotheque.BlazorWasm/GestionnaireBibliotheque.BlazorWasm.csproj src/GestionnaireBibliotheque.BlazorWasm/
RUN dotnet restore src/GestionnaireBibliotheque.BlazorWasm/GestionnaireBibliotheque.BlazorWasm.csproj

# 2) Reste du code + publication du site WASM statique
COPY src/GestionnaireBibliotheque.BlazorWasm/ src/GestionnaireBibliotheque.BlazorWasm/
RUN dotnet publish src/GestionnaireBibliotheque.BlazorWasm/GestionnaireBibliotheque.BlazorWasm.csproj \
    -c Release -o /app/publish

# ---------- Runtime (nginx sert le WASM statique) ----------
FROM nginx:alpine AS final
# URL que le NAVIGATEUR doit utiliser pour joindre l'API (API publiée sur l'hôte).
ARG API_BASE_URL=http://localhost:8080/
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY src/GestionnaireBibliotheque.BlazorWasm/nginx.conf /etc/nginx/conf.d/default.conf
# Surcharge de la config front (ApiBaseUrl) pour le déploiement conteneur.
RUN printf '{\n  "ApiBaseUrl": "%s"\n}\n' "$API_BASE_URL" > /usr/share/nginx/html/appsettings.json
EXPOSE 80
