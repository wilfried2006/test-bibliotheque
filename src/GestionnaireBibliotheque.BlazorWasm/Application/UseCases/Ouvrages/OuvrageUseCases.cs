using GestionnaireBibliotheque.BlazorWasm.Domain.Ouvrages;
using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Ouvrages;

public record CreerOuvrageCommand(string Titre, int AuteurId, int NombreExemplaires);

public class RecupereLesOuvragesUseCase(IBackApiService apiService)
{
    public async Task<List<OuvrageDto>?> ExecuteAsync()
        => await apiService.RecupereLesOuvragesAsync();
}

public class CreerOuvrageUseCase(IBackApiService apiService)
{
    public async Task<HttpResponseMessage> ExecuteAsync(CreerOuvrageCommand command)
    {
        var ouvrage = Ouvrage.Creer(command.Titre, command.AuteurId, command.NombreExemplaires);
        return await apiService.CreerOuvrageAsync(new { ouvrage.Titre, ouvrage.AuteurId, ouvrage.NombreExemplaires });
    }
}
