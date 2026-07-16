using GestionnaireBibliotheque.Domain.Enums;

namespace GestionnaireBibliotheque.Application.DTOs.Responses;

public record ExemplaireResponse(int Id, int OuvrageId, Guid CodeBarre, EtatDisponibilite EtatDisponibilite);
