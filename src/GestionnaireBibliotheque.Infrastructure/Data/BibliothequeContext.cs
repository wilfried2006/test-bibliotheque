using GestionnaireBibliotheque.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionnaireBibliotheque.Infrastructure.Data;

public class BibliothequeContext(DbContextOptions<BibliothequeContext> options) : DbContext(options)
{
    public DbSet<TableAuteur> Auteurs => Set<TableAuteur>();
    public DbSet<TableMembre> Membres => Set<TableMembre>();
    public DbSet<TableTypeAdherent> TypesAdherent => Set<TableTypeAdherent>();
    public DbSet<TableOuvrage> Ouvrages => Set<TableOuvrage>();
    public DbSet<TableExemplaire> Exemplaires => Set<TableExemplaire>();
    public DbSet<TableEmprunt> Emprunts => Set<TableEmprunt>();
    public DbSet<TablePenalite> Penalites => Set<TablePenalite>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BibliothequeContext).Assembly);
    }
}
