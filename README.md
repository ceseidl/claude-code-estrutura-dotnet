English | [Português](README.pt-BR.md)

# claude-code-estrutura-dotnet

Companion repository for the article "Claude Code Project
Structure: What Goes in Each File and Folder". It is a small
.NET 10 solution (Minimal API + xUnit) with the **complete
Claude Code file layout** working on top of it:

| Piece | In this repo |
| ----- | ------------ |
| `CLAUDE.md` | team instructions, with an `@README.md` import |
| `CLAUDE.local.md` | `CLAUDE.local.md.example` (the real one is gitignored) |
| `.mcp.json` | two project-scoped MCP servers (HTTP) |
| `.claude/settings.json` | permissions + a `PreToolUse` hook |
| `.claude/settings.local.json` | `.example` only (gitignored) |
| `.claude/rules/` | three rules scoped by `paths` (`*.cs`, tests, endpoints) |
| `.claude/commands/` | `review.md`, `fix-issue.md` (legacy format, still supported) |
| `.claude/skills/deploy/` | `SKILL.md` + `deploy-config.md` |
| `.claude/agents/` | `code-reviewer`, `security-auditor` |
| `.claude/hooks/` | `validate-bash.sh`, the script the hook calls |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Bash (Git Bash on Windows) to run the hook script
- Optional: [Claude Code](https://code.claude.com/docs)

## How to run

```bash
dotnet build
dotnet test
dotnet run --project src/Tarefas.Api
```

Expected test output: 5 passed, 0 failed.

## Try the hook without Claude Code

Claude Code sends the tool call as JSON on stdin. Exit code 2
blocks it and stderr is the reason; exit 0 allows it.

```bash
echo '{"tool_name":"Bash","tool_input":{"command":"dotnet publish"}}' \
  | bash .claude/hooks/validate-bash.sh; echo "exit=$?"
# Bloqueado pelo hook: rode 'dotnet test' antes do 'dotnet publish'.
# exit=2
```

## Use the personal files

```bash
cp CLAUDE.local.md.example CLAUDE.local.md
cp .claude/settings.local.json.example .claude/settings.local.json
```

Both are listed in `.gitignore`. The `.gitignore` also has
`!.claude/`, because a global `.claude/` ignore rule would
otherwise hide the shared configuration from git.

## Notes

- The Claude Code files follow the official documentation
  (code.claude.com/docs). The .NET build, the tests and the hook
  script were run; the configuration was **not** run inside a
  Claude Code session.
- The MCP servers in `.mcp.json` need network access; `github`
  needs a `GITHUB_TOKEN` environment variable.
- Text of the instruction files is in Portuguese on purpose
  (the team language of the article).
