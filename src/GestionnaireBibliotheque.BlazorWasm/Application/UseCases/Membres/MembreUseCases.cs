using GestionnaireBibliotheque.BlazorWasm.Domain.Membres;
using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Membres;

public record CreerMembreCommand(string Nom, string Prenom, string? Email, byte TypeAdherentId);

public class RecupereLesMembreUseCase(IBackApiService apiService)
{
    public async Task<List<MembreDto>?> ExecuteAsync()
        => await apiService.RecupereLesMembreAsync();
}

public class RecupereLesTypesAdherentUseCase(IBackApiService apiService)
{
    public async Task<List<TypeAdherentDto>?> ExecuteAsync()
        => await apiService.RecupereLesTypesAdherentAsync();
}

public class CreerMembreUseCase(IBackApiService apiService)
{
    public async Task<HttpResponseMessage> ExecuteAsync(CreerMembreCommand command)
    {
        var membre = Membre.Creer(command.Nom, command.Prenom, command.Email, command.TypeAdherentId);
        return await apiService.CreerMembreAsync(new
        {
            membre.Nom,
            membre.Prenom,
            Email = membre.Email?.Valeur,
            membre.TypeAdherentId
        });
    }
}
