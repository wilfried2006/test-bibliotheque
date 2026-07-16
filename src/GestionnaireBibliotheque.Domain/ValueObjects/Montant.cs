using GestionnaireBibliotheque.Domain.Common;
using GestionnaireBibliotheque.Domain.Exceptions;

namespace GestionnaireBibliotheque.Domain.ValueObjects;

/// <summary>Value Object : montant monétaire (en euros), positif ou nul, arrondi au centime.</summary>
public sealed class Montant : ValueObject
{
    public decimal Valeur { get; }

    private Montant(decimal valeur) => Valeur = valeur;

    public static readonly Montant Zero = new(0m);

    public static Montant De(decimal valeur)
    {
        if (valeur < 0)
            throw new DomainException("Un montant ne peut pas être négatif.");

        return new Montant(decimal.Round(valeur, 2));
    }

    public Montant Plafonner(Montant plafond) => Valeur <= plafond.Valeur ? this : plafond;

    public static Montant operator +(Montant a, Montant b) => new(a.Valeur + b.Valeur);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Valeur;
    }

    public override string ToString() => Valeur.ToString("0.00");
}
