using GestionnaireBibliotheque.Domain.Exceptions;

namespace GestionnaireBibliotheque.Domain.Entities;

public class TypeAdherent
{
    public byte Id { get; private set; }
    public string Libelle { get; private set; } = null!;

    public byte NombreOuvragesMax { get; private set; }

    public byte DureeEmpruntJours { get; private set; }

    private TypeAdherent() { } // EF

    public static TypeAdherent Creer(string libelle, byte nombreOuvragesMax, byte dureeEmpruntJours)
    {
        var type = new TypeAdherent();
        type.Modifier(libelle, nombreOuvragesMax, dureeEmpruntJours);
        return type;
    }

    public static TypeAdherent Reconstituer(byte id, string libelle, byte nombreOuvragesMax, byte dureeEmpruntJours)
        => new() { Id = id, Libelle = libelle, NombreOuvragesMax = nombreOuvragesMax, DureeEmpruntJours = dureeEmpruntJours };

    public void Modifier(string libelle, byte nombreOuvragesMax, byte dureeEmpruntJours)
    {
        if (string.IsNullOrWhiteSpace(libelle))
            throw new DomainException("Le libellé du type d'adhérent est obligatoire.");
        if (nombreOuvragesMax == 0)
            throw new DomainException("Le nombre maximum d'ouvrages doit être supérieur à 0.");
        if (dureeEmpruntJours == 0)
            throw new DomainException("La durée d'emprunt doit être supérieure à 0.");

        Libelle = libelle.Trim();
        NombreOuvragesMax = nombreOuvragesMax;
        DureeEmpruntJours = dureeEmpruntJours;
    }
}
