using GestionnaireBibliotheque.BlazorWasm.Models;

namespace GestionnaireBibliotheque.BlazorWasm.Services;

public interface IBackApiService
{
    // Auteurs
    Task<List<AuteurDto>?> RecupereLesAuteursAsync();
    Task<HttpResponseMessage> CreerAuteurAsync(CreateAuteurRequest request);

    // Ouvrages
    Task<List<OuvrageDto>?> RecupereLesOuvragesAsync();
    Task<HttpResponseMessage> CreerOuvrageAsync(CreateOuvrageRequest request);

    // Exemplaires
    Task<List<ExemplaireDto>?> RecupereLesExemplairesAsync();
    Task<HttpResponseMessage> CreerExemplaireAsync(CreateExemplaireRequest request);

    // Membres
    Task<List<MembreDto>?> RecupereLesMembreAsync();
    Task<HttpResponseMessage> CreerMembreAsync(CreateMembreRequest request);

    // Types d'adhérent
    Task<List<TypeAdherentDto>?> RecupereLesTypesAdherentAsync();

    // Emprunts
    Task<List<EmpruntDto>?> RecupereLesEmpruntAsync();
    Task<HttpResponseMessage> CreerEmpruntAsync(CreateEmpruntRequest request);
    Task<HttpResponseMessage> RetournerEmpruntAsync(int empruntId);

    // Pénalités
    Task<List<PenaliteDto>?> RecupereLespenalitesAsync();
    Task<HttpResponseMessage> MarquerPenalitePayeAsync(int penaliteId);
}
