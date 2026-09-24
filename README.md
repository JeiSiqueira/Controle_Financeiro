# Controle Financeiro

Aplicação web para gerenciamento de receitas, despesas e categorias.

Projeto desenvolvido como parte do meu portfólio para aplicar na prática conhecimentos de desenvolvimento backend, frontend, APIs REST, autenticação, banco de dados e integração entre aplicações.

## Sobre o projeto

O Controle Financeiro permite que usuários gerenciem suas movimentações financeiras, cadastrando receitas e despesas, organizando transações por categorias e acompanhando um resumo financeiro através do dashboard.

A aplicação possui autenticação de usuários utilizando JWT e integração entre uma API desenvolvida em C#/.NET e um frontend desenvolvido em React + TypeScript.

## Funcionalidades

- Cadastro de usuário
- Login e autenticação com JWT
- Proteção de rotas autenticadas
- Cadastro de receitas e despesas
- Listagem de transações
- Edição de transações
- Exclusão de transações
- Cadastro de categorias
- Prevenção de categorias duplicadas
- Criação de categoria durante o cadastro de uma transação
- Dashboard com receitas, despesas e saldo
- Integração entre frontend e backend
- Tratamento global de exceções na API

## Tecnologias

### Backend

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- JWT
- BCrypt
- Swagger
- Repository Pattern
- DTOs

### Frontend

- React
- TypeScript
- Vite
- Axios
- React Router
- CSS

### Ferramentas

- Visual Studio
- Visual Studio Code
- Git
- GitHub
- Swagger

## Arquitetura

O backend foi organizado em camadas para separar as responsabilidades da aplicação.

```text
backend/
│
├── Controle_Financeiro.API
│   ├── Controllers
│   ├── Middleware
│   └── Services
│
├── Controle_Financeiro.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Exceptions
│
├── Controle_Financeiro.Domain
│   └── Entities
│
├── Controle_Financeiro.Infrastructure
│   ├── Data
│   ├── Repositories
│   └── Migrations
│
└── Controle_Financeiro.Shared
    └── Responses
```

## Como executar

### Pré-requisitos

- .NET 8 SDK
- Node.js
- MySQL
- Git

### Backend

Clone o repositório:

```bash
git clone https://github.com/JeiSiqueira/Controle_Financeiro.git