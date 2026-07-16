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
