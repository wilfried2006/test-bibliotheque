using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionnaireBibliotheque.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMembres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 20 membres, chacun rattaché à un type d'adhérent (1 = standard, 2 = étudiant).
            migrationBuilder.Sql(@"
INSERT INTO Membres (Nom, Prenom, Email, DateInscription, TypeAdherentId) VALUES
(N'Dupont',   N'Marie',   N'marie.dupont@biblio.fr',      '2025-01-12', 1),
(N'Martin',   N'Pierre',  N'pierre.martin@biblio.fr',     '2025-01-28', 2),
(N'Bernard',  N'Sophie',  N'sophie.bernard@biblio.fr',    '2025-02-09', 1),
(N'Petit',    N'Luc',     N'luc.petit@biblio.fr',         '2025-02-21', 2),
(N'Durand',   N'Emma',    N'emma.durand@biblio.fr',       '2025-03-05', 1),
(N'Leroy',    N'Thomas',  N'thomas.leroy@biblio.fr',      '2025-03-18', 2),
(N'Moreau',   N'Julie',   N'julie.moreau@biblio.fr',      '2025-04-02', 1),
(N'Simon',    N'Nicolas', N'nicolas.simon@biblio.fr',     '2025-04-15', 2),
(N'Laurent',  N'Camille', N'camille.laurent@biblio.fr',   '2025-05-01', 1),
(N'Lefebvre', N'Antoine', N'antoine.lefebvre@biblio.fr',  '2025-05-20', 2),
(N'Michel',   N'Léa',     N'lea.michel@biblio.fr',        '2025-06-07', 1),
(N'Garcia',   N'Hugo',    N'hugo.garcia@biblio.fr',       '2025-06-23', 2),
(N'David',    N'Chloé',   N'chloe.david@biblio.fr',       '2025-07-11', 1),
(N'Bertrand', N'Maxime',  N'maxime.bertrand@biblio.fr',   '2025-08-02', 2),
(N'Roux',     N'Manon',   N'manon.roux@biblio.fr',        '2025-09-14', 1),
(N'Vincent',  N'Louis',   N'louis.vincent@biblio.fr',     '2025-10-06', 2),
(N'Fournier', N'Sarah',   N'sarah.fournier@biblio.fr',    '2025-11-19', 1),
(N'Morel',    N'Julien',  N'julien.morel@biblio.fr',      '2025-12-08', 2),
(N'Girard',   N'Inès',    N'ines.girard@biblio.fr',       '2026-01-22', 1),
(N'André',    N'Paul',    N'paul.andre@biblio.fr',        '2026-02-16', 2);
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert : retire les membres ajoutés par ce seed.
            migrationBuilder.Sql("DELETE FROM Membres WHERE Email LIKE '%@biblio.fr';");
        }
    }
}
