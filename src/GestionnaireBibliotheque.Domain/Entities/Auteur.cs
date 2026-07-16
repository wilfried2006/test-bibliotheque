using GestionnaireBibliotheque.Domain.Exceptions;

namespace GestionnaireBibliotheque.Domain.Entities;

/// <summary>Entité : un auteur.</summary>
public class Auteur
{
    public int Id { get; private set; }
    public string Nom { get; private set; } = null!;
    public string Prenom { get; private set; } = null!;

    private Auteur() { } // EF

    public static Auteur Creer(string nom, string prenom)
    {
        var auteur = new Auteur();
        auteur.Renommer(nom, prenom);
        return auteur;
    }

    public static Auteur Reconstituer(int id, string nom, string prenom)
        => new() { Id = id, Nom = nom, Prenom = prenom };

    public void Renommer(string nom, string prenom)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new DomainException("Le nom de l'auteur est obligatoire.");
        if (string.IsNullOrWhiteSpace(prenom))
            throw new DomainException("Le prénom de l'auteur est obligatoire.");

        Nom = nom.Trim();
        Prenom = prenom.Trim();
    }
}
