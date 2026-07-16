namespace GestionnaireBibliotheque.Domain.Exceptions;

public sealed class EmpruntDejaRetourneException(int empruntId)
    : ConflitMetierException($"L'emprunt {empruntId} a déjà été retourné.");
