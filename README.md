# O.D.I.N. API - .NET (Advanced Business Development)

## 📋 Visão Geral

**O.D.I.N. (Orbital Debris Identification Network)** é uma plataforma de monitoramento orbital desenvolvida em **ASP.NET Core** para gerenciar satélites, detritos espaciais, operadores e manobras evasivas. Esta é a implementação em **.NET** da disciplina **Advanced Business Development with .NET**.

---

## 🎯 Requisitos Atendidos

### ✅ Requisitos Técnicos (PDF)

| Requisito | Status | Detalhes |
|-----------|--------|----------|
| **API REST** | ✅ Completo | ASP.NET Core 8.0 com boas práticas |
| **Banco de Dados Relacional** | ✅ Completo | SQL Server com Entity Framework Core |
| **Relacionamentos (1:N e N:N)** | ✅ Completo | Operador→Satélites, Satélite→Manobras, Detrito→Alertas |
| **Migrations** | ✅ Completo | Versionamento automático do schema |
| **Documentação** | ✅ Completo | README com diagramas, desenvolvimento e testes |

---

## 🏗️ Arquitetura

### Padrão de Projeto: **Clean Architecture + Repository Pattern**

```
OdinApi/
├── Models/              # Entidades de domínio
│   ├── Operador.cs
│   ├── Satelite.cs
│   ├── Detrito.cs
│   ├── Manobra.cs
│   └── Alerta.cs
├── Data/                # Camada de persistência
│   ├── OdinDbContext.cs
│   └── Migrations/
├── Services/            # Lógica de negócio
│   ├── ISateliteService.cs
│   ├── SateliteService.cs
│   ├── IOperadorService.cs
│   ├── OperadorService.cs
│   ├── IDebitoService.cs
│   └── DebitoService.cs
├── Controllers/         # Endpoints da API
│   ├── SatelitesController.cs
│   ├── OperadoresController.cs
│   └── DetritosController.cs
├── Program.cs           # Configuração da aplicação
├── appsettings.json     # Configurações
└── OdinApi.csproj       # Definição do projeto
```

---

## 📊 Modelo de Dados

### Entidades e Relacionamentos

```
┌─────────────────────────────────────────────────────────────┐
│                        OPERADOR (1)                          │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ Id (PK)                                             │   │
│  │ Nome                                                │   │
│  │ Email                                               │   │
│  │ NivelAcesso (ADMIN, OPERADOR, USUARIO)             │   │
│  │ DataCriacao                                         │   │
│  └─────────────────────────────────────────────────────┘   │
│         │                    │                    │         │
│         │ 1:N                │ 1:N                │ 1:N     │
│         ▼                    ▼                    ▼         │
│  ┌────────────────┐  ┌────────────────┐  ┌────────────────┐│
│  │   SATELITE     │  │    DETRITO     │  │    MANOBRA     ││
│  ├────────────────┤  ├────────────────┤  ├────────────────┤│
│  │ Id (PK)        │  │ Id (PK)        │  │ Id (PK)        ││
│  │ Nome           │  │ Identificação  │  │ SateliteId (FK)││
│  │ Combustível    │  │ Latitude       │  │ OperadorId (FK)││
│  │ Status         │  │ Longitude      │  │ Tipo           ││
│  │ DataLançamento │  │ Altitude       │  │ Status         ││
│  │ OperadorId(FK) │  │ Velocidade     │  │ DataSolicitação││
│  └────────────────┘  │ NivelRisco     │  │ DataExecução   ││
│         │            │ OperadorId(FK) │  │ Combustível    ││
│         │            └────────────────┘  └────────────────┘│
│         │ 1:N              │ 1:N                            │
│         └──────────────────┼────────────────────────────────┤
│                            ▼                                │
│                      ┌────────────────┐                     │
│                      │     ALERTA     │                     │
│                      ├────────────────┤                     │
│                      │ Id (PK)        │                     │
│                      │ SateliteId(FK) │                     │
│                      │ DebitoId (FK)  │                     │
│                      │ Severidade     │                     │
│                      │ Mensagem       │                     │
│                      │ Status         │                     │
│                      │ DataCriação    │                     │
│                      │ DataResolução  │                     │
│                      └────────────────┘                     │
└─────────────────────────────────────────────────────────────┘
```

### Relacionamentos Implementados

| Relacionamento | Tipo | Descrição |
|---|---|---|
| **Operador → Satelites** | 1:N | Um operador gerencia múltiplos satélites |
| **Operador → Detritos** | 1:N | Um operador detecta múltiplos detritos |
| **Operador → Manobras** | 1:N | Um operador autoriza múltiplas manobras |
| **Satelite → Manobras** | 1:N | Um satélite executa múltiplas manobras |
| **Satelite → Alertas** | 1:N | Um satélite gera múltiplos alertas |
| **Detrito → Alertas** | 1:N | Um detrito gera múltiplos alertas |

---

## 🗄️ Migrations

### O que são Migrations?

Migrations são versionamentos automáticos do schema do banco de dados. Permitem rastrear mudanças na estrutura do banco ao longo do tempo.

### Migrations Implementadas

