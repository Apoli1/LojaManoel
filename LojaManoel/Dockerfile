# Est�gio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia o arquivo de projeto e restaura as depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copia todo o c�digo-fonte
COPY . ./
RUN dotnet publish -c Release -o out

# Est�gio de execu��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Exp�e a porta que sua API usa (verifique launchSettings.json para as portas HTTPS/HTTP)
# Geralmente � 80 para HTTP e 443 para HTTPS dentro do cont�iner
EXPOSE 80
EXPOSE 443

# Define o ponto de entrada da aplica��o
ENTRYPOINT ["dotnet", "LojaManoel.dll"]