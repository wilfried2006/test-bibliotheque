using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.Application.Services;

public interface IExemplaireService
{
    Task<IReadOnlyList<ExemplaireResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ExemplaireResponse> CreateAsync(CreateExemplaireRequest dto, CancellationToken cancellationToken = default);
}
