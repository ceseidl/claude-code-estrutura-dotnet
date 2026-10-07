# Tarefas API

API mínima em ASP.NET Core (.NET 10) com lista de tarefas em
memória. Visão geral e como rodar: @README.md

## Comandos

- Build: `dotnet build`
- Testes: `dotnet test`
- Rodar a API: `dotnet run --project src/Tarefas.Api`
- Pacotes NuGet: sempre com
  `--source https://api.nuget.org/v3/index.json`

## Estrutura

- `src/Tarefas.Api/`: rotas em `TarefaEndpoints.cs`,
  armazenamento em `TarefaRepositorio.cs`
- `tests/Tarefas.Api.Tests/`: testes de integração (xUnit)
- `.claude/`: regras, skills, agentes e hooks do time

## Convenções

- C# 14, `Nullable` habilitado, namespaces com escopo de arquivo.
- Detalhes de estilo, testes e rotas: `.claude/rules/`
  (carregam sozinhas ao abrir arquivos do escopo).
- Deploy só pela skill `/deploy`, nunca à mão.

## Fluxo de trabalho

- Rode `dotnet build` e `dotnet test` antes de dizer que
  terminou.
- Não faça commit nem push sem eu pedir.
- Nunca leia nem escreva arquivos `.env`.

<!-- Preferências pessoais: CLAUDE.local.md (fora do git) -->
