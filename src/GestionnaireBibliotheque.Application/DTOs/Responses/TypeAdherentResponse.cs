namespace GestionnaireBibliotheque.Application.DTOs.Responses;

public record TypeAdherentResponse(byte Id, string Libelle, byte NombreOuvragesMax, byte DureeEmpruntJours);
