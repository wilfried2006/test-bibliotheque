namespace GestionnaireBibliotheque.BlazorWasm.Models;

public record MembreDto(
    int Id,
    string Nom,
    string Prenom,
    string? Email,
    DateTime DateInscription,
    byte TypeAdherentId);
