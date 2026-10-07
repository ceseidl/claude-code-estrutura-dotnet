using System.Collections.Concurrent;

namespace Tarefas.Api;

public class TarefaRepositorio
{
    private readonly ConcurrentDictionary<int, Tarefa> _itens =
        new();
    private int _ultimoId;

    public IReadOnlyList<Tarefa> Listar(bool? concluida = null) =>
        _itens.Values
            .Where(t => concluida is null
                || t.Concluida == concluida)
            .OrderBy(t => t.Id)
            .ToList();

    public Tarefa? Obter(int id) => _itens.GetValueOrDefault(id);

    public Tarefa Criar(string titulo)
    {
        var id = Interlocked.Increment(ref _ultimoId);
        var tarefa = new Tarefa(id, titulo, false);
        _itens[id] = tarefa;
        return tarefa;
    }

    public Tarefa? Concluir(int id) =>
        _itens.TryGetValue(id, out var atual)
            ? _itens[id] = atual with { Concluida = true }
            : null;
}
