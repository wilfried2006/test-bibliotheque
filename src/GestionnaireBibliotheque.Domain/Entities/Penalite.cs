using GestionnaireBibliotheque.Domain.Enums;
using GestionnaireBibliotheque.Domain.Exceptions;
using GestionnaireBibliotheque.Domain.ValueObjects;

namespace GestionnaireBibliotheque.Domain.Entities;

public class Penalite
{
    public const decimal TarifJournalier = 0.20m;

    public const decimal PlafondTotal = 10.00m;

    /// <summary>Plafond du total des pénalités en cours d'un membre.</summary>
    public static readonly Montant Plafond = Montant.De(PlafondTotal);

    public int Id { get; private set; }
    public int MembreId { get; private set; }
    public int ExemplaireId { get; private set; }
    public int EmpruntId { get; private set; }
    public int JoursRetard { get; private set; }
    public Montant Montant { get; private set; } = null!;
    public DateTime DatePenalite { get; private set; }
    public StatutPenalite Statut { get; private set; }

    private Penalite() { } // EF

    public static Penalite PourRetard(int membreId, int exemplaireId, int empruntId, int joursRetard, DateTime date)
    {
        if (joursRetard <= 0)
            throw new DomainException("Une pénalité de retard exige un nombre de jours de retard positif.");

        return new Penalite
        {
            MembreId = membreId,
            ExemplaireId = exemplaireId,
            EmpruntId = empruntId,
            JoursRetard = joursRetard,
            Montant = MontantRetard(joursRetard),
            DatePenalite = date,
            Statut = StatutPenalite.APayer
        };
    }

    public static Penalite Reconstituer(
        int id, int membreId, int exemplaireId, int empruntId, int joursRetard,
        decimal montant, DateTime datePenalite, StatutPenalite statut)
        => new()
        {
            Id = id,
            MembreId = membreId,
            ExemplaireId = exemplaireId,
            EmpruntId = empruntId,
            JoursRetard = joursRetard,
            Montant = Montant.De(montant),
            DatePenalite = datePenalite,
            Statut = statut
        };

    /// <summary>Marque la pénalité comme réglée (idempotent).</summary>
    public void MarquerPaye() => Statut = StatutPenalite.Paye;

    /// <summary>
    /// Montant d'une pénalité de retard pour un nombre de jours donné — règle unique du domaine
    /// (tarif journalier × jours), via le VO <see cref="Montant"/> (arrondi centime, non négatif).
    /// Utilisée aussi bien pour une pénalité persistée que pour un retard « en cours » calculé.
    /// </summary>
    public static Montant MontantRetard(int joursRetard)
        => joursRetard <= 0 ? Montant.Zero : Montant.De(TarifJournalier * joursRetard);

    /// <summary>
    /// Politique de pénalité : solde en cours d'un membre = somme des montants (via le VO Montant),
    /// plafonnée à <see cref="Plafond"/>. Toute la règle vit dans le domaine.
    /// </summary>
    public static SoldePenalites CalculerSolde(IEnumerable<Penalite> penalites)
    {
        var brut = penalites.Aggregate(Montant.Zero, (acc, p) => acc + p.Montant);
        return new SoldePenalites(brut, brut.Plafonner(Plafond), Plafond);
    }
}
