using System;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NextFit.Data;
using NextFit.Models;
using NextFit.Repositories;
using NextFit.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AcademiaContext>(opt =>
    opt.UseInMemoryDatabase("Db"));

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAulaRepository, AulaRepository>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

builder.Services.AddScoped<IAcademiaService, AcademiaService>();

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.WriteIndented = true;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AcademiaContext>();

    if (!db.Alunos.Any())
    {
        db.Alunos.AddRange(
            new Aluno { Nome = "Lucas", Plano = TipoPlano.Mensal },
            new Aluno { Nome = "Mariana", Plano = TipoPlano.Trimestral },
            new Aluno { Nome = "Pedro", Plano = TipoPlano.Anual }
        );
    }

    if (!db.Aulas.Any())
    {
        db.Aulas.AddRange(
            new Aula { Tipo = "Cross", DataHora = DateTime.UtcNow.AddDays(1), CapacidadeMaxima = 10 },
            new Aula { Tipo = "Funcional", DataHora = DateTime.UtcNow.AddDays(2), CapacidadeMaxima = 8 },
            new Aula { Tipo = "Pilates", DataHora = DateTime.UtcNow.AddDays(3), CapacidadeMaxima = 6 }
        );
    }

    db.SaveChanges();
}

if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "NextFit API v1");
        options.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); 
app.Run();

