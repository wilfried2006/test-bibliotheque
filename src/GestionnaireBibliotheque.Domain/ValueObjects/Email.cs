using System.Text.RegularExpressions;
using GestionnaireBibliotheque.Domain.Common;
using GestionnaireBibliotheque.Domain.Exceptions;

namespace GestionnaireBibliotheque.Domain.ValueObjects;

/// <summary>Value Object : adresse e-mail valide.</summary>
public sealed partial class Email : ValueObject
{
    public string Valeur { get; }

    private Email(string valeur) => Valeur = valeur;

    public static Email Creer(string? valeur)
    {
        if (string.IsNullOrWhiteSpace(valeur))
            throw new DomainException("L'e-mail est obligatoire.");

        var normalise = valeur.Trim();
        if (!EmailRegex().IsMatch(normalise))
            throw new DomainException($"L'e-mail '{valeur}' est invalide.");

        return new Email(normalise);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Valeur.ToLowerInvariant();
    }

    public override string ToString() => Valeur;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
}
