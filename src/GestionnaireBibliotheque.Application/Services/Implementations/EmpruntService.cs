using AutoMapper;
using GestionnaireBibliotheque.Application.DTOs.Requests;
using GestionnaireBibliotheque.Application.DTOs.Responses;
using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Enums;
using GestionnaireBibliotheque.Domain.Exceptions;
using GestionnaireBibliotheque.Domain.Interfaces;
using GestionnaireBibliotheque.Domain.Services;

namespace GestionnaireBibliotheque.Application.Services;

public class EmpruntService(
    IEmpruntRepository empruntRepository,
    IMembreRepository membreRepository,
    ITypeAdherentRepository typeAdherentRepository,
    IExemplaireRepository exemplaireRepository,
    IPenaliteRepository penaliteRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IEmpruntService
{
    public async Task<IReadOnlyList<EmpruntResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        => mapper.Map<IReadOnlyList<EmpruntResponse>>(await empruntRepository.ListAsync(cancellationToken));

    /// <summary>Emprunt d'un ouvrage (quota + disponibilité, via le service de domaine).</summary>
    public async Task<EmpruntResponse> CreateAsync(CreateEmpruntRequest dto, CancellationToken cancellationToken = default)
    {
        var membre = await membreRepository.GetByIdAsync(dto.MembreId, cancellationToken)
            ?? throw new RessourceIntrouvableException($"Membre {dto.MembreId} introuvable.");
        var type = await typeAdherentRepository.GetByIdAsync(membre.TypeAdherentId, cancellationToken)
            ?? throw new RessourceIntrouvableException($"Type d'adhérent {membre.TypeAdherentId} introuvable.");

        var nombreActifs = await empruntRepository.CompterEmpruntsActifsAsync(dto.MembreId, cancellationToken);

        var exemplaire = await exemplaireRepository.PremierDisponibleAsync(dto.OuvrageId, cancellationToken)
            ?? throw new AucunExemplaireDisponibleException(dto.OuvrageId);

        // Politique d'emprunt (règles portées par le domaine) — mute l'exemplaire.
        var emprunt = ServiceEmprunt.Emprunter(membre, type, nombreActifs, exemplaire, DateTime.UtcNow.Date);
        exemplaireRepository.Update(exemplaire);
        await empruntRepository.AddAsync(emprunt, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return mapper.Map<EmpruntResponse>(emprunt);
    }

    public async Task<RetourEmpruntResponse?> RetournerAsync(
        int empruntId, DateTime? dateRetour = null, CancellationToken cancellationToken = default)
    {
        var emprunt = await empruntRepository.GetByIdAsync(empruntId, cancellationToken);
        if (emprunt is null)
            return null;

        var dateEffective = dateRetour is { } valeur ? NormaliserUtc(valeur) : DateTime.UtcNow;

        // Invariant métier (lève EmpruntDejaRetourneException / DomainException).
        emprunt.Retourner(dateEffective);
        empruntRepository.Update(emprunt);

        // Libère l'exemplaire.
        var exemplaire = await exemplaireRepository.GetByIdAsync(emprunt.ExemplaireId, cancellationToken);
        if (exemplaire is not null)
        {
            exemplaire.Rendre();
            exemplaireRepository.Update(exemplaire);
        }

        // Pénalité en cas de retard.
        decimal montantPenalite = 0m;
        if (emprunt.EtatEmprunt == EtatEmprunt.Retard)
        {
            var penalite = Penalite.PourRetard(emprunt.MembreId, emprunt.ExemplaireId, emprunt.JoursRetard, dateEffective);
            montantPenalite = penalite.Montant.Valeur;
            await penaliteRepository.AddAsync(penalite, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RetourEmpruntResponse(
            emprunt.Id, emprunt.ExemplaireId, emprunt.MembreId,
            emprunt.DateRetourReel!.Value, emprunt.DateRetourPrevue,
            emprunt.EtatEmprunt == EtatEmprunt.Retard, emprunt.JoursRetard, montantPenalite, emprunt.EtatEmprunt);
    }

    private static DateTime NormaliserUtc(DateTime valeur) => valeur.Kind switch
    {
        DateTimeKind.Utc => valeur,
        DateTimeKind.Local => valeur.ToUniversalTime(),
        _ => DateTime.SpecifyKind(valeur, DateTimeKind.Utc)
    };
}
