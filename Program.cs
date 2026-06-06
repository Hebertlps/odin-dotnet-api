using Microsoft.EntityFrameworkCore;
using OdinApi.Data;
using OdinApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicionar DbContext com SQLite (sem necessidade de servidor externo)
builder.Services.AddDbContext<OdinDbContext>(options =>
    options.UseSqlite("Data Source=odin.db"));

// Adicionar Services
builder.Services.AddScoped<ISateliteService, SateliteService>();
builder.Services.AddScoped<IOperadorService, OperadorService>();
builder.Services.AddScoped<IDebitoService, DebitoService>();

// Adicionar Controllers com tratamento de ciclos JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "O.D.I.N. API - .NET",
        Version = "v1",
        Description = "API REST para gerenciamento de satélites, operadores e detritos espaciais. Projeto O.D.I.N. (Orbital Debris Identification Network) - Disciplina Advanced Business Development with .NET - FIAP",
        Contact = new OpenApiContact
        {
            Name = "ODIN Team - Marcus Vinícius, Hebert Lopes, Nicolas Monteiro",
            Email = "contact@odin.local"
        }
    });
});

// Adicionar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Aplicar migrations e seed automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OdinDbContext>();
    db.Database.Migrate();
}

// Swagger habilitado em todos os ambientes
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "O.D.I.N. API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "O.D.I.N. API - .NET";
});

// Redirecionar raiz para Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
