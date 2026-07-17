using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Ouvrages;

public record CreerOuvrageCommand(string Titre, int AuteurId, int NombreExemplaires);

public class RecupereLesOuvragesUseCase(IBackApiService apiService)
{
    public Task<List<OuvrageDto>?> ExecuteAsync() => apiService.RecupereLesOuvragesAsync();
}

public class CreerOuvrageUseCase(IBackApiService apiService)
{
    public Task<HttpResponseMessage> ExecuteAsync(CreerOuvrageCommand command)
        => apiService.CreerOuvrageAsync(new CreateOuvrageRequest(command.Titre, command.AuteurId, command.NombreExemplaires));
}
