namespace GestionnaireBibliotheque.BlazorWasm.Domain.Specifications;

/// <summary>Interface de base pour les Specifications (critères de requête réutilisables).</summary>
public interface ISpecification<T>
{
    Func<T, bool> Criteria { get; }
    string Description { get; }
}
