namespace GestionnaireBibliotheque.BlazorWasm.Models;

public record ExemplaireDto(int Id, int OuvrageId, Guid CodeBarre, string EtatDisponibilite);
