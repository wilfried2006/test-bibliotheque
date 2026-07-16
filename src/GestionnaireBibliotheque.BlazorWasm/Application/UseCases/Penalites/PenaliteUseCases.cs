using GestionnaireBibliotheque.BlazorWasm.Models;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Penalites;

public class RecupereLePenalitesMembreUseCase(IBackApiService apiService)
{
    public async Task<List<PenaliteDto>?> ExecuteAsync(int membreId)
        => await apiService.RecupereLePenalitesMembreAsync(membreId);
}

public class RecupereLeTotalPenalitesMembreUseCase(IBackApiService apiService)
{
    public async Task<TotalPenalitesDto?> ExecuteAsync(int membreId)
        => await apiService.RecupereLeTotalPenalitesMembreAsync(membreId);
}

public class RecupereLespenalitesUseCase(IBackApiService apiService)
{
    public async Task<List<PenaliteDto>?> ExecuteAsync()
        => await apiService.RecupereLespenalitesAsync();
}
