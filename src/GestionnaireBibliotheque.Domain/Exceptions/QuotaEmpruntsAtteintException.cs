namespace GestionnaireBibliotheque.Domain.Exceptions;

public sealed class QuotaEmpruntsAtteintException(int membreId, int quota)
    : ConflitMetierException($"Le membre {membreId} a atteint son quota d'emprunts simultanés ({quota}).");
