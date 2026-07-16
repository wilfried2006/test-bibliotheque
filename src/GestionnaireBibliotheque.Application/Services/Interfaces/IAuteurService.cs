using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.Application.Services;

public interface IAuteurService
{
    Task<IReadOnlyList<AuteurResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AuteurResponse> CreateAsync(CreateAuteurRequest dto, CancellationToken cancellationToken = default);
}
