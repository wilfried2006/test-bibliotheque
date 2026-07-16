using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.Application.Services;

public interface IOuvrageService
{
    Task<IReadOnlyList<OuvrageResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OuvrageResponse> CreateAsync(CreateOuvrageRequest dto, CancellationToken cancellationToken = default);
}
