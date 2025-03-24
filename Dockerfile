FROM chatmessapi
# Instala dependências como root

USER root
RUN apt-get update && apt-get install -y curl 

# Volte para o usuário original
USER app


# Fase base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Fase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ChatMessAPI.csproj", "."]
RUN dotnet restore "./ChatMessAPI.csproj"
COPY . .
RUN dotnet build "ChatMessAPI.csproj" -c Release -o /app/build

# Fase de publicação
FROM build AS publish
RUN dotnet publish "ChatMessAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Fase final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatMessAPI.dll"]