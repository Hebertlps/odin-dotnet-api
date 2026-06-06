# 🧪 Guia de Testes - O.D.I.N. API .NET

Este documento descreve como testar todos os endpoints da API.

---

## 📋 Pré-requisitos

- .NET SDK 8.0+
- SQL Server
- Postman, Insomnia ou cURL
- Aplicação rodando em `https://localhost:5001`

---

## 🚀 Iniciar a Aplicação

```bash
# 1. Restaurar dependências
dotnet restore

# 2. Aplicar migrations
dotnet ef database update

# 3. Executar
dotnet run

# A API estará em: https://localhost:5001
# Swagger UI: https://localhost:5001/swagger/index.html
```

---

## 🧪 Testes de Endpoints

### 1️⃣ SATELITES

#### 1.1 Listar Todos os Satélites

```bash
curl -X GET "https://localhost:5001/api/v1/satelites" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Status 200 OK com array de satélites

#### 1.2 Obter Satélite por ID

```bash
curl -X GET "https://localhost:5001/api/v1/satelites/1" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Status 200 OK com dados do satélite INSAT-3D

#### 1.3 Criar Novo Satélite

```bash
curl -X POST "https://localhost:5001/api/v1/satelites" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "NOVO-SATELITE",
    "combustivelAtual": 300,
    "statusOperacional": "ATIVO",
    "dataLancamento": "2024-01-15T00:00:00",
    "operadorId": 1
  }' \
  -k
```

**Esperado:** Status 201 Created com ID do novo satélite

#### 1.4 Atualizar Satélite

```bash
curl -X PUT "https://localhost:5001/api/v1/satelites/1" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "INSAT-3D-UPDATED",
    "combustivelAtual": 480,
    "statusOperacional": "MANUTENCAO",
    "dataLancamento": "2013-09-26T00:00:00",
    "operadorId": 1
  }' \
  -k
```

**Esperado:** Status 200 OK com dados atualizados

#### 1.5 Deletar Satélite

```bash
curl -X DELETE "https://localhost:5001/api/v1/satelites/6" \
  -k
```

**Esperado:** Status 204 No Content

---

### 2️⃣ OPERADORES

#### 2.1 Listar Todos os Operadores

```bash
curl -X GET "https://localhost:5001/api/v1/operadores" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Status 200 OK com array de 5 operadores

#### 2.2 Obter Operador por ID

```bash
curl -X GET "https://localhost:5001/api/v1/operadores/1" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Status 200 OK com dados de Marcus Vinícius

#### 2.3 Criar Novo Operador

```bash
curl -X POST "https://localhost:5001/api/v1/operadores" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "email": "joao@odin.local",
    "nivelAcesso": "OPERADOR",
    "dataCriacao": "2026-06-06T00:00:00"
  }' \
  -k
```

**Esperado:** Status 201 Created com novo operador

#### 2.4 Atualizar Operador

```bash
curl -X PUT "https://localhost:5001/api/v1/operadores/1" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Marcus Vinícius Silva",
    "email": "marcus.silva@odin.local",
    "nivelAcesso": "ADMIN"
  }' \
  -k
```

**Esperado:** Status 200 OK com dados atualizados

#### 2.5 Deletar Operador

```bash
curl -X DELETE "https://localhost:5001/api/v1/operadores/5" \
  -k
```

**Esperado:** Status 204 No Content

---

### 3️⃣ DETRITOS

#### 3.1 Listar Todos os Detritos

```bash
curl -X GET "https://localhost:5001/api/v1/detritos" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Status 200 OK com array de detritos

#### 3.2 Obter Detrito por ID

```bash
curl -X GET "https://localhost:5001/api/v1/detritos/1" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Status 200 OK com dados do detrito DEB-001

#### 3.3 Criar Novo Detrito

```bash
curl -X POST "https://localhost:5001/api/v1/detritos" \
  -H "Content-Type: application/json" \
  -d '{
    "identificacao": "DEB-006",
    "latitude": 45.5,
    "longitude": 120.3,
    "altitude": 500,
    "velocidade": 7.5,
    "nivelRisco": 70,
    "operadorId": 1,
    "dataDeteccao": "2026-06-06T00:00:00"
  }' \
  -k
```

**Esperado:** Status 201 Created com novo detrito

