---
name: deploy
description: >-
  Publica a Tarefas API: roda os testes e gera o pacote com
  dotnet publish. Só o usuário aciona, com /deploy [ambiente].
argument-hint: "[staging|production]"
disable-model-invocation: true
allowed-tools:
  - Bash(dotnet test *)
  - Bash(dotnet publish *)
---

# Deploy

Ambiente: $ARGUMENTS

Estado atual do repositório:

!`git status --short`

Siga nesta ordem e pare no primeiro erro:

1. Se houver mudanças não commitadas, avise e pare.
2. Leia `deploy-config.md` (nesta pasta) e ache o ambiente.
3. Rode `dotnet test && dotnet publish` com a configuração
   do ambiente, em um único comando (o hook exige os testes).
4. Responda com o resultado dos testes e a pasta gerada.
