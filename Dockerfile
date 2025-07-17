FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto e restaura as depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copia o restante dos arquivos e compila o projeto
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "TradeControl.dll"]

HEALTHCHECK --interval=45s --timeout=9s --start-period=15s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1