| Migration | Descrição |
|-----------|-----------|
| **20260606_InitialCreate** | Criação inicial de todas as tabelas, relacionamentos e seed de dados |

### Como Gerenciar Mudanças

Para adicionar uma nova coluna na tabela `Satelites`:

```bash
# 1. Modificar o modelo
# Editar Models/Satelite.cs

# 2. Criar nova migration
dotnet ef migrations add AddNovaColuna

# 3. Aplicar migration
dotnet ef database update
```

---

## 🚀 Como Executar

### Pré-requisitos

- **.NET SDK 8.0+** instalado
- **SQL Server** (local ou remoto)
- **Git**

### Passos de Instalação

```bash
# 1. Clonar o repositório
git clone https://github.com/Hebertlps/odin-dotnet-api.git
cd odin-dotnet-api

# 2. Restaurar dependências
dotnet restore

# 3. Configurar conexão com banco de dados
# Editar appsettings.json e ajustar a string de conexão

# 4. Aplicar migrations
dotnet ef database update

# 5. Executar a aplicação
dotnet run

# A API estará disponível em: https://localhost:5001
# Swagger UI: https://localhost:5001/swagger/index.html
```

---

## 📡 Endpoints da API

### Base URL

**Produção (Render):**
```
https://odin-dotnet-api.onrender.com/api/v1
```

**Local:**
```
https://localhost:5001/api/v1
```

### Satélites

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| **GET** | `/satelites` | Listar todos os satélites |
| **GET** | `/satelites/{id}` | Obter satélite por ID |
| **POST** | `/satelites` | Criar novo satélite |
| **PUT** | `/satelites/{id}` | Atualizar satélite |
| **DELETE** | `/satelites/{id}` | Deletar satélite |

### Operadores

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| **GET** | `/operadores` | Listar todos os operadores |
| **GET** | `/operadores/{id}` | Obter operador por ID |
| **POST** | `/operadores` | Criar novo operador |
| **PUT** | `/operadores/{id}` | Atualizar operador |
| **DELETE** | `/operadores/{id}` | Deletar operador |

### Detritos

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| **GET** | `/detritos` | Listar todos os detritos |
| **GET** | `/detritos/{id}` | Obter detrito por ID |
| **POST** | `/detritos` | Criar novo detrito |
| **PUT** | `/detritos/{id}` | Atualizar detrito |
| **DELETE** | `/detritos/{id}` | Deletar detrito |

---

## 🧪 Exemplos de Testes

### 1. Listar Satélites

```bash
curl -X GET "https://localhost:5001/api/v1/satelites" \
  -H "accept: application/json"
```

**Resposta (200 OK):**
```json
[
  {
    "id": 1,
    "nome": "INSAT-3D",
    "combustivelAtual": 500,
    "statusOperacional": "ATIVO",
    "dataLancamento": "2013-09-26T00:00:00",
    "operadorId": 1,
    "operador": {
      "id": 1,
      "nome": "Marcus Vinícius",
      "email": "marcus@odin.local",
      "nivelAcesso": "ADMIN"
    }
  }
]
```

### 2. Criar Novo Satélite

```bash
curl -X POST "https://localhost:5001/api/v1/satelites" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "NOVO-SAT",
    "combustivelAtual": 400,
    "statusOperacional": "ATIVO",
    "dataLancamento": "2024-01-01T00:00:00",
    "operadorId": 1
  }'
```

**Resposta (201 Created):**
```json
{
  "id": 6,
  "nome": "NOVO-SAT",
  "combustivelAtual": 400,
  "statusOperacional": "ATIVO",
  "dataLancamento": "2024-01-01T00:00:00",
  "operadorId": 1
}
```

### 3. Atualizar Satélite

```bash
curl -X PUT "https://localhost:5001/api/v1/satelites/1" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "INSAT-3D-UPDATED",
    "combustivelAtual": 450,
    "statusOperacional": "MANUTENCAO",
    "dataLancamento": "2013-09-26T00:00:00",
    "operadorId": 1
  }'
```

### 4. Deletar Satélite

```bash
curl -X DELETE "https://localhost:5001/api/v1/satelites/6"
```

**Resposta (204 No Content)**

### 5. Listar Operadores

```bash
curl -X GET "https://localhost:5001/api/v1/operadores" \
  -H "accept: application/json"
```

### 6. Listar Detritos

```bash
curl -X GET "https://localhost:5001/api/v1/detritos" \
  -H "accept: application/json"
```

---

## 📝 Tratamento de Erros

A API implementa tratamento robusto de erros:

### Erro 404 - Não Encontrado

```json
{
  "message": "Satélite com ID 999 não encontrado"
}
```

### Erro 400 - Requisição Inválida

```json
{
  "errors": {
    "Nome": ["O campo Nome é obrigatório"],
    "CombustivelAtual": ["O combustível deve ser maior que 0"]
  }
}
```

### Erro 500 - Erro Interno

```json
{
  "message": "Erro interno do servidor"
}
```

---

## 🔍 Boas Práticas Implementadas

