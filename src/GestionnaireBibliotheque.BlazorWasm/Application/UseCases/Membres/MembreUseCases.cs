using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Membres;

public record CreerMembreCommand(string Nom, string Prenom, string? Email, byte TypeAdherentId);

public class RecupereLesMembreUseCase(IBackApiService apiService)
{
    public Task<List<MembreDto>?> ExecuteAsync() => apiService.RecupereLesMembreAsync();
}

public class RecupereLesTypesAdherentUseCase(IBackApiService apiService)
{
    public Task<List<TypeAdherentDto>?> ExecuteAsync() => apiService.RecupereLesTypesAdherentAsync();
}

public class CreerMembreUseCase(IBackApiService apiService)
{
    public Task<HttpResponseMessage> ExecuteAsync(CreerMembreCommand command)
        => apiService.CreerMembreAsync(new CreateMembreRequest(command.Nom, command.Prenom, command.Email, command.TypeAdherentId));
}
