using GestionnaireBibliotheque.Domain.Enums;
using GestionnaireBibliotheque.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionnaireBibliotheque.Infrastructure.Data.Configurations;

public class EmpruntConfiguration : IEntityTypeConfiguration<TableEmprunt>
{
    public void Configure(EntityTypeBuilder<TableEmprunt> builder)
    {
        builder.ToTable("Emprunts");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.MembreId).IsRequired();
        builder.Property(e => e.DateEmprunt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.DateRetourPrevue).IsRequired();

        builder.Property(e => e.EtatEmprunt)
            .IsRequired()
            .HasConversion<byte>()
            .HasDefaultValue(EtatEmprunt.Actif);

        builder.Property(e => e.RowVersion).IsRowVersion();

        builder.HasIndex(e => new { e.MembreId, e.EtatEmprunt });
        builder.HasIndex(e => e.DateRetourPrevue);

        builder.HasOne<TableExemplaire>()
            .WithMany()
            .HasForeignKey(e => e.ExemplaireId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<TableMembre>()
            .WithMany()
            .HasForeignKey(e => e.MembreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
