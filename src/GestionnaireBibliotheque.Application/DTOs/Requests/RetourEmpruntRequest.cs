namespace GestionnaireBibliotheque.Application.DTOs.Requests;

/// <summary>Requête de retour d'un emprunt. Date optionnelle (par défaut : maintenant, UTC).</summary>
public record RetourEmpruntRequest(DateTime? DateRetour);
