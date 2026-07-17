using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Penalites;

public class RecupereLespenalitesUseCase(IBackApiService apiService)
{
    public async Task<List<PenaliteDto>?> ExecuteAsync()
        => await apiService.RecupereLespenalitesAsync();
}

public class MarquerPenalitePayeUseCase(IBackApiService apiService)
{
    public async Task<HttpResponseMessage> ExecuteAsync(int penaliteId)
        => await apiService.MarquerPenalitePayeAsync(penaliteId);
}
