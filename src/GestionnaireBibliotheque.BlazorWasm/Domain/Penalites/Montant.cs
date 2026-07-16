namespace GestionnaireBibliotheque.BlazorWasm.Domain.Penalites;

public record Montant(decimal Valeur)
{
    public static Montant Create(decimal valeur)
    {
        if (valeur < 0)
            throw new ArgumentException("Le montant doit être >= 0", nameof(valeur));

        return new Montant(valeur);
    }
}
