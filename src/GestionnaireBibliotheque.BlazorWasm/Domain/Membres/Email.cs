namespace GestionnaireBibliotheque.BlazorWasm.Domain.Membres;

public record Email(string Valeur)
{
    public static Email? TryCreate(string? valeur)
    {
        if (string.IsNullOrWhiteSpace(valeur))
            return null;

        if (!valeur.Contains('@'))
            throw new ArgumentException("Email invalide", nameof(valeur));

        return new Email(valeur);
    }
}
