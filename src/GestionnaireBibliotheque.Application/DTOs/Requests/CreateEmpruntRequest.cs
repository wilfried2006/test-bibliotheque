namespace GestionnaireBibliotheque.Application.DTOs.Requests;

/// <summary>Demande d'emprunt : un membre emprunte un ouvrage (un exemplaire dispo lui est assigné).</summary>
public record CreateEmpruntRequest(int OuvrageId, int MembreId);
