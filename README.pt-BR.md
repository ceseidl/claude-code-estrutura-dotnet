[English](README.md) | Português

# claude-code-estrutura-dotnet

Repositório de apoio do artigo "Estrutura de um Projeto Claude
Code: o Que Vai em Cada Arquivo e Pasta". É uma solução .NET 10
pequena (Minimal API + xUnit) com a **estrutura completa de
arquivos do Claude Code** funcionando em cima dela:

| Peça | Neste repositório |
| ---- | ----------------- |
| `CLAUDE.md` | instruções do time, com import `@README.md` |
| `CLAUDE.local.md` | `CLAUDE.local.md.example` (o real é ignorado pelo git) |
| `.mcp.json` | dois servidores MCP do projeto (HTTP) |
| `.claude/settings.json` | permissões + um hook `PreToolUse` |
| `.claude/settings.local.json` | só o `.example` (ignorado pelo git) |
| `.claude/rules/` | três regras com escopo por `paths` (`*.cs`, testes, rotas) |
| `.claude/commands/` | `review.md`, `fix-issue.md` (formato legado, ainda aceito) |
| `.claude/skills/deploy/` | `SKILL.md` + `deploy-config.md` |
| `.claude/agents/` | `code-reviewer`, `security-auditor` |
| `.claude/hooks/` | `validate-bash.sh`, o script chamado pelo hook |

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Bash (Git Bash no Windows) para rodar o script do hook
- Opcional: [Claude Code](https://code.claude.com/docs)

## Como rodar

```bash
dotnet build
dotnet test
dotnet run --project src/Tarefas.Api
```

Saída esperada dos testes: 5 aprovados, 0 com falha.

## Testar o hook sem o Claude Code

O Claude Code envia a chamada da ferramenta como JSON no stdin.
Código de saída 2 bloqueia e o stderr é o motivo; 0 libera.

```bash
echo '{"tool_name":"Bash","tool_input":{"command":"dotnet publish"}}' \
  | bash .claude/hooks/validate-bash.sh; echo "exit=$?"
# Bloqueado: rode 'dotnet test' antes do 'dotnet publish'.
# exit=2
```

## Usar os arquivos pessoais

```bash
cp CLAUDE.local.md.example CLAUDE.local.md
cp .claude/settings.local.json.example .claude/settings.local.json
```

Os dois estão no `.gitignore`. O `.gitignore` também tem
`!.claude/`, porque uma regra global que ignora `.claude/`
esconderia a configuração compartilhada do git.

## Notas

- Os arquivos do Claude Code seguem a documentação oficial
  (code.claude.com/docs). O build .NET, os testes e o script do
  hook foram executados; a configuração **não** foi executada
  dentro de uma sessão do Claude Code.
- Os servidores MCP do `.mcp.json` precisam de rede; o `github`
  precisa da variável de ambiente `GITHUB_TOKEN`.
