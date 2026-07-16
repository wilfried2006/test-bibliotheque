using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Auteurs;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Exemplaires;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Emprunts;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Membres;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Ouvrages;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Penalites;
using GestionnaireBibliotheque.BlazorWasm.Domain.Services;
using GestionnaireBibliotheque.BlazorWasm.Services;

namespace GestionnaireBibliotheque.BlazorWasm.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<CalculPenaliteService>();
        services.AddScoped<VerificationLimitesEmpruntService>();
        return services;
    }

    public static IServiceCollection AddApplicationUseCases(this IServiceCollection services)
    {
        // Auteurs
        services.AddScoped<RecupereLesAuteursUseCase>();
        services.AddScoped<CreerAuteurUseCase>();

        // Ouvrages
        services.AddScoped<RecupereLesOuvragesUseCase>();
        services.AddScoped<CreerOuvrageUseCase>();

        // Exemplaires
        services.AddScoped<RecupereLesExemplairesUseCase>();
        services.AddScoped<CreerExemplaireUseCase>();

        // Membres
        services.AddScoped<RecupereLesMembreUseCase>();
        services.AddScoped<RecupereLesTypesAdherentUseCase>();
        services.AddScoped<CreerMembreUseCase>();

        // Emprunts
        services.AddScoped<RecupereLesEmpruntUseCase>();
        services.AddScoped<CreerEmpruntUseCase>();
        services.AddScoped<RetournerEmpruntUseCase>();

        // Pénalités
        services.AddScoped<RecupereLespenalitesUseCase>();
        services.AddScoped<RecupereLePenalitesMembreUseCase>();
        services.AddScoped<RecupereLeTotalPenalitesMembreUseCase>();
        services.AddScoped<MarquerPenalitePayeUseCase>();

        return services;
    }

    public static IServiceCollection AddDomainSpecifications(this IServiceCollection services)
    {
        // Les Specifications sont des classes de critères, pas des services injectables
        // Elles sont utilisées directement dans les Use Cases ou Services
        return services;
    }
}
