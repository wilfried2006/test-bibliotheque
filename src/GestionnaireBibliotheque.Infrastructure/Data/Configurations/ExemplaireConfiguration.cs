using GestionnaireBibliotheque.Domain.Enums;
using GestionnaireBibliotheque.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionnaireBibliotheque.Infrastructure.Data.Configurations;

public class ExemplaireConfiguration : IEntityTypeConfiguration<TableExemplaire>
{
    public void Configure(EntityTypeBuilder<TableExemplaire> builder)
    {
        builder.ToTable("Exemplaires");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.CodeBarre)
            .HasDefaultValueSql("NEWID()")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.EtatDisponibilite)
            .IsRequired()
            .HasConversion<byte>()
            .HasDefaultValue(EtatDisponibilite.Disponible);

        builder.HasIndex(e => e.CodeBarre).IsUnique();
        builder.HasIndex(e => e.EtatDisponibilite);

        builder.HasOne<TableOuvrage>()
            .WithMany()
            .HasForeignKey(e => e.OuvrageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
