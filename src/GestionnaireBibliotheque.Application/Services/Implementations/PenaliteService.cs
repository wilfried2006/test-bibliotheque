using AutoMapper;
using GestionnaireBibliotheque.Application.DTOs.Responses;
using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Interfaces;

namespace GestionnaireBibliotheque.Application.Services;

public class PenaliteService(IPenaliteRepository repository, IMapper mapper) : IPenaliteService
{
    public async Task<IReadOnlyList<PenaliteResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var penalites = await repository.ListAsync(cancellationToken);
        return mapper.Map<IReadOnlyList<PenaliteResponse>>(penalites);
    }

    public async Task<PenaliteResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var penalite = await repository.GetByIdAsync(id, cancellationToken);
        return penalite is null ? null : mapper.Map<PenaliteResponse>(penalite);
    }

    public async Task<IReadOnlyList<PenaliteResponse>> GetByMembreAsync(int membreId, CancellationToken cancellationToken = default)
    {
        var penalites = await repository.ListerParMembreAsync(membreId, cancellationToken);
        return mapper.Map<IReadOnlyList<PenaliteResponse>>(penalites);
    }

    public async Task<TotalPenalitesResponse> GetTotalEnCoursAsync(int membreId, CancellationToken cancellationToken = default)
    {
        var penalites = await repository.ListerParMembreAsync(membreId, cancellationToken);

        var brut = penalites.Aggregate(0m, (total, p) => total + p.Montant.Valeur);
        var total = Math.Min(brut, Penalite.PlafondTotal);

        return new TotalPenalitesResponse(membreId, total, Penalite.PlafondTotal, Plafonne: brut > Penalite.PlafondTotal);
    }
}
