using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Emprunts;

public record CreerEmpruntCommand(int OuvrageId, int MembreId);
public record RetournerEmpruntCommand(int EmpruntId);

public class RecupereLesEmpruntUseCase(IBackApiService apiService)
{
    public Task<List<EmpruntDto>?> ExecuteAsync() => apiService.RecupereLesEmpruntAsync();
}

public class CreerEmpruntUseCase(IBackApiService apiService)
{
    // Le serveur choisit l'exemplaire disponible et calcule l'échéance selon le type d'adhérent.
    public Task<HttpResponseMessage> ExecuteAsync(CreerEmpruntCommand command)
        => apiService.CreerEmpruntAsync(new CreateEmpruntRequest(command.OuvrageId, command.MembreId));
}

public class RetournerEmpruntUseCase(IBackApiService apiService)
{
    public Task<HttpResponseMessage> ExecuteAsync(RetournerEmpruntCommand command)
        => apiService.RetournerEmpruntAsync(command.EmpruntId);
}
