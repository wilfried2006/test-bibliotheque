using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.Application.Services;

public interface ITypeAdherentService
{
    Task<IReadOnlyList<TypeAdherentResponse>> GetAllAsync(CancellationToken cancellationToken = default);
}
