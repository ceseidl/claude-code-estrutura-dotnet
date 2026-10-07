---
paths:
  - "src/**/*Endpoints.cs"
---

# Convenções de rotas

- Rotas agrupadas com `MapGroup("/recurso")`.
- Erro de validação: `Results.ValidationProblem` (400).
- Criação devolve `Results.Created` com o `Location`.
- Não encontrado: `Results.NotFound()`, sem corpo.
- Acesso a dados só pelo repositório, nunca na rota.
