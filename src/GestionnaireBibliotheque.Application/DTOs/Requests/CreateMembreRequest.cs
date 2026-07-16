namespace GestionnaireBibliotheque.Application.DTOs.Requests;

public record CreateMembreRequest(string Nom, string Prenom, string? Email, byte TypeAdherentId);
