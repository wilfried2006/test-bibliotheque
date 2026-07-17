using GestionnaireBibliotheque.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionnaireBibliotheque.Infrastructure.Data.Configurations;

public class TypeAdherentConfiguration : IEntityTypeConfiguration<TableTypeAdherent>
{
    public void Configure(EntityTypeBuilder<TableTypeAdherent> builder)
    {
        builder.ToTable("TypesAdherent");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.Libelle).IsRequired().HasMaxLength(100);
        builder.Property(t => t.NombreOuvragesMax).IsRequired();
        builder.Property(t => t.DureeEmpruntJours).IsRequired();

        builder.HasIndex(t => t.Libelle).IsUnique();

        // Types d'adhérent de référence.
        builder.HasData(
            new TableTypeAdherent { Id = 1, Libelle = "Adhérent standard", NombreOuvragesMax = 3, DureeEmpruntJours = 21 },
            new TableTypeAdherent { Id = 2, Libelle = "Adhérent étudiant", NombreOuvragesMax = 5, DureeEmpruntJours = 28 });
    }
}
