using GestionnaireBibliotheque.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionnaireBibliotheque.Infrastructure.Data.Configurations;

public class MembreConfiguration : IEntityTypeConfiguration<TableMembre>
{
    public void Configure(EntityTypeBuilder<TableMembre> builder)
    {
        builder.ToTable("Membres");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedOnAdd();

        builder.Property(m => m.Nom).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Prenom).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Email).HasMaxLength(256);
        builder.Property(m => m.DateInscription).HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(m => m.Nom);

        builder.HasOne<TableTypeAdherent>()
            .WithMany()
            .HasForeignKey(m => m.TypeAdherentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
