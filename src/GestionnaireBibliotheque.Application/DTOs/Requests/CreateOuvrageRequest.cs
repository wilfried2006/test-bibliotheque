namespace GestionnaireBibliotheque.Application.DTOs.Requests;

public record CreateOuvrageRequest(string Titre, int AuteurId, int NombreExemplaires);
