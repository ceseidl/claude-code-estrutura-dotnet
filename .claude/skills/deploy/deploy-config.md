# Configuração de deploy

| Ambiente   | Configuração | Saída                  |
| ---------- | ------------ | ---------------------- |
| staging    | Release      | `artifacts/staging`    |
| production | Release      | `artifacts/production` |

Comando base:

```bash
dotnet test && dotnet publish src/Tarefas.Api \
  -c Release -o artifacts/<ambiente>
```

Produção só com a branch `main` limpa.
