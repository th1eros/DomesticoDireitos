using Microsoft.EntityFrameworkCore;
using DomesticoDireitos.Api.Data;
using DomesticoDireitos.Api.Services;
using DomesticoDireitos.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. SERVIÇOS
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Atende: Segurança (Evita loops infinitos em relações N:M)
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS: Atende o critério de Estrutura Cliente-Servidor (React <-> .NET)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Conexão com SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro do Serviço de Negócio
builder.Services.AddScoped<DiagnosticoService>();

var app = builder.Build();

// 2. BANCO E SEED (Atende: Modelagem de banco de dados)
using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // REMOVIDO: EnsureCreated (Para não conflitar com as Migrations)
        DbInitializer.Initialize(context); // Alimenta o banco com a Lei 150/2015
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERRO BANCO]: {ex.Message}");
    }
}

// 3. PIPELINE DE EXECUÇÃO
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Direitos Domésticos API v1");
    c.RoutePrefix = string.Empty; // Swagger abre direto na raiz (/)
    c.DocumentTitle = "Guia Direitos Domésticos - Lei 150/2015";
});

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();