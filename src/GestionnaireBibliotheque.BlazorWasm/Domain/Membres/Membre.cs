namespace GestionnaireBibliotheque.BlazorWasm.Domain.Membres;

public class Membre
{
    public int Id { get; set; }
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public Email? Email { get; set; }
    public byte TypeAdherentId { get; set; }

    private Membre() { }

    public static Membre Creer(string nom, string prenom, string? email, byte typeAdherentId)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new ArgumentException("Le nom est obligatoire", nameof(nom));
        if (string.IsNullOrWhiteSpace(prenom))
            throw new ArgumentException("Le prénom est obligatoire", nameof(prenom));
        if (typeAdherentId <= 0)
            throw new ArgumentException("Le type d'adhérent est obligatoire", nameof(typeAdherentId));

        return new Membre
        {
            Nom = nom,
            Prenom = prenom,
            Email = Email.TryCreate(email),
            TypeAdherentId = typeAdherentId
        };
    }

    public string NomComplet => $"{Nom} {Prenom}";
}
