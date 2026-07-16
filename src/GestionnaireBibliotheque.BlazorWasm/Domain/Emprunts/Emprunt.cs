namespace GestionnaireBibliotheque.BlazorWasm.Domain.Emprunts;

public class Emprunt
{
    public int Id { get; set; }
    public int ExemplaireId { get; set; }
    public int MembreId { get; set; }
    public DateTime DateEmprunt { get; set; }
    public DateTime DateRetourPrevue { get; set; }
    public DateTime? DateRetourReel { get; set; }
    public required string EtatEmprunt { get; set; }

    private Emprunt() { }

    public static Emprunt Creer(int exemplaireId, int membreId, DateTime dateEmprunt, DateTime dateRetourPrevue)
    {
        if (exemplaireId <= 0)
            throw new ArgumentException("L'exemplaire est obligatoire", nameof(exemplaireId));
        if (membreId <= 0)
            throw new ArgumentException("Le membre est obligatoire", nameof(membreId));
        if (dateRetourPrevue <= dateEmprunt)
            throw new ArgumentException("La date de retour prévue doit être après la date d'emprunt", nameof(dateRetourPrevue));

        return new Emprunt
        {
            ExemplaireId = exemplaireId,
            MembreId = membreId,
            DateEmprunt = dateEmprunt,
            DateRetourPrevue = dateRetourPrevue,
            EtatEmprunt = "Actif"
        };
    }

    public bool EstEnRetard => DateRetourReel.HasValue && DateRetourReel > DateRetourPrevue;
    public int JoursRetard => EstEnRetard ? (int)(DateRetourReel!.Value.Date - DateRetourPrevue.Date).TotalDays : 0;
}
