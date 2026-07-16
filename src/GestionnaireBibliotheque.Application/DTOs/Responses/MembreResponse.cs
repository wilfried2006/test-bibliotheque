namespace GestionnaireBibliotheque.Application.DTOs.Responses;

public record MembreResponse(
    int Id,
    string Nom,
    string Prenom,
    string? Email,
    DateTime DateInscription,
    byte TypeAdherentId);
