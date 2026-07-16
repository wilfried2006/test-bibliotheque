namespace GestionnaireBibliotheque.Domain.Exceptions;

/// <summary>Exception de base signalant la violation d'une règle métier du domaine.</summary>
public class DomainException(string message) : Exception(message);
