namespace Tarefas.Api;

public static class TarefaEndpoints
{
    public static IEndpointRouteBuilder MapTarefas(
        this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/tarefas");

        grupo.MapGet("/",
            (TarefaRepositorio repo, bool? concluida) =>
                repo.Listar(concluida));

        grupo.MapGet("/{id:int}",
            (int id, TarefaRepositorio repo) =>
                repo.Obter(id) is { } t
                    ? Results.Ok(t)
                    : Results.NotFound());

        grupo.MapPost("/",
            (NovaTarefa nova, TarefaRepositorio repo) =>
        {
            if (string.IsNullOrWhiteSpace(nova.Titulo))
            {
                return Results.ValidationProblem(
                    new Dictionary<string, string[]>
                    {
                        ["titulo"] = ["Informe o título."]
                    });
            }

            var tarefa = repo.Criar(nova.Titulo.Trim());
            return Results.Created(
                $"/tarefas/{tarefa.Id}", tarefa);
        });

        grupo.MapPost("/{id:int}/concluir",
            (int id, TarefaRepositorio repo) =>
                repo.Concluir(id) is { } t
                    ? Results.Ok(t)
                    : Results.NotFound());

        return app;
    }
}
