using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GestionnaireBibliotheque.BlazorWasm;
using GestionnaireBibliotheque.BlazorWasm.Infrastructure;
using GestionnaireBibliotheque.BlazorWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Base URL de l'API (wwwroot/appsettings.json), avec repli sur l'hôte du site.
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
builder.Services.AddScoped<IBackApiService, BackApiService>();

// Cas d'usage (clients de l'API)
builder.Services.AddApplicationUseCases();

await builder.Build().RunAsync();
