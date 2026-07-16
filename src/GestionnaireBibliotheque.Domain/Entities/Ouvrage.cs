using GestionnaireBibliotheque.Domain.Exceptions;

namespace GestionnaireBibliotheque.Domain.Entities;

public class Ouvrage
{
    public int Id { get; private set; }
    public string Titre { get; private set; } = null!;
    public int AuteurId { get; private set; }

    private Ouvrage() { } // EF

    public static Ouvrage Creer(string titre, int auteurId)
    {
        var ouvrage = new Ouvrage();
        ouvrage.Modifier(titre, auteurId);
        return ouvrage;
    }

    public void Modifier(string titre, int auteurId)
    {
        if (string.IsNullOrWhiteSpace(titre))
            throw new DomainException("Le titre de l'ouvrage est obligatoire.");
        if (auteurId <= 0)
            throw new DomainException("Un ouvrage doit être rattaché à un auteur.");

        Titre = titre.Trim();
        AuteurId = auteurId;
    }

    public static Ouvrage Reconstituer(int id, string titre, int auteurId)
        => new() { Id = id, Titre = titre, AuteurId = auteurId };

    public Exemplaire CreerExemplaire() => Exemplaire.Creer(Id);
}
