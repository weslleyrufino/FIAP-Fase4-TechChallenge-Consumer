# Etapa 1 - build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas os arquivos de projeto para restaurar as dependências
COPY ["GestorContatos.WorkerService/GestorContatos.WorkerService.csproj", "GestorContatos.WorkerService/"]
COPY ["GestorContatos.Core/GestorContatos.Core.csproj", "GestorContatos.Core/"]
RUN dotnet restore "GestorContatos.WorkerService/GestorContatos.WorkerService.csproj"

# Copia o restante do código-fonte
COPY . .

# Publica o projeto
WORKDIR "/src/GestorContatos.WorkerService"
RUN dotnet publish -c Release -o /app/publish

# Etapa 2 - runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GestorContatos.WorkerService.dll"]
