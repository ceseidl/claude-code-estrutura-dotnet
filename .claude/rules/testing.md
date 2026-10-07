---
paths:
  - "tests/**/*.cs"
---

# Testes

- xUnit, um `[Fact]` por comportamento.
- Nome no padrão `Acao_condicao_resultado`.
- Testes de integração com `WebApplicationFactory<Program>`.
- Todo endpoint novo: pelo menos um teste de sucesso e um
  de erro.
- Rode `dotnet test` antes de concluir qualquer tarefa.
