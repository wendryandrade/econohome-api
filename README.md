# 🏠 EconoHome - Sistema de Controle de Gastos Residenciais

Sistema completo de controle financeiro residencial desenvolvido com **.NET 8**, seguindo Clean Architecture e boas práticas de desenvolvimento.

---

## 📋 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Tecnologias](#-tecnologias)
- [Funcionalidades](#-funcionalidades)
- [Regras de Negócio](#-regras-de-negócio)
- [Pré-requisitos](#-pré-requisitos)
- [Configuração do Ambiente](#-configuração-do-ambiente)
- [Como Executar](#-como-executar)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [API Endpoints](#-api-endpoints)
- [Seed de Dados](#-seed-de-dados)

---

## 🎯 Sobre o Projeto

O **EconoHome** é um sistema de controle de gastos residenciais que permite gerenciar:
- Cadastro de pessoas (adultos e menores de idade)
- Categorias de transações (despesas, receitas ou ambas)
- Transações financeiras com validações específicas
- Relatórios de totais por pessoa e por categoria

O sistema possui regras de negócio importantes, como a restrição de menores de idade registrarem receitas, e validação de compatibilidade entre categorias e tipos de transação.

---

## 🚀 Tecnologias

### Backend
- **.NET 8** - Framework principal
- **C#** - Linguagem de programação
- **Entity Framework Core** - ORM para acesso ao banco de dados
- **MediatR** - Padrão CQRS para separação de comandos e queries
- **SQL Server** - Banco de dados (rodando no Docker)
- **Swagger** - Documentação automática da API

### Infraestrutura
- **Docker** - Containerização do SQL Server
- **Clean Architecture** - Separação em camadas (Domain, Application, Infrastructure, API)

---

## ✨ Funcionalidades

### 1. Cadastro de Pessoas
- ✅ Criar, editar, deletar e listar pessoas
- ✅ Nome (máx 200 caracteres) e idade
- ✅ Ao deletar uma pessoa, todas as suas transações são removidas (Cascade Delete)

### 2. Cadastro de Categorias
- ✅ Criar e listar categorias
- ✅ Descrição (máx 400 caracteres)
- ✅ Finalidade: Despesa, Receita ou Ambas

### 3. Cadastro de Transações
- ✅ Criar e listar transações
- ✅ Descrição (máx 400 caracteres), valor, tipo, categoria e pessoa
- ✅ Validações automáticas de regras de negócio

### 4. Relatórios
- ✅ Totais por pessoa (receitas, despesas e saldo)
- ✅ Totais por categoria (receitas, despesas e saldo)
- ✅ Total geral consolidado

---

## 📜 Regras de Negócio

### 🔴 Regra 1: Menor de Idade
**Menores de 18 anos só podem registrar DESPESAS.**
- A lógica é simples: criança não trabalha, então não tem receita
- Validação aplicada ao criar e atualizar transações

### 🔴 Regra 2: Compatibilidade de Categoria
**A categoria deve ser compatível com o tipo de transação.**
- Categoria "Salário" (Income) → Só aceita receitas
- Categoria "Alimentação" (Expense) → Só aceita despesas
- Categoria "Reembolsos" (Both) → Aceita ambos

### 🔴 Regra 3: Cascade Delete
**Ao deletar uma pessoa, todas as suas transações são automaticamente excluídas.**

### 🔴 Regra 4: Validações de Tamanho
- Nome da pessoa: máximo 200 caracteres
- Descrição de categoria: máximo 400 caracteres
- Descrição de transação: máximo 400 caracteres

---

## 📦 Pré-requisitos

Antes de começar, você vai precisar ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (versão 22 ou superior)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

---

## ⚙️ Configuração do Ambiente

### 1️⃣ Clonar o Repositório

```bash
git clone https://github.com/seu-usuario/EconoHome.git
cd EconoHome
```

### 2️⃣ Configurar o SQL Server no Docker

O projeto utiliza SQL Server rodando em um container Docker.

#### Criar e executar o container:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SuaSenhaForte@123" -p 1433:1433 --name sqlserver-econohome -d mcr.microsoft.com/mssql/server:2022-latest
```

**Importante:**
- Troque `SuaSenhaForte@123` por uma senha forte de sua preferência
- A senha deve conter: letras maiúsculas, minúsculas, números e caracteres especiais
- Anote a senha, você vai precisar dela na connection string

#### Verificar se o container está rodando:

```bash
docker ps
```

Você deve ver o container `sqlserver-econohome` na lista.

#### Comandos úteis do Docker:

```bash
# Parar o container
docker stop sqlserver-econohome

# Iniciar o container novamente
docker start sqlserver-econohome

# Ver logs do SQL Server
docker logs sqlserver-econohome

# Remover o container (cuidado: isso apaga o banco!)
docker rm -f sqlserver-econohome
```

### 3️⃣ Configurar a Connection String

Abra o arquivo `EconoHome/appsettings.json` e configure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=EconoHomeDB;User Id=sa;Password=SuaSenhaForte@123;TrustServerCertificate=True;"
  }
}
```

**Atenção:** Substitua `SuaSenhaForte@123` pela senha que você definiu no Docker!

### 4️⃣ Criar o Banco de Dados

Abra o **Package Manager Console** no Visual Studio ou use o terminal:

```bash
# Via Package Manager Console (Visual Studio)
Add-Migration InitialCreate -StartupProject EconoHome.API -Project EconoHome.Infrastructure
Update-Database -StartupProject EconoHome.API -Project EconoHome.Infrastructure

# Ou via .NET CLI (Terminal)
cd EconoHome.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../EconoHome
dotnet ef database update --startup-project ../EconoHome
```

Isso vai:
1. Criar as migrations
2. Criar o banco `EconoHomeDB` no SQL Server
3. Criar todas as tabelas (Persons, Categories, Transactions)

---

## 🏃 Como Executar

### Backend (.NET API)

#### Opção 1: Via Visual Studio
1. Abra a solution `EconoHome.sln`
2. Defina `EconoHome.API` como projeto de inicialização
3. Pressione `F5` ou clique em "Run"

#### Opção 2: Via Terminal
```bash
cd EconoHome
dotnet run
```

A API estará disponível em:
- **HTTPS:** `https://localhost:7202`
- **HTTP:** `http://localhost:5000`
- **Swagger:** `https://localhost:7001/swagger`

### Frontend (React)

```bash
cd econohome-frontend
npm install
npm run dev
```

---

## 🌱 Seed de Dados

**O banco é populado automaticamente na primeira execução!**

Quando você rodar a aplicação pela primeira vez, o sistema vai inserir dados de exemplo:

### Pessoas Criadas:
- **João Silva** (30 anos) - Adulto
- **Maria Santos** (25 anos) - Adulto
- **Pedro Costa** (16 anos) - **Menor de idade**
- **Ana Lima** (17 anos) - **Menor de idade**

### Categorias Criadas:
- **Despesas:** Alimentação, Transporte, Educação
- **Receitas:** Salário, Freelance, Investimentos
- **Ambas:** Reembolsos, Ajuste de Contas

### Transações Criadas:
- **16 transações no total**
- Adultos têm receitas e despesas
- Menores têm **apenas despesas** (regra respeitada ✅)

**Observação:** O seed só roda uma vez. Se você quiser resetar os dados:
1. Delete o banco via SQL Server Management Studio ou Docker
2. Execute novamente `dotnet ef database update`

---

## 📁 Estrutura do Projeto

```
EconoHome/
├── EconoHome.Domain/              # Camada de Domínio
│   ├── Entities/                  # Entidades (Person, Category, Transaction)
│   └── Enums/                     # Enums (TransactionType, CategoryPurpose)
│
├── EconoHome.Application/         # Camada de Aplicação
│   ├── Features/                  # Casos de uso (CQRS)
│   │   ├── Persons/
│   │   │   ├── Commands/          # Comandos (Create, Update, Delete)
│   │   │   ├── Queries/           # Queries (GetAll, GetById, Summary)
│   │   │   └── DTOs/              # Data Transfer Objects
│   │   ├── Categories/
│   │   └── Transactions/
│   └── Interfaces/                # Interfaces dos repositórios
│
├── EconoHome.Infrastructure/      # Camada de Infraestrutura
│   ├── Persistence/
│   │   ├── Context/               # DbContext do EF Core
│   │   ├── Repositories/          # Implementação dos repositórios
│   │   └── Data/                  # DatabaseSeeder
│   └── Migrations/                # Migrations do EF Core
│
├── EconoHome.API/                 # Camada de Apresentação (API)
│   ├── Controllers/               # Controllers da API
│   ├── Program.cs                 # Configuração da aplicação
│   └── appsettings.json           # Configurações (connection string)
│
└── econohome-frontend/            # Frontend React
    ├── src/
    ├── package.json
    └── vite.config.ts
```

---

## 🔌 API Endpoints

### 👥 Persons (Pessoas)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/persons` | Lista todas as pessoas |
| GET | `/api/persons/{id}` | Busca pessoa por ID |
| GET | `/api/persons/summary` | Totais por pessoa + total geral |
| POST | `/api/persons` | Cria nova pessoa |
| PUT | `/api/persons/{id}` | Atualiza pessoa |
| DELETE | `/api/persons/{id}` | Deleta pessoa (e suas transações) |

### 🏷️ Categories (Categorias)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/categories` | Lista todas as categorias |
| GET | `/api/categories/summary` | Totais por categoria + total geral |
| POST | `/api/categories` | Cria nova categoria |
| PUT | `/api/categories/{id}` | Atualiza categoria |
| DELETE | `/api/categories/{id}` | Deleta categoria |

### 💰 Transactions (Transações)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/transactions` | Lista todas as transações |
| POST | `/api/transactions` | Cria nova transação |
| PUT | `/api/transactions/{id}` | Atualiza transação |
| DELETE | `/api/transactions/{id}` | Deleta transação |

---

## 🧪 Testando a API

### Via Swagger
1. Acesse `https://localhost:7202/swagger`
2. Explore os endpoints interativamente
3. Teste as validações (tente criar receita para menor de idade!)

---

## 📝 Notas Importantes

### Sobre a Arquitetura

O projeto segue **Clean Architecture**:
- **Domain:** Entidades e regras de negócio puras
- **Application:** Casos de uso (CQRS com MediatR)
- **Infrastructure:** Acesso a dados (EF Core)
- **API:** Camada de apresentação (Controllers)

---

## 👨‍💻 Desenvolvimento

### Padrões Utilizados

- ✅ **CQRS** - Separação entre Commands e Queries
- ✅ **MediatR** - Mediator pattern para desacoplamento
- ✅ **Repository Pattern** - Abstração do acesso a dados
- ✅ **DTOs** - Separação entre entidades e contratos da API
- ✅ **Dependency Injection** - Injeção de dependências nativa do .NET