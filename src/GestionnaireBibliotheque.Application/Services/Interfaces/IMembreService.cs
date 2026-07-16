using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.Application.Services;

public interface IMembreService
{
    Task<IReadOnlyList<MembreResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MembreResponse> CreateAsync(CreateMembreRequest dto, CancellationToken cancellationToken = default);
}
