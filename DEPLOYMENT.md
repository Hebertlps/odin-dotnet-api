# 🚀 Guia de Deployment - O.D.I.N. API .NET

Instruções para fazer deploy da API em diferentes ambientes.

---

## 📋 Pré-requisitos

- .NET SDK 8.0+
- Git
- Conta em plataforma de hosting (Render, Azure, AWS, etc.)
- SQL Server (local ou cloud)

---

## 🏠 Deployment Local

### Executar em Desenvolvimento

```bash
# 1. Clonar repositório
git clone https://github.com/Hebertlps/odin-dotnet-api.git
cd odin-dotnet-api

# 2. Restaurar dependências
dotnet restore

# 3. Aplicar migrations
dotnet ef database update

# 4. Executar em desenvolvimento
dotnet run

# A API estará em: https://localhost:5001
```

### Executar em Produção (Local)

```bash
# 1. Build para produção
dotnet publish -c Release -o ./publish

# 2. Executar
dotnet ./publish/OdinApi.dll

# A API estará em: https://localhost:5001
```

---

## ☁️ Deployment em Render

### Passos

1. **Criar conta em Render.com**
   - Acessar https://render.com
   - Fazer login com GitHub

2. **Conectar Repositório**
   - Clicar em "New +"
   - Selecionar "Web Service"
   - Conectar repositório GitHub

3. **Configurar Serviço**

| Campo | Valor |
|-------|-------|
| **Name** | odin-dotnet-api |
| **Environment** | .NET |
| **Build Command** | `dotnet publish -c Release -o ./publish` |
| **Start Command** | `dotnet ./publish/OdinApi.dll` |

4. **Variáveis de Ambiente**

```
ConnectionStrings__DefaultConnection=Server=seu-servidor;Database=OdinDb;User Id=usuario;Password=senha;
ASPNETCORE_ENVIRONMENT=Production
```

5. **Deploy**
   - Clicar em "Create Web Service"
   - Aguardar build e deployment

---

## 🔷 Deployment em Azure

### Passos

1. **Criar Conta Azure**
   - Acessar https://azure.microsoft.com
   - Criar conta gratuita

2. **Criar App Service**

```bash
# Login no Azure
az login

# Criar resource group
az group create --name odin-rg --location eastus

# Criar App Service Plan
az appservice plan create --name odin-plan \
  --resource-group odin-rg \
  --sku B1 --is-linux

# Criar Web App
az webapp create --resource-group odin-rg \
  --plan odin-plan \
  --name odin-dotnet-api \
  --runtime "DOTNETCORE|8.0"
```

3. **Deploy com Git**

```bash
# Configurar Git remoto
git remote add azure https://odin-dotnet-api.scm.azurewebsites.net/odin-dotnet-api.git

# Push para Azure
git push azure main
```

4. **Configurar Banco de Dados**

```bash
# Criar SQL Server
az sql server create --resource-group odin-rg \
  --name odin-server \
  --admin-user adminuser \
  --admin-password SenhaForte123!

# Criar banco de dados
az sql db create --resource-group odin-rg \
  --server odin-server \
  --name OdinDb
```

5. **Configurar Connection String**

```bash
az webapp config appsettings set \
  --resource-group odin-rg \
  --name odin-dotnet-api \
  --settings "ConnectionStrings__DefaultConnection=Server=odin-server.database.windows.net;Database=OdinDb;User Id=adminuser;Password=SenhaForte123!;"
```

---

## 🐳 Deployment com Docker

### Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["OdinApi.csproj", "./"]
RUN dotnet restore "OdinApi.csproj"

COPY . .
RUN dotnet build "OdinApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OdinApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5001
ENV ASPNETCORE_URLS=https://+:5001
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "OdinApi.dll"]
```

### Executar com Docker

```bash
# Build imagem
docker build -t odin-dotnet-api:latest .

# Executar container
docker run -d \
  -p 5001:5001 \
  -e "ConnectionStrings__DefaultConnection=Server=host.docker.internal;Database=OdinDb;Trusted_Connection=true;" \
  --name odin-api \
  odin-dotnet-api:latest

# Verificar logs
docker logs -f odin-api
```

### Docker Compose

```yaml
version: '3.8'

services:
  api:
    build: .
    ports:
      - "5001:5001"
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=OdinDb;User Id=sa;Password=SenhaForte123!;
      - ASPNETCORE_ENVIRONMENT=Production
    depends_on:
      - db

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - SA_PASSWORD=SenhaForte123!
      - ACCEPT_EULA=Y
    ports:
      - "1433:1433"
    volumes:
      - sqldata:/var/opt/mssql

volumes:
  sqldata:
```

Executar:
```bash
docker-compose up -d
```

---

## 🔒 Configurações de Produção

### appsettings.Production.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=seu-servidor;Database=OdinDb;User Id=usuario;Password=senha;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft": "Warning"
    }
  },
  "AllowedHosts": "*.odin.com"
}
```

### Variáveis de Ambiente Recomendadas

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=https://+:5001
ConnectionStrings__DefaultConnection=<string-de-conexao>
```

---

## 📊 Monitoramento

### Application Insights (Azure)

```csharp
// Program.cs
builder.Services.AddApplicationInsightsTelemetry();
```

### Logs

```bash
# Ver logs em produção
dotnet run --configuration Release --verbosity detailed
```

---

## 🔄 CI/CD com GitHub Actions

### .github/workflows/deploy.yml

```yaml
name: Deploy

on:
  push:
    branches: [main]

jobs:
  deploy:
    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v2
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: '8.0.x'
    
    - name: Restore
      run: dotnet restore
    
    - name: Build
      run: dotnet build --configuration Release --no-restore
    
    - name: Test
      run: dotnet test --configuration Release --no-build
    
    - name: Publish
      run: dotnet publish -c Release -o ./publish
    
    - name: Deploy to Render
      run: |
        curl -X POST https://api.render.com/deploy/srv-xxx \
          -H "Authorization: Bearer ${{ secrets.RENDER_API_KEY }}"
```

---

## 🚨 Troubleshooting

### Erro: "Unable to connect to database"

```bash
# Verificar connection string
# Verificar se SQL Server está acessível
# Executar migrations novamente
dotnet ef database update
```

### Erro: "Port 5001 already in use"

```bash
# Usar porta diferente
dotnet run --urls "https://localhost:5002"
```

### Erro: "SSL certificate error"

```bash
# Gerar certificado auto-assinado
dotnet dev-certs https --trust
```

---

## ✅ Checklist de Deployment

- [ ] Código está no GitHub
- [ ] Todas as migrations foram aplicadas
- [ ] Variáveis de ambiente estão configuradas
- [ ] Connection string está correta
- [ ] Certificado SSL está configurado
- [ ] Logs estão sendo registrados
- [ ] Monitoramento está ativo
- [ ] Backup do banco de dados está configurado
- [ ] API está respondendo em produção
- [ ] Swagger está acessível

---

**Última atualização:** 06/06/2026
