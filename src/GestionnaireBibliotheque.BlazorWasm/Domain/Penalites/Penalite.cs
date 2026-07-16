namespace GestionnaireBibliotheque.BlazorWasm.Domain.Penalites;

public class Penalite
{
    public int Id { get; set; }
    public int MembreId { get; set; }
    public int ExemplaireId { get; set; }
    public int JoursRetard { get; set; }
    public required Montant Montant { get; set; }
    public DateTime DatePenalite { get; set; }

    private Penalite() { }

    public static Penalite Creer(int membreId, int exemplaireId, int joursRetard, decimal montant, DateTime datePenalite)
    {
        if (membreId <= 0)
            throw new ArgumentException("Le membre est obligatoire", nameof(membreId));
        if (exemplaireId <= 0)
            throw new ArgumentException("L'exemplaire est obligatoire", nameof(exemplaireId));
        if (joursRetard <= 0)
            throw new ArgumentException("Les jours de retard doivent être > 0", nameof(joursRetard));

        return new Penalite
        {
            MembreId = membreId,
            ExemplaireId = exemplaireId,
            JoursRetard = joursRetard,
            Montant = Montant.Create(montant),
            DatePenalite = datePenalite
        };
    }
}
