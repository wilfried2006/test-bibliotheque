namespace GestionnaireBibliotheque.Domain.Exceptions;

public sealed class AucunExemplaireDisponibleException(int ouvrageId)
    : ConflitMetierException($"Aucun exemplaire disponible pour l'ouvrage {ouvrageId}.");
