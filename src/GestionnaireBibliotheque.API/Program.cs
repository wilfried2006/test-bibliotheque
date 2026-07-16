using System.Text.Json.Serialization;
using GestionnaireBibliotheque.API.Filters;
using GestionnaireBibliotheque.API.Middleware;
using GestionnaireBibliotheque.Application.DependencyInjection;
using GestionnaireBibliotheque.Infrastructure.DependencyInjection;
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
