Controle Financeiro API

API REST desenvolvida em **C# e .NET 10** para gerenciamento de receitas e despesas.

Este projeto foi criado com o objetivo de praticar conceitos de desenvolvimento backend, utilizando boas práticas como DTOs, Entity Framework Core e arquitetura em camadas.

---

Tecnologias

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- Swagger
- Git e GitHub

---

Estrutura do projeto

```
Controle_Financeiro
│
├── Controle_Financeiro.API
├── Controle_Financeiro.Application
├── Controle_Financeiro.Domain
├── Controle_Financeiro.Infrastructure
└── Controle_Financeiro.Shared
```

---

Funcionalidades

Transações

- Criar transações
- Listar todas as transações
- Buscar transação por ID
- Atualizar transações
- Excluir transações

Categorias

- Associação entre transação e categoria
- Validação de categoria existente

---

Endpoints

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/transacoes` | Lista todas as transações |
| GET | `/api/transacoes/{id}` | Busca uma transação pelo ID |
| POST | `/api/transacoes` | Cadastra uma nova transação |
| PUT | `/api/transacoes/{id}` | Atualiza uma transação |
| DELETE | `/api/transacoes/{id}` | Remove uma transação |

---

Como executar

Clone o repositório:

```bash
git clone <URL_DO_REPOSITORIO>
```

Entre na pasta do projeto:

```bash
cd backend
```

Execute:

```bash
dotnet restore
dotnet run
```

Depois acesse o Swagger pelo navegador.

---

Desenvolvido por

**Jeimili Siqueira**

GitHub: https://github.com/JeiSiqueira