using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GestionnaireBibliotheque.Infrastructure.Data;

/// <summary>
/// Factory utilisée uniquement au design-time par les outils EF Core
/// (dotnet ef migrations / update), afin de ne pas dépendre du démarrage de l'API.
/// </summary>
public class BibliothequeContextFactory : IDesignTimeDbContextFactory<BibliothequeContext>
{
    public BibliothequeContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("ConnectionStrings__SqlServer")
            ?? "Server=localhost,1433;Database=GestionnaireBibliotheque;User Id=sa;Password=Your_password123;TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<BibliothequeContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new BibliothequeContext(options);
    }
}
