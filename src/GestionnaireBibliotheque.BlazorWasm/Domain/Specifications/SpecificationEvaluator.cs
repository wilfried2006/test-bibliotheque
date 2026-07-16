namespace GestionnaireBibliotheque.BlazorWasm.Domain.Specifications;

/// <summary>Utilitaire pour évaluer les Specifications sur des collections.</summary>
public static class SpecificationEvaluator
{
    public static IEnumerable<T> GetQuery<T>(IEnumerable<T> items, ISpecification<T> spec)
        => items.Where(spec.Criteria);

    public static List<T> GetQueryList<T>(IEnumerable<T> items, ISpecification<T> spec)
        => items.Where(spec.Criteria).ToList();
}
