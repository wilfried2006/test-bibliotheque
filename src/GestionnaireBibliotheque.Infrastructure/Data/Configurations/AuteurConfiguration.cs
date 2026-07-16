using GestionnaireBibliotheque.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionnaireBibliotheque.Infrastructure.Data.Configurations;

public class AuteurConfiguration : IEntityTypeConfiguration<TableAuteur>
{
    public void Configure(EntityTypeBuilder<TableAuteur> builder)
    {
        builder.ToTable("Auteurs");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Nom).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Prenom).IsRequired().HasMaxLength(200);

        builder.HasIndex(a => a.Nom);
    }
}
