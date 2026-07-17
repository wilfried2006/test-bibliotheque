using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Auteurs;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Exemplaires;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Emprunts;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Membres;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Ouvrages;
using GestionnaireBibliotheque.BlazorWasm.Application.UseCases.Penalites;

namespace GestionnaireBibliotheque.BlazorWasm.Infrastructure;

public static class DependencyInjection
{
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
        services.AddScoped<MarquerPenalitePayeUseCase>();

        return services;
    }
}
