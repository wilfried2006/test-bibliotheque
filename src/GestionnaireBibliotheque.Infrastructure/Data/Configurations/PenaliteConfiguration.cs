using GestionnaireBibliotheque.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionnaireBibliotheque.Infrastructure.Data.Configurations;

public class PenaliteConfiguration : IEntityTypeConfiguration<TablePenalite>
{
    public void Configure(EntityTypeBuilder<TablePenalite> builder)
    {
        builder.ToTable("Penalites");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Montant).HasPrecision(10, 2);
        builder.Property(p => p.DatePenalite).HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(p => p.MembreId);

        builder.HasOne<TableMembre>()
            .WithMany()
            .HasForeignKey(p => p.MembreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<TableExemplaire>()
            .WithMany()
            .HasForeignKey(p => p.ExemplaireId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
