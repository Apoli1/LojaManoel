# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia o arquivo de projeto e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código-fonte
COPY . ./
RUN dotnet publish -c Release -o out

# Estágio de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expõe a porta que sua API usa (verifique launchSettings.json para as portas HTTPS/HTTP)
# Geralmente é 80 para HTTP e 443 para HTTPS dentro do contêiner
EXPOSE 80
EXPOSE 443

# Define o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "LojaManoel.dll"]