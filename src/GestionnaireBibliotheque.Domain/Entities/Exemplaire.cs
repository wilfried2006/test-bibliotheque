using GestionnaireBibliotheque.Domain.Enums;
using GestionnaireBibliotheque.Domain.Exceptions;

namespace GestionnaireBibliotheque.Domain.Entities;

/// <summary>Entité : un exemplaire physique d'un ouvrage.</summary>
public class Exemplaire
{
    public int Id { get; private set; }
    public int OuvrageId { get; private set; }
    public Guid CodeBarre { get; private set; }

    public EtatDisponibilite EtatDisponibilite { get; private set; }

    private Exemplaire() { } // EF

    public static Exemplaire Creer(int ouvrageId, EtatDisponibilite etat = EtatDisponibilite.Disponible)
    {
        if (ouvrageId <= 0)
            throw new DomainException("Un exemplaire doit être rattaché à un ouvrage.");

        return new Exemplaire { OuvrageId = ouvrageId, EtatDisponibilite = etat };
    }

    public static Exemplaire Reconstituer(int id, int ouvrageId, Guid codeBarre, EtatDisponibilite etat)
        => new() { Id = id, OuvrageId = ouvrageId, CodeBarre = codeBarre, EtatDisponibilite = etat };

    public bool EstDisponible => EtatDisponibilite == EtatDisponibilite.Disponible;

   public void Emprunter()
    {
        if (!EstDisponible)
            throw new ConflitMetierException($"L'exemplaire {Id} n'est pas disponible.");
        EtatDisponibilite = EtatDisponibilite.Emprunte;
    }

    public void Rendre() => EtatDisponibilite = EtatDisponibilite.Disponible;
}
