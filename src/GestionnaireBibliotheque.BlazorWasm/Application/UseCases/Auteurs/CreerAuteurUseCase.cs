using GestionnaireBibliotheque.BlazorWasm.Domain.Auteurs;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Auteurs;

public record CreerAuteurCommand(string Nom, string Prenom);

public class CreerAuteurUseCase(IBackApiService apiService)
{
    public async Task<HttpResponseMessage> ExecuteAsync(CreerAuteurCommand command)
    {
        // Valider le domaine
        var auteur = Auteur.Creer(command.Nom, command.Prenom);

        // Appeler l'API
        return await apiService.CreerAuteurAsync(new { auteur.Nom, auteur.Prenom });
    }
}
