namespace GestionnaireBibliotheque.BlazorWasm.Domain.Auteurs;

public class Auteur
{
    public int Id { get; set; }
    public required string Nom { get; set; }
    public required string Prenom { get; set; }

    private Auteur() { }

    public static Auteur Creer(string nom, string prenom)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new ArgumentException("Le nom est obligatoire", nameof(nom));
        if (string.IsNullOrWhiteSpace(prenom))
            throw new ArgumentException("Le prénom est obligatoire", nameof(prenom));

        return new Auteur { Nom = nom, Prenom = prenom };
    }

    public string NomComplet => $"{Nom} {Prenom}";
}
