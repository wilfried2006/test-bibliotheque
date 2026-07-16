namespace GestionnaireBibliotheque.Application.DTOs.Responses;

public record PenaliteResponse(
    int Id,
    int MembreId,
    int ExemplaireId,
    int JoursRetard,
    decimal Montant,
    DateTime DatePenalite);
