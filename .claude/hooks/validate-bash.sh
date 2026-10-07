#!/usr/bin/env bash
# PreToolUse (Bash): bloqueia comandos perigosos antes de rodar.
# Entrada: JSON no stdin. Saída: exit 0 libera, exit 2 bloqueia
# (o stderr vira o motivo mostrado ao Claude).
input=$(cat)

if command -v jq >/dev/null 2>&1; then
  cmd=$(printf '%s' "$input" | jq -r '.tool_input.command // ""')
else
  # sem jq: pega o valor de "command" (aceita \" dentro dele)
  cmd=$(printf '%s' "$input" | sed -n \
    's/.*"command" *: *"\(\([^"\\]\|\\.\)*\)".*/\1/p')
fi

block() { echo "Bloqueado: $1" >&2; exit 2; }

case "$cmd" in
  *"git push --force"*|*"git push -f"*)
    block "force push não é permitido." ;;
  *"rm -rf"*)
    block "rm -rf não é permitido." ;;
  *"dotnet publish"*)
    case "$cmd" in
      *"dotnet test"*) ;;
      *) block "rode 'dotnet test' antes do 'dotnet publish'." ;;
    esac ;;
esac

exit 0
