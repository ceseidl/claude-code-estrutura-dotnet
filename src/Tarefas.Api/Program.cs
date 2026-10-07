using Tarefas.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<TarefaRepositorio>();

var app = builder.Build();
app.MapTarefas();
app.Run();

public partial class Program;
