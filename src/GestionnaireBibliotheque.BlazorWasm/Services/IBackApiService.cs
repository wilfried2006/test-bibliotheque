using GestionnaireBibliotheque.BlazorWasm.Models;

namespace GestionnaireBibliotheque.BlazorWasm.Services;

public interface IBackApiService
{
    // Auteurs
    Task<List<AuteurDto>?> RecupereLesAuteursAsync();
    Task<HttpResponseMessage> CreerAuteurAsync(dynamic request);

    // Ouvrages
    Task<List<OuvrageDto>?> RecupereLesOuvragesAsync();
    Task<HttpResponseMessage> CreerOuvrageAsync(dynamic request);

    // Exemplaires
    Task<List<ExemplaireDto>?> RecupereLesExemplairesAsync();
    Task<HttpResponseMessage> CreerExemplaireAsync(dynamic request);

    // Membres
    Task<List<MembreDto>?> RecupereLesMembreAsync();
    Task<HttpResponseMessage> CreerMembreAsync(dynamic request);

    // Types d'adhérent
    Task<List<TypeAdherentDto>?> RecupereLesTypesAdherentAsync();

    // Emprunts
    Task<List<EmpruntDto>?> RecupereLesEmpruntAsync();
    Task<HttpResponseMessage> CreerEmpruntAsync(dynamic request);
    Task<HttpResponseMessage> RetournerEmpruntAsync(int empruntId);

    // Pénalités
    Task<List<PenaliteDto>?> RecupereLespenalitesAsync();
    Task<List<PenaliteDto>?> RecupereLePenalitesMembreAsync(int membreId);
    Task<TotalPenalitesDto?> RecupereLeTotalPenalitesMembreAsync(int membreId);
}
