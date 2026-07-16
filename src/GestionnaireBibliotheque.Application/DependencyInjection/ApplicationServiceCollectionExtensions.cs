using Microsoft.Extensions.DependencyInjection;

namespace GestionnaireBibliotheque.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Squelette : aucun service applicatif enregistré pour l'instant.
        return services;
    }
}
