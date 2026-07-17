using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using GestionnaireBibliotheque.BlazorWasm.Models;

namespace GestionnaireBibliotheque.BlazorWasm.Services;

public class BackApiService(HttpClient httpClient) : IBackApiService
{
    // --- Auteurs ---
    public Task<List<AuteurDto>?> RecupereLesAuteursAsync() => GetAsync<List<AuteurDto>>("api/auteurs");
    public Task<HttpResponseMessage> CreerAuteurAsync(CreateAuteurRequest request) => PostAsync("api/auteurs", request);

    // --- Ouvrages ---
    public Task<List<OuvrageDto>?> RecupereLesOuvragesAsync() => GetAsync<List<OuvrageDto>>("api/ouvrages");
    public Task<HttpResponseMessage> CreerOuvrageAsync(CreateOuvrageRequest request) => PostAsync("api/ouvrages", request);

    // --- Exemplaires ---
    public Task<List<ExemplaireDto>?> RecupereLesExemplairesAsync() => GetAsync<List<ExemplaireDto>>("api/exemplaires");
    public Task<HttpResponseMessage> CreerExemplaireAsync(CreateExemplaireRequest request) => PostAsync("api/exemplaires", request);

    // --- Membres ---
    public Task<List<MembreDto>?> RecupereLesMembreAsync() => GetAsync<List<MembreDto>>("api/membres");
    public Task<HttpResponseMessage> CreerMembreAsync(CreateMembreRequest request) => PostAsync("api/membres", request);

    // --- Types d'adhérent ---
    public Task<List<TypeAdherentDto>?> RecupereLesTypesAdherentAsync() => GetAsync<List<TypeAdherentDto>>("api/typesadherent");

    // --- Emprunts ---
    public Task<List<EmpruntDto>?> RecupereLesEmpruntAsync() => GetAsync<List<EmpruntDto>>("api/emprunts");
    public Task<HttpResponseMessage> CreerEmpruntAsync(CreateEmpruntRequest request) => PostAsync("api/emprunts", request);
    public Task<HttpResponseMessage> RetournerEmpruntAsync(int empruntId) => PostAsync($"api/emprunts/{empruntId}/retour");

    // --- Pénalités ---
    public Task<List<PenaliteDto>?> RecupereLespenalitesAsync() => GetAsync<List<PenaliteDto>>("api/penalites");
    public Task<HttpResponseMessage> MarquerPenalitePayeAsync(int penaliteId) => PostAsync($"api/penalites/{penaliteId}/payer");

    // --- Helpers HTTP ---

    private async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            // NoStore : contourne le cache navigateur pour toujours relire l'état frais du serveur.
            using var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.SetBrowserRequestCache(BrowserRequestCache.NoStore);
            using var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        catch
        {
            return default;
        }
    }

    // POST typé (System.Text.Json) — aucune réflexion dynamique (DLR), compatible trimming/AOT.
    private Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data) => httpClient.PostAsJsonAsync(endpoint, data);

    // POST sans corps : l'identifiant est porté par la route (retour d'emprunt, paiement de pénalité).
    private Task<HttpResponseMessage> PostAsync(string endpoint) => httpClient.PostAsync(endpoint, content: null);
}
