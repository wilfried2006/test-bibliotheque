using System.Text.Json.Serialization;
using GestionnaireBibliotheque.API.Filters;
using GestionnaireBibliotheque.API.Middleware;
using GestionnaireBibliotheque.Application.DependencyInjection;
using GestionnaireBibliotheque.Infrastructure.Data;
using GestionnaireBibliotheque.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logging Serilog (console)
builder.Host.UseSerilog((context, configuration) =>
    configuration
        .MinimumLevel.Information()
        .WriteTo.Console());

// MVC + filtre de validation global + enums sérialisés en chaîne
builder.Services
    .AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS pour le front Blazor WASM (origines de dev).
const string CorsPolitiqueBlazor = "BlazorWasm";
builder.Services.AddCors(options => options.AddPolicy(CorsPolitiqueBlazor, policy =>
    policy.WithOrigins("http://localhost:5024", "https://localhost:7072")
          .AllowAnyHeader()
          .AllowAnyMethod()));

// Couches applicatives
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Applique les migrations EF au démarrage (schéma + données de seed portées par les migrations),
// pour un `docker compose up` fonctionnel du premier coup. On réessaie : le healthcheck TCP de
// SQL Server peut passer avant que l'instance accepte réellement les connexions.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BibliothequeContext>();
    for (var tentative = 1; ; tentative++)
    {
        try
        {
            db.Database.Migrate();
            app.Logger.LogInformation("Migrations appliquées.");
            break;
        }
        catch (Exception ex) when (tentative < 10)
        {
            app.Logger.LogWarning(ex, "Base indisponible (tentative {Tentative}/10) — nouvel essai dans 3 s…", tentative);
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }
    }
}

// Gestion centralisée des erreurs
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors(CorsPolitiqueBlazor);
app.UseAuthorization();
app.MapControllers();

app.Run();
