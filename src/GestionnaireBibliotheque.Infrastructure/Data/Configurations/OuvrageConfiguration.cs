using GestionnaireBibliotheque.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionnaireBibliotheque.Infrastructure.Data.Configurations;

public class OuvrageConfiguration : IEntityTypeConfiguration<TableOuvrage>
{
    public void Configure(EntityTypeBuilder<TableOuvrage> builder)
    {
        builder.ToTable("Ouvrages");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();

        builder.Property(o => o.Titre).IsRequired().HasMaxLength(300);
        builder.HasIndex(o => o.Titre);

        builder.HasOne<TableAuteur>()
            .WithMany()
            .HasForeignKey(o => o.AuteurId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