### 1. **Separação de Responsabilidades**
- **Controllers:** Recebem requisições e retornam respostas
- **Services:** Contêm lógica de negócio
- **Repositories:** Gerenciam acesso ao banco de dados

### 2. **Padrão Repository**
- Abstração da camada de dados
- Facilita testes unitários
- Permite trocar implementação do banco sem afetar a lógica

### 3. **Dependency Injection**
- Injeção automática de dependências no Program.cs
- Facilita testes e manutenção

### 4. **Validação de Dados**
- Validação de modelos com atributos `[Required]`, `[Range]`, etc.
- Retorno automático de erros de validação

### 5. **Logging**
- Logs de operações importantes
- Facilita debug e monitoramento

### 6. **Documentação Swagger**
- Documentação automática dos endpoints
- Interface interativa para testar a API

---

## 🗂️ Estrutura de Pastas

```
odin-dotnet-api/
├── Models/                          # Entidades
│   ├── Operador.cs
│   ├── Satelite.cs
│   ├── Detrito.cs
│   ├── Manobra.cs
│   └── Alerta.cs
├── Data/                            # Camada de dados
│   ├── OdinDbContext.cs
│   └── Migrations/
│       ├── 20260606_InitialCreate.cs
│       └── OdinDbContextModelSnapshot.cs
├── Services/                        # Lógica de negócio
│   ├── IServices.cs
│   ├── SateliteService.cs
│   ├── OperadorService.cs
│   └── DebitoService.cs
├── Controllers/                     # Endpoints
│   ├── SatelitesController.cs
│   ├── OperadoresController.cs
│   └── DetritosController.cs
├── Program.cs                       # Configuração
├── appsettings.json                 # Configurações
├── OdinApi.csproj                   # Definição do projeto
└── README.md                        # Este arquivo
```

---

## 🔧 Configuração do Banco de Dados

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=OdinDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Opções de Banco de Dados

- **SQL Server Local:** `Server=localhost;Database=OdinDb;Trusted_Connection=true;`
- **SQL Server Remoto:** `Server=servidor.com;Database=OdinDb;User Id=sa;Password=senha;`
- **Azure SQL:** `Server=servidor.database.windows.net;Database=OdinDb;User Id=usuario;Password=senha;`

---

## 📊 Dados de Seed

A aplicação inclui dados iniciais (seed) para testes:

### Operadores (5)
- Marcus Vinícius (ADMIN)
- Hebert Lopes (OPERADOR)
- Nicolas Monteiro (OPERADOR)
- Ana Silva (USUARIO)
- Carlos Santos (USUARIO)

### Satélites (5)
- INSAT-3D
- CBERS-4
- Amazonia-1
- SGDC-1
- BRASILSAT-B2

### Detritos (5)
- DEB-001 a DEB-005

### Manobras (5)
- Variadas com status EXECUTADA, PENDENTE, CANCELADA

### Alertas (5)
- Variadas com severidade BAIXA, MEDIA, ALTA, CRITICA

---

## 🎓 Conceitos Abordados

### 1. **Arquitetura em Camadas**
- Separação clara de responsabilidades
- Facilita manutenção e escalabilidade

### 2. **Entity Framework Core**
- ORM para acesso ao banco de dados
- Migrations para versionamento do schema
- LINQ para consultas type-safe

### 3. **Dependency Injection**
- Injeção automática de dependências
- Facilita testes unitários

### 4. **RESTful API**
- Endpoints seguindo padrões REST
- Códigos HTTP apropriados
- JSON como formato de troca

### 5. **Validação e Tratamento de Erros**
- Validação de entrada
- Tratamento de exceções
- Mensagens de erro claras

---

## 🚨 Troubleshooting

### Erro: "Unable to connect to database"

```bash
# Verificar se SQL Server está rodando
# Verificar string de conexão em appsettings.json
# Executar migrations novamente
dotnet ef database update
```

### Erro: "Migrations not found"

```bash
# Recriar migrations
dotnet ef migrations add InitialCreate --force
dotnet ef database update
```

### Erro: "Port 5001 already in use"

```bash
# Usar porta diferente
dotnet run --urls "https://localhost:5002"
```

---

## 🔗 Links de Produção

| Recurso | URL |
|---------|-----|
| **API REST** | https://odin-dotnet-api.onrender.com/api/v1/satelites |
| **Swagger UI** | https://odin-dotnet-api.onrender.com/swagger/index.html |
| **OpenAPI JSON** | https://odin-dotnet-api.onrender.com/swagger/v1/swagger.json |
| **GitHub** | https://github.com/Hebertlps/odin-dotnet-api |
| **Link do video explicação** | https://youtu.be/UnXwQ5acYWs |
| **Link do video Pitch** |   |

---

## 📚 Referências

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [RESTful API Design](https://restfulapi.net)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

---

## 👥 Integrantes

- **Marcus Vinícius** (RM 558771)
- **Hebert Lopes do Santos** (RM 563192)
- **Nicolas Monteiro** (RM 562380)

---

## 📄 Licença

Este projeto é parte da disciplina **Advanced Business Development with .NET** da FIAP.

---

**Última atualização:** 06/06/2026  
**Versão:** 1.0.0
