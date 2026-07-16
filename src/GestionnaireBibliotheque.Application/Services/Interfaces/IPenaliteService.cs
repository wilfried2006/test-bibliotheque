using GestionnaireBibliotheque.Application.DTOs.Responses;

namespace GestionnaireBibliotheque.Application.Services;

public interface IPenaliteService
{
    /// <summary>Liste les pénalités d'un membre.</summary>
    Task<IReadOnlyList<PenaliteResponse>> GetByMembreAsync(int membreId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Total des pénalités en cours d'un membre, plafonné à
    /// <see cref="GestionnaireBibliotheque.Domain.Entities.Penalite.PlafondTotal"/> €.
    /// </summary>
    Task<TotalPenalitesResponse> GetTotalEnCoursAsync(int membreId, CancellationToken cancellationToken = default);
}
