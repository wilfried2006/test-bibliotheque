using GestionnaireBibliotheque.BlazorWasm.Domain.Ouvrages;
using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Exemplaires;

public record CreerExemplaireCommand(int OuvrageId, string EtatDisponibilite);

public class RecupereLesExemplairesUseCase(IBackApiService apiService)
{
    public async Task<List<ExemplaireDto>?> ExecuteAsync()
        => await apiService.RecupereLesExemplairesAsync();
}

public class CreerExemplaireUseCase(IBackApiService apiService)
{
    public async Task<HttpResponseMessage> ExecuteAsync(CreerExemplaireCommand command)
    {
        var exemplaire = Exemplaire.Creer(command.OuvrageId, command.EtatDisponibilite);
        return await apiService.CreerExemplaireAsync(new { exemplaire.OuvrageId, exemplaire.EtatDisponibilite });
    }
}
