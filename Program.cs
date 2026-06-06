using Microsoft.EntityFrameworkCore;
using OdinApi.Data;
using OdinApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplicationBuilder.CreateBuilder(args);

// Adicionar DbContext
builder.Services.AddDbContext<OdinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar Services
builder.Services.AddScoped<ISateliteService, SateliteService>();
builder.Services.AddScoped<IOperadorService, OperadorService>();
builder.Services.AddScoped<IDebitoService, DebitoService>();

// Adicionar Controllers
builder.Services.AddControllers()
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Program>());

// Adicionar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "O.D.I.N. API - .NET",
        Version = "v1",
        Description = "API REST para gerenciamento de satélites, operadores e detritos espaciais",
        Contact = new OpenApiContact
        {
            Name = "ODIN Team",
            Email = "contact@odin.local"
        }
    });
});

// Adicionar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Aplicar migrations automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OdinDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "O.D.I.N. API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();
