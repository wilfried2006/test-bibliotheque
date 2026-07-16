namespace GestionnaireBibliotheque.BlazorWasm.Models;

public record PenaliteDto(
    int Id,
    int MembreId,
    int ExemplaireId,
    int JoursRetard,
    decimal Montant,
    DateTime DatePenalite,
    int? EmpruntId,
    string Statut);

public record TotalPenalitesDto(int MembreId, decimal Total, decimal Plafond, bool Plafonne);
