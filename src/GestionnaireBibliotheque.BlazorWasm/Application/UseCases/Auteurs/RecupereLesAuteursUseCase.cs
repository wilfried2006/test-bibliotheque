using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Auteurs;

public class RecupereLesAuteursUseCase(IBackApiService apiService)
{
    public async Task<List<AuteurDto>?> ExecuteAsync()
        => await apiService.RecupereLesAuteursAsync();
}
