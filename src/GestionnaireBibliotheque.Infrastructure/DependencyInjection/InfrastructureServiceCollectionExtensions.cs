using GestionnaireBibliotheque.Domain.Interfaces;
using GestionnaireBibliotheque.Infrastructure.Data;
using GestionnaireBibliotheque.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestionnaireBibliotheque.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BibliothequeContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAuteurRepository, AuteurRepository>();
        services.AddScoped<ITypeAdherentRepository, TypeAdherentRepository>();
        services.AddScoped<IOuvrageRepository, OuvrageRepository>();
        services.AddScoped<IExemplaireRepository, ExemplaireRepository>();
        services.AddScoped<IMembreRepository, MembreRepository>();
        services.AddScoped<IEmpruntRepository, EmpruntRepository>();
        services.AddScoped<IPenaliteRepository, PenaliteRepository>();

        return services;
    }
}
