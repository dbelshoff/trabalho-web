# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar apenas o arquivo .csproj primeiro para otimizar o cache do Docker
COPY ["projetoX.csproj", "./"]
RUN dotnet restore "projetoX.csproj" --no-cache --force

# Copiar o restante dos arquivos
COPY . .
RUN dotnet build "projetoX.csproj" -c Release -o /app/build

# Etapa de publicação
FROM build AS publish
RUN dotnet publish "projetoX.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "projetoX.dll"]