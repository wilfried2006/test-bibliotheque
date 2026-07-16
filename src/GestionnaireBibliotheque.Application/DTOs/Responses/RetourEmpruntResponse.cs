using GestionnaireBibliotheque.Domain.Enums;

namespace GestionnaireBibliotheque.Application.DTOs.Responses;

/// <summary>Résultat d'un retour : état final, retard éventuel et pénalité appliquée.</summary>
public record RetourEmpruntResponse(
    int EmpruntId,
    int ExemplaireId,
    int MembreId,
    DateTime DateRetourReel,
    DateTime DateRetourPrevue,
    bool EnRetard,
    int JoursRetard,
    decimal MontantPenalite,
    EtatEmprunt EtatEmprunt);
