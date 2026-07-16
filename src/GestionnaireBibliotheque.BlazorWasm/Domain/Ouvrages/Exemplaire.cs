namespace GestionnaireBibliotheque.BlazorWasm.Domain.Ouvrages;

public class Exemplaire
{
    public int Id { get; set; }
    public int OuvrageId { get; set; }
    public required Guid CodeBarre { get; set; }
    public required string EtatDisponibilite { get; set; }

    private Exemplaire() { }

    public static Exemplaire Creer(int ouvrageId, string etatDisponibilite = "Disponible")
    {
        if (ouvrageId <= 0)
            throw new ArgumentException("L'ouvrage est obligatoire", nameof(ouvrageId));
        if (!IsEtatValide(etatDisponibilite))
            throw new ArgumentException("L'état doit être 'Disponible' ou 'Emprunte'", nameof(etatDisponibilite));

        return new Exemplaire
        {
            OuvrageId = ouvrageId,
            CodeBarre = Guid.NewGuid(),
            EtatDisponibilite = etatDisponibilite
        };
    }

    private static bool IsEtatValide(string etat) => etat is "Disponible" or "Emprunte";
}
