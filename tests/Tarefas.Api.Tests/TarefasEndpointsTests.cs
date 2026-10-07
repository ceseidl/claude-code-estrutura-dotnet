using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Tarefas.Api;

namespace Tarefas.Api.Tests;

public class TarefasEndpointsTests(
    WebApplicationFactory<Program> fabrica)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _http = fabrica.CreateClient();

    private async Task<Tarefa> CriarAsync(string titulo)
    {
        var resp = await _http.PostAsJsonAsync(
            "/tarefas", new NovaTarefa(titulo));
        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<Tarefa>())!;
    }

    [Fact]
    public async Task Criar_com_titulo_valido_retorna_201()
    {
        var resp = await _http.PostAsJsonAsync(
            "/tarefas", new NovaTarefa("Ler a documentação"));

        Assert.Equal(HttpStatusCode.Created, resp.StatusCode);
        var tarefa = await resp.Content
            .ReadFromJsonAsync<Tarefa>();
        Assert.Equal("Ler a documentação", tarefa!.Titulo);
        Assert.False(tarefa.Concluida);
        Assert.Equal($"/tarefas/{tarefa.Id}",
            resp.Headers.Location!.OriginalString);
    }

    [Fact]
    public async Task Criar_com_titulo_vazio_retorna_400()
    {
        var resp = await _http.PostAsJsonAsync(
            "/tarefas", new NovaTarefa("   "));

        Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
    }

    [Fact]
    public async Task Obter_inexistente_retorna_404()
    {
        var resp = await _http.GetAsync("/tarefas/999999");

        Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
    }

    [Fact]
    public async Task Concluir_marca_a_tarefa_como_concluida()
    {
        var criada = await CriarAsync("Rodar os testes");

        var resp = await _http.PostAsync(
            $"/tarefas/{criada.Id}/concluir", null);

        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        var tarefa = await resp.Content
            .ReadFromJsonAsync<Tarefa>();
        Assert.True(tarefa!.Concluida);
    }

    [Fact]
    public async Task Listar_filtra_por_concluida()
    {
        var aberta = await CriarAsync("Aberta");
        var feita = await CriarAsync("Feita");
        await _http.PostAsync(
            $"/tarefas/{feita.Id}/concluir", null);

        var abertas = await _http.GetFromJsonAsync<List<Tarefa>>(
            "/tarefas?concluida=false");

        Assert.Contains(abertas!, t => t.Id == aberta.Id);
        Assert.DoesNotContain(abertas!, t => t.Id == feita.Id);
    }
}
