namespace GestionnaireBibliotheque.BlazorWasm.Models;

// Contrats d'écriture (POST) vers l'API — typés (plus de `dynamic`).
// Le serveur est la source de vérité : ces records reflètent simplement le corps attendu par l'API.

public record CreateAuteurRequest(string Nom, string Prenom);

public record CreateOuvrageRequest(string Titre, int AuteurId, int NombreExemplaires);

public record CreateExemplaireRequest(int OuvrageId, string EtatDisponibilite);

public record CreateMembreRequest(string Nom, string Prenom, string? Email, byte TypeAdherentId);

public record CreateEmpruntRequest(int OuvrageId, int MembreId);
