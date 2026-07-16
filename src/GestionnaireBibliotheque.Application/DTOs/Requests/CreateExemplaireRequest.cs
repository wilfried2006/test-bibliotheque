using GestionnaireBibliotheque.Domain.Enums;

namespace GestionnaireBibliotheque.Application.DTOs.Requests;

public record CreateExemplaireRequest(int OuvrageId, EtatDisponibilite EtatDisponibilite);
