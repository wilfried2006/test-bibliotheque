namespace GestionnaireBibliotheque.Application.DTOs.Responses;

/// <summary>Total des pénalités en cours d'un membre, plafonné.</summary>
public record TotalPenalitesResponse(int MembreId, decimal Total, decimal Plafond, bool Plafonne);
