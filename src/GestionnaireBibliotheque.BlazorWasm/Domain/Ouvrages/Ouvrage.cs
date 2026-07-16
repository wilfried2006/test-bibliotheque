namespace GestionnaireBibliotheque.BlazorWasm.Domain.Ouvrages;

public class Ouvrage
{
    public int Id { get; set; }
    public required string Titre { get; set; }
    public int AuteurId { get; set; }
    public int NombreExemplaires { get; set; }

    private Ouvrage() { }

    public static Ouvrage Creer(string titre, int auteurId, int nombreExemplaires = 0)
    {
        if (string.IsNullOrWhiteSpace(titre))
            throw new ArgumentException("Le titre est obligatoire", nameof(titre));
        if (auteurId <= 0)
            throw new ArgumentException("L'auteur est obligatoire", nameof(auteurId));
        if (nombreExemplaires < 0)
            throw new ArgumentException("Le nombre d'exemplaires doit être >= 0", nameof(nombreExemplaires));

        return new Ouvrage
        {
            Titre = titre,
            AuteurId = auteurId,
            NombreExemplaires = nombreExemplaires
        };
    }
}
