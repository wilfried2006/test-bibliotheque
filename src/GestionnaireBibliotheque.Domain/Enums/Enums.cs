namespace GestionnaireBibliotheque.Domain.Enums;

/// <summary>État de disponibilité d'un exemplaire physique.</summary>
public enum EtatDisponibilite
{
    Disponible,
    Emprunte
}

/// <summary>Cycle de vie d'un emprunt.</summary>
public enum EtatEmprunt
{
    Actif,
    Cloture,
    Retard
}

/// <summary>Statut d'une pénalité.</summary>
public enum StatutPenalite
{
    /// <summary>Exemplaire pas encore rendu, retard en cours (montant calculé à la volée).</summary>
    EnCours,

    /// <summary>Exemplaire rendu en retard : pénalité due.</summary>
    APayer,

    /// <summary>Pénalité réglée.</summary>
    Paye
}
