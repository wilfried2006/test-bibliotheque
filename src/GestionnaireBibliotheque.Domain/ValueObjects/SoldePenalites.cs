namespace GestionnaireBibliotheque.Domain.ValueObjects;

/// <summary>
/// Solde des pénalités d'un membre : total brut, total plafonné et plafond appliqué.
/// Value Object calculé par la politique de pénalité du domaine.
/// </summary>
public sealed record SoldePenalites(Montant Brut, Montant Total, Montant Plafond)
{
    public bool EstPlafonne => Brut.Valeur > Plafond.Valeur;
}
