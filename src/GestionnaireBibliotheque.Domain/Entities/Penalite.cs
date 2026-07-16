using GestionnaireBibliotheque.Domain.Exceptions;
using GestionnaireBibliotheque.Domain.ValueObjects;

namespace GestionnaireBibliotheque.Domain.Entities;

public class Penalite
{
    public const decimal TarifJournalier = 0.20m;

    public const decimal PlafondTotal = 10.00m;

    public int Id { get; private set; }
    public int MembreId { get; private set; }
    public int ExemplaireId { get; private set; }
    public int JoursRetard { get; private set; }
    public Montant Montant { get; private set; } = null!;
    public DateTime DatePenalite { get; private set; }

    private Penalite() { } // EF

    public static Penalite PourRetard(int membreId, int exemplaireId, int joursRetard, DateTime date)
    {
        if (joursRetard <= 0)
            throw new DomainException("Une pénalité de retard exige un nombre de jours de retard positif.");

        return new Penalite
        {
            MembreId = membreId,
            ExemplaireId = exemplaireId,
            JoursRetard = joursRetard,
            Montant = Montant.De(TarifJournalier * joursRetard),
            DatePenalite = date
        };
    }

    public static Penalite Reconstituer(int id, int membreId, int exemplaireId, int joursRetard, decimal montant, DateTime datePenalite)
        => new()
        {
            Id = id,
            MembreId = membreId,
            ExemplaireId = exemplaireId,
            JoursRetard = joursRetard,
            Montant = Montant.De(montant),
            DatePenalite = datePenalite
        };
}
