namespace Tarefas.Api;

public record Tarefa(int Id, string Titulo, bool Concluida);

public record NovaTarefa(string? Titulo);
