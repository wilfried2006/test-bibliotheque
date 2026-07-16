using GestionnaireBibliotheque.Domain.Exceptions;
using GestionnaireBibliotheque.Domain.ValueObjects;

namespace GestionnaireBibliotheque.Domain.Entities;

/// <summary>Entité : un adhérent.</summary>
public class Membre
{
    public int Id { get; private set; }
    public string Nom { get; private set; } = null!;
    public string Prenom { get; private set; } = null!;
    public Email? Email { get; private set; }
    public DateTime DateInscription { get; private set; }
    public byte TypeAdherentId { get; private set; }

    private Membre() { }  

    public static Membre Creer(string nom, string prenom, string? email, byte typeAdherentId)
    {
        var membre = new Membre { DateInscription = DateTime.UtcNow };
        membre.ModifierIdentite(nom, prenom, email);
        membre.ChangerType(typeAdherentId);
        return membre;
    }

    public static Membre Reconstituer(int id, string nom, string prenom, string? email, DateTime dateInscription, byte typeAdherentId)
        => new()
        {
            Id = id,
            Nom = nom,
            Prenom = prenom,
            Email = string.IsNullOrWhiteSpace(email) ? null : ValueObjects.Email.Creer(email),
            DateInscription = dateInscription,
            TypeAdherentId = typeAdherentId
        };

    public void ModifierIdentite(string nom, string prenom, string? email)
    {
        if (string.IsNullOrWhiteSpace(nom))
            throw new DomainException("Le nom du membre est obligatoire.");
        if (string.IsNullOrWhiteSpace(prenom))
            throw new DomainException("Le prénom du membre est obligatoire.");

        Nom = nom.Trim();
        Prenom = prenom.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : Email.Creer(email);
    }

    public void ChangerType(byte typeAdherentId)
    {
        if (typeAdherentId == 0)
            throw new DomainException("Le membre doit avoir un type d'adhérent.");
        TypeAdherentId = typeAdherentId;
    }
}
