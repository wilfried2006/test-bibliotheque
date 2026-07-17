using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Auteurs;

public record CreerAuteurCommand(string Nom, string Prenom);

public class CreerAuteurUseCase(IBackApiService apiService)
{
    public Task<HttpResponseMessage> ExecuteAsync(CreerAuteurCommand command)
        => apiService.CreerAuteurAsync(new CreateAuteurRequest(command.Nom, command.Prenom));
}
