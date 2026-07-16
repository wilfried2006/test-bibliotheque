using GestionnaireBibliotheque.BlazorWasm.Domain.Emprunts;
using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Emprunts;

public record CreerEmpruntCommand(int OuvrageId, int MembreId);
public record RetournerEmpruntCommand(int EmpruntId);

public class RecupereLesEmpruntUseCase(IBackApiService apiService)
{
    public async Task<List<EmpruntDto>?> ExecuteAsync()
        => await apiService.RecupereLesEmpruntAsync();
}

public class CreerEmpruntUseCase(IBackApiService apiService)
{
    public async Task<HttpResponseMessage> ExecuteAsync(CreerEmpruntCommand command)
    {
        var dateEmprunt = DateTime.Today;
        var dateRetourPrevue = dateEmprunt.AddDays(14);
        var emprunt = Emprunt.Creer(1, command.MembreId, dateEmprunt, dateRetourPrevue);

        return await apiService.CreerEmpruntAsync(new { command.OuvrageId, command.MembreId });
    }
}

public class RetournerEmpruntUseCase(IBackApiService apiService)
{
    public async Task<HttpResponseMessage> ExecuteAsync(RetournerEmpruntCommand command)
        => await apiService.RetournerEmpruntAsync(command.EmpruntId);
}
