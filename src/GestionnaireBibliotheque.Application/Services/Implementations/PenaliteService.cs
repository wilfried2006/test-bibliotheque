using AutoMapper;
using GestionnaireBibliotheque.Application.DTOs.Responses;
using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Enums;
using GestionnaireBibliotheque.Domain.Interfaces;

namespace GestionnaireBibliotheque.Application.Services;

public class PenaliteService(
    IPenaliteRepository repository,
    IEmpruntRepository empruntRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IPenaliteService
{
    public async Task<IReadOnlyList<PenaliteResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // Pénalités déjà enregistrées (exemplaire rendu en retard) → statut « À payer ».
        var persistees = mapper.Map<IReadOnlyList<PenaliteResponse>>(await repository.ListAsync(cancellationToken));

        // Emprunts encore en cours et hors délai → pénalité « En cours »,
        // montant calculé à la volée (tarif journalier × jours de retard).
        var emprunts = await empruntRepository.ListAsync(cancellationToken);
        var enCours = emprunts
            .Where(e => e.EstEnRetard)
            .Select(e => new PenaliteResponse(
                Id: 0,
                MembreId: e.MembreId,
                ExemplaireId: e.ExemplaireId,
                JoursRetard: e.JoursRetard,
                Montant: Penalite.MontantRetard(e.JoursRetard).Valeur,
                DatePenalite: e.DateRetourPrevue,
                EmpruntId: e.Id,
                Statut: StatutPenalite.EnCours));

        return persistees.Concat(enCours).ToList();
    }

    public async Task<PenaliteResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var penalite = await repository.GetByIdAsync(id, cancellationToken);
        return penalite is null ? null : mapper.Map<PenaliteResponse>(penalite);
    }

    public async Task<bool> MarquerPayeAsync(int id, CancellationToken cancellationToken = default)
    {
        var penalite = await repository.GetByIdAsync(id, cancellationToken);
        if (penalite is null)
            return false;

        penalite.MarquerPaye();
        repository.Update(penalite);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<PenaliteResponse>> GetByMembreAsync(int membreId, CancellationToken cancellationToken = default)
    {
        var penalites = await repository.ListerParMembreAsync(membreId, cancellationToken);
        return mapper.Map<IReadOnlyList<PenaliteResponse>>(penalites);
    }

    public async Task<TotalPenalitesResponse> GetTotalEnCoursAsync(int membreId, CancellationToken cancellationToken = default)
    {
        // La politique (somme des montants + plafonnement) est portée par le domaine.
        var solde = Penalite.CalculerSolde(await repository.ListerParMembreAsync(membreId, cancellationToken));
        return new TotalPenalitesResponse(membreId, solde.Total.Valeur, solde.Plafond.Valeur, solde.EstPlafonne);
    }
}
