using GestionnaireBibliotheque.Domain.Enums;
using GestionnaireBibliotheque.Domain.Exceptions;

namespace GestionnaireBibliotheque.Domain.Entities;

public class Emprunt
{
    public int Id { get; private set; }
    public int ExemplaireId { get; private set; }
    public int MembreId { get; private set; }
    public DateTime DateEmprunt { get; private set; }
    public DateTime DateRetourPrevue { get; private set; }
    public DateTime? DateRetourReel { get; private set; }
    public EtatEmprunt EtatEmprunt { get; private set; }

    public byte[] RowVersion { get; private set; } = [];

    private Emprunt() { }

    public static Emprunt Creer(int exemplaireId, int membreId, DateTime dateEmprunt, DateTime dateRetourPrevue)
    {
        if (exemplaireId <= 0)
            throw new DomainException("L'emprunt doit référencer un exemplaire.");
        if (membreId <= 0)
            throw new DomainException("L'emprunt doit référencer un membre.");
        if (dateRetourPrevue.Date <= dateEmprunt.Date)
            throw new DomainException("La date de retour prévue doit être postérieure à la date d'emprunt.");

        return new Emprunt
        {
            ExemplaireId = exemplaireId,
            MembreId = membreId,
            DateEmprunt = dateEmprunt,
            DateRetourPrevue = dateRetourPrevue,
            EtatEmprunt = EtatEmprunt.Actif
        };
    }

    public static Emprunt Reconstituer(
        int id, int exemplaireId, int membreId, DateTime dateEmprunt, DateTime dateRetourPrevue,
        DateTime? dateRetourReel, EtatEmprunt etatEmprunt, byte[] rowVersion) => new()
        {
            Id = id,
            ExemplaireId = exemplaireId,
            MembreId = membreId,
            DateEmprunt = dateEmprunt,
            DateRetourPrevue = dateRetourPrevue,
            DateRetourReel = dateRetourReel,
            EtatEmprunt = etatEmprunt,
            RowVersion = rowVersion
        };

    public void Retourner(DateTime dateRetour)
    {
        if (DateRetourReel is not null)
            throw new EmpruntDejaRetourneException(Id);
        if (dateRetour < DateEmprunt)
            throw new DomainException(
                $"La date de retour ({dateRetour:d}) ne peut pas précéder la date d'emprunt ({DateEmprunt:d}).");

        DateRetourReel = dateRetour;
        EtatEmprunt = dateRetour.Date > DateRetourPrevue.Date ? EtatEmprunt.Retard : EtatEmprunt.Cloture;
    }

    public bool EstEnRetard => DateRetourReel is null && DateTime.UtcNow > DateRetourPrevue;

    public int JoursRetard
    {
        get
        {
            var reference = DateRetourReel ?? DateTime.UtcNow;
            var jours = (reference.Date - DateRetourPrevue.Date).Days;
            return jours > 0 ? jours : 0;
        }
    }

    public TimeSpan DureeEmprunt => (DateRetourReel ?? DateTime.UtcNow) - DateEmprunt;
}