#### 3.4 Atualizar Detrito

```bash
curl -X PUT "https://localhost:5001/api/v1/detritos/1" \
  -H "Content-Type: application/json" \
  -d '{
    "identificacao": "DEB-001-UPDATED",
    "latitude": 0.5,
    "longitude": 79.6,
    "altitude": 36100,
    "velocidade": 3.08,
    "nivelRisco": 50,
    "operadorId": 1
  }' \
  -k
```

**Esperado:** Status 200 OK com dados atualizados

#### 3.5 Deletar Detrito

```bash
curl -X DELETE "https://localhost:5001/api/v1/detritos/6" \
  -k
```

**Esperado:** Status 204 No Content

---

## ❌ Testes de Erro

### Erro 404 - Recurso Não Encontrado

```bash
curl -X GET "https://localhost:5001/api/v1/satelites/999" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Status 404 com mensagem "Satélite com ID 999 não encontrado"

### Erro 400 - Dados Inválidos

```bash
curl -X POST "https://localhost:5001/api/v1/satelites" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "",
    "combustivelAtual": -100,
    "statusOperacional": "INVALIDO",
    "dataLancamento": "2024-01-15T00:00:00",
    "operadorId": 1
  }' \
  -k
```

**Esperado:** Status 400 com erros de validação

---

## 📊 Validação de Relacionamentos

### Verificar Relacionamento Operador → Satélites

```bash
# Listar operador com seus satélites
curl -X GET "https://localhost:5001/api/v1/operadores/1" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Resposta inclui array `satelites` com todos os satélites do operador

### Verificar Relacionamento Satelite → Manobras

```bash
# Listar satélite com suas manobras
curl -X GET "https://localhost:5001/api/v1/satelites/1" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Resposta inclui array `manobras` com todas as manobras do satélite

### Verificar Relacionamento Detrito → Alertas

```bash
# Listar detrito com seus alertas
curl -X GET "https://localhost:5001/api/v1/detritos/1" \
  -H "accept: application/json" \
  -k
```

**Esperado:** Resposta inclui array `alertas` com todos os alertas do detrito

---

## 🔍 Testes com Postman

### Importar Collection

1. Abrir Postman
2. Clicar em "Import"
3. Colar a URL ou arquivo da collection
4. Executar os testes

### Exemplo de Request

```
GET /api/v1/satelites
Host: localhost:5001
Accept: application/json
```

---

## 📈 Testes de Carga

```bash
# Testar múltiplas requisições
for i in {1..10}; do
  curl -X GET "https://localhost:5001/api/v1/satelites" \
    -H "accept: application/json" \
    -k
done
```

---

## ✅ Checklist de Testes

- [ ] GET /satelites - Retorna 200 com array
- [ ] GET /satelites/1 - Retorna 200 com satélite
- [ ] POST /satelites - Retorna 201 com novo satélite
- [ ] PUT /satelites/1 - Retorna 200 com satélite atualizado
- [ ] DELETE /satelites/6 - Retorna 204
- [ ] GET /operadores - Retorna 200 com array
- [ ] GET /operadores/1 - Retorna 200 com operador
- [ ] POST /operadores - Retorna 201 com novo operador
- [ ] PUT /operadores/1 - Retorna 200 com operador atualizado
- [ ] DELETE /operadores/5 - Retorna 204
- [ ] GET /detritos - Retorna 200 com array
- [ ] GET /detritos/1 - Retorna 200 com detrito
- [ ] POST /detritos - Retorna 201 com novo detrito
- [ ] PUT /detritos/1 - Retorna 200 com detrito atualizado
- [ ] DELETE /detritos/6 - Retorna 204
- [ ] GET /satelites/999 - Retorna 404
- [ ] Relacionamentos carregam corretamente

---

## 🐛 Debugging

### Ver Logs

```bash
# Logs detalhados
dotnet run --verbosity detailed
```

### Verificar Banco de Dados

```bash
# Conectar ao SQL Server
sqlcmd -S localhost -U sa -P sua_senha

# Listar tabelas
SELECT * FROM INFORMATION_SCHEMA.TABLES;

# Verificar dados
SELECT * FROM Satelites;
SELECT * FROM Operadores;
SELECT * FROM Detritos;
```

---

**Última atualização:** 06/06/2026
