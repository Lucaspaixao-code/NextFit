# API - Gestão de Alunos e Aulas

## Visão Geral
API desenvolvida em **.NET 8**, organizada em camadas, com suporte a **Swagger** para documentação.  
Funcionalidades principais:
- Cadastro de alunos e aulas
- Agendamento de alunos em aulas
- Relatórios simples por aluno

Os dados são armazenados em memória apenas durante a execução.

---

## Requisitos

- **.NET 8 SDK** ou superior → [Download](https://dotnet.microsoft.com/download/dotnet/8.0)  
- **IDE** recomendada:
  - Visual Studio 2022 (17.8 ou superior)
  - ou Visual Studio Code + extensão C#
- (Opcional) **Postman** para testes de endpoints

---

## Dependências

- `Microsoft.AspNetCore.OpenApi`
- `Swashbuckle.AspNetCore`
- `System.Text.Json`
- (Opcional) `AutoMapper`

---

## Executando o Projeto

1. Clone este repositório ou extraia o projeto.
2. No terminal, navegue até a pasta do projeto (onde está o `.csproj`).
3. Execute:

   ```bash
   dotnet run
   ```

4. A API será inicializada nas portas padrão:

   ```
   http://localhost:55001
   ```

---

## Documentação da API (Swagger)

- Ambiente de desenvolvimento abre automaticamente o Swagger no navegador.
- Caso contrário, acesse manualmente:

```
http://localhost:55001/swagger
```

---

## Estrutura do Projeto

```
/Controllers     -> Endpoints da API
/Services        -> Regras de negócio
/Repositories    -> Camada de acesso a dados em memória
/Models          -> Entidades do domínio
/DTOs            -> Objetos de transferência de dados
```

---

## Testando a API

Exemplo de request com `cURL`:

```bash
curl -X GET "https://localhost:7000/api/alunos" -H "accept: application/json"
```

Exemplo de JSON para criação de aluno:

```json
{
  "nome": "João Silva",
  "plano": "Mensal"
}
```

Exemplo de JSON para criação de aula:

```json
{
  "tipo": "Cross",
  "dataHora": "2025-09-20T10:00:00",
  "capacidadeMaxima": 15
}
```

---

## Notas de Desenvolvimento

- Os dados são reiniciados sempre que a aplicação é parada.
- Para persistência real, substitua os repositórios em memória por integração com banco de dados.
