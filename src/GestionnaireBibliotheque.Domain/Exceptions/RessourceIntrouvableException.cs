namespace GestionnaireBibliotheque.Domain.Exceptions;

public sealed class RessourceIntrouvableException(string message) : DomainException(message);
