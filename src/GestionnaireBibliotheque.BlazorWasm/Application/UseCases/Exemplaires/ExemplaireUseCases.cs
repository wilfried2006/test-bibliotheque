using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Exemplaires;

public record CreerExemplaireCommand(int OuvrageId, string EtatDisponibilite);

public class RecupereLesExemplairesUseCase(IBackApiService apiService)
{
    public Task<List<ExemplaireDto>?> ExecuteAsync() => apiService.RecupereLesExemplairesAsync();
}

public class CreerExemplaireUseCase(IBackApiService apiService)
{
    public Task<HttpResponseMessage> ExecuteAsync(CreerExemplaireCommand command)
        => apiService.CreerExemplaireAsync(new CreateExemplaireRequest(command.OuvrageId, command.EtatDisponibilite));
}
