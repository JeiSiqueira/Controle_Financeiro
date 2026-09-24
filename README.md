#  Controle Financeiro — Backend

API REST desenvolvida em **C# e .NET 10** para gerenciamento de receitas e despesas pessoais.

O projeto tem como objetivo oferecer uma API organizada para controle financeiro, utilizando autenticação, persistência de dados, arquitetura em camadas e geração de relatórios financeiros.

##  Tecnologias

* C#
* .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* MySQL
* JWT (JSON Web Token)
* Swagger / OpenAPI
* ClosedXML
* Git / GitHub

##  Funcionalidades

###  Autenticação

* Cadastro de usuários
* Login
* Autenticação utilizando JWT
* Proteção dos endpoints com autenticação

###  Transações

* Cadastro de receitas e despesas
* Consulta de transações
* Consulta de transação por ID
* Atualização de transações
* Exclusão de transações
* Associação com categorias
* Identificação do tipo da transação

###  Relatórios financeiros

* Geração de relatórios por período
* Total de receitas
* Total de despesas
* Cálculo do saldo
* Resumo financeiro mensal
* Resultado mensal
* Identificação da situação financeira mensal
* Detalhamento das transações do período

###  Exportação para Excel

* Exportação dos relatórios financeiros para Excel
* Geração de arquivos `.xlsx`
* Utilização da biblioteca **ClosedXML**

##  Arquitetura

O projeto utiliza uma arquitetura organizada em camadas, buscando separar as responsabilidades da aplicação:

```text
Controle_Financeiro/
│
├── Controle_Financeiro.API/
│   ├── Controllers/
│   ├── Services/
│   └── Program.cs
│
├── Controle_Financeiro.Application/
│   ├── Dtos/
│   ├── Interfaces/
│   └── Services/
│
├── Controle_Financeiro.Domain/
│
├── Controle_Financeiro.Infrastructure/
│   └── Repositories/
│
└── Controle_Financeiro.Shared/
```

Essa organização permite separar as responsabilidades entre API, regras de aplicação, domínio e acesso aos dados.

##  Principais endpoints

### Autenticação

```http
POST /api/Auth
```

### Transações

```http
GET    /api/Transacoes
GET    /api/Transacoes/{id}
POST   /api/Transacoes
PUT    /api/Transacoes/{id}
DELETE /api/Transacoes/{id}
```

### Relatórios

```http
GET /api/Relatorios
GET /api/Relatorios/exportar
```

Os endpoints de relatório recebem uma **data inicial** e uma **data final** para definir o período da consulta.

##  Banco de dados

O projeto utiliza MySQL para armazenamento dos dados e Entity Framework Core como ORM.

As alterações da estrutura do banco são gerenciadas utilizando EF Core Migrations.

Como executar
Pré-requisitos

Antes de executar o projeto, tenha instalado:

.NET 10 SDK
MySQL
Visual Studio ou VS Code
Git
1. Clone o repositório
git clone https://github.com/JeiSiqueira/Controle_Financeiro.git
2. Configure o banco de dados

Configure a connection string do MySQL nas configurações da aplicação.

3. Execute as migrations
dotnet ef database update
4. Execute a API
dotnet run
Documentação da API

A API utiliza Swagger / OpenAPI para documentação e testes dos endpoints.

Após iniciar a aplicação, acesse a rota do Swagger disponibilizada pelo projeto.

Frontend

O projeto possui um frontend desenvolvido utilizando React, TypeScript e Vite.

Repositório:

https://github.com/JeiSiqueira/Controle_Financeiro-Frontend

Desenvolvido por

Jeimili Siqueira Mendes

Projeto desenvolvido para estudo, prática e construção de portfólio profissional, com foco em desenvolvimento de APIs REST utilizando C# e .NET, integração com banco de dados relacional, autenticação e geração de relatórios financeiros.
