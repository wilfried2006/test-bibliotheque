namespace GestionnaireBibliotheque.BlazorWasm.Models;

public record EmpruntDto(
    int Id,
    int ExemplaireId,
    int MembreId,
    DateTime DateEmprunt,
    DateTime DateRetourPrevue,
    DateTime? DateRetourReel,
    string EtatEmprunt);

public record RetourResultDto(
    int EmpruntId,
    int ExemplaireId,
    int MembreId,
    DateTime DateRetourReel,
    DateTime DateRetourPrevue,
    bool EnRetard,
    int JoursRetard,
    decimal MontantPenalite,
    string EtatEmprunt);
