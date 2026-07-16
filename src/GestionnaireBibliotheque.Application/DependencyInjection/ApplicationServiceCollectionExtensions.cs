using System.Reflection;
using FluentValidation;
using GestionnaireBibliotheque.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GestionnaireBibliotheque.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));
        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<IAuteurService, AuteurService>();
        services.AddScoped<IOuvrageService, OuvrageService>();
        services.AddScoped<IExemplaireService, ExemplaireService>();
        services.AddScoped<IEmpruntService, EmpruntService>();
        services.AddScoped<IMembreService, MembreService>();
        services.AddScoped<ITypeAdherentService, TypeAdherentService>();
        services.AddScoped<IPenaliteService, PenaliteService>();

        return services;
    }
}
