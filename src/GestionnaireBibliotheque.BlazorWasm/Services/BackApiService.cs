using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using GestionnaireBibliotheque.BlazorWasm.Models;

namespace GestionnaireBibliotheque.BlazorWasm.Services;

public class BackApiService(HttpClient httpClient) : IBackApiService
{
    public async Task<List<AuteurDto>?> RecupereLesAuteursAsync()
        => await GetAsync<List<AuteurDto>>("api/auteurs");

    public async Task<HttpResponseMessage> CreerAuteurAsync(dynamic request)
        => await PostRawAsync("api/auteurs", request);

    public async Task<List<OuvrageDto>?> RecupereLesOuvragesAsync()
        => await GetAsync<List<OuvrageDto>>("api/ouvrages");

    public async Task<HttpResponseMessage> CreerOuvrageAsync(dynamic request)
        => await PostRawAsync("api/ouvrages", request);

    public async Task<List<ExemplaireDto>?> RecupereLesExemplairesAsync()
        => await GetAsync<List<ExemplaireDto>>("api/exemplaires");

    public async Task<HttpResponseMessage> CreerExemplaireAsync(dynamic request)
        => await PostRawAsync("api/exemplaires", request);

    public async Task<List<MembreDto>?> RecupereLesMembreAsync()
        => await GetAsync<List<MembreDto>>("api/membres");

    public async Task<HttpResponseMessage> CreerMembreAsync(dynamic request)
        => await PostRawAsync("api/membres", request);

    public async Task<List<TypeAdherentDto>?> RecupereLesTypesAdherentAsync()
        => await GetAsync<List<TypeAdherentDto>>("api/typesadherent");

    public async Task<List<EmpruntDto>?> RecupereLesEmpruntAsync()
        => await GetAsync<List<EmpruntDto>>("api/emprunts");

    public async Task<HttpResponseMessage> CreerEmpruntAsync(dynamic request)
        => await PostRawAsync("api/emprunts", request);

    public async Task<HttpResponseMessage> RetournerEmpruntAsync(int empruntId)
        => await PostRawAsync($"api/emprunts/{empruntId}/retour", new { });

    public async Task<List<PenaliteDto>?> RecupereLespenalitesAsync()
        => await GetAsync<List<PenaliteDto>>("api/penalites");

    public async Task<List<PenaliteDto>?> RecupereLePenalitesMembreAsync(int membreId)
        => await GetAsync<List<PenaliteDto>>($"api/membres/{membreId}/penalites");

    public async Task<TotalPenalitesDto?> RecupereLeTotalPenalitesMembreAsync(int membreId)
        => await GetAsync<TotalPenalitesDto>($"api/membres/{membreId}/penalites/total");

    private async Task<T?> GetAsync<T>(string endpoint)
    {
        try
        {
            // NoStore : contourne le cache navigateur pour toujours relire l'état frais du serveur
            // (sinon la liste peut rester périmée après une création / un retour).
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

    private async Task<HttpResponseMessage> PostRawAsync(string endpoint, dynamic data)
    {
        using var content = JsonContent.Create(data);
        return await httpClient.PostAsync(endpoint, content);
    }
}
