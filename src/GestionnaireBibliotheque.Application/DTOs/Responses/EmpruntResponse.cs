using GestionnaireBibliotheque.Domain.Enums;

namespace GestionnaireBibliotheque.Application.DTOs.Responses;

public record EmpruntResponse(
    int Id,
    int ExemplaireId,
    int MembreId,
    DateTime DateEmprunt,
    DateTime DateRetourPrevue,
    DateTime? DateRetourReel,
    EtatEmprunt EtatEmprunt);
