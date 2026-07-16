using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionnaireBibliotheque.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypesAdherent",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Libelle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreOuvragesMax = table.Column<byte>(type: "tinyint", nullable: false),
                    DureeEmpruntJours = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesAdherent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ouvrages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AuteurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ouvrages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ouvrages_Auteurs_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Membres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    TypeAdherentId = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membres_TypesAdherent_TypeAdherentId",
                        column: x => x.TypeAdherentId,
                        principalTable: "TypesAdherent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exemplaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OuvrageId = table.Column<int>(type: "int", nullable: false),
                    CodeBarre = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EtatDisponibilite = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exemplaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exemplaires_Ouvrages_OuvrageId",
                        column: x => x.OuvrageId,
                        principalTable: "Ouvrages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emprunts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExemplaireId = table.Column<int>(type: "int", nullable: false),
                    MembreId = table.Column<int>(type: "int", nullable: false),
                    DateEmprunt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DateRetourPrevue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRetourReel = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EtatEmprunt = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprunts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprunts_Exemplaires_ExemplaireId",
                        column: x => x.ExemplaireId,
                        principalTable: "Exemplaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emprunts_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Penalites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembreId = table.Column<int>(type: "int", nullable: false),
                    ExemplaireId = table.Column<int>(type: "int", nullable: false),
                    EmpruntId = table.Column<int>(type: "int", nullable: false),
                    JoursRetard = table.Column<int>(type: "int", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    DatePenalite = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Statut = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalites_Exemplaires_ExemplaireId",
                        column: x => x.ExemplaireId,
                        principalTable: "Exemplaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Penalites_Membres_MembreId",
                        column: x => x.MembreId,
                        principalTable: "Membres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auteurs_Nom",
                table: "Auteurs",
                column: "Nom");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_DateRetourPrevue",
                table: "Emprunts",
                column: "DateRetourPrevue");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_ExemplaireId",
                table: "Emprunts",
                column: "ExemplaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_MembreId_EtatEmprunt",
                table: "Emprunts",
                columns: new[] { "MembreId", "EtatEmprunt" });

            migrationBuilder.CreateIndex(
                name: "IX_Exemplaires_CodeBarre",
                table: "Exemplaires",
                column: "CodeBarre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exemplaires_EtatDisponibilite",
                table: "Exemplaires",
                column: "EtatDisponibilite");

            migrationBuilder.CreateIndex(
                name: "IX_Exemplaires_OuvrageId",
                table: "Exemplaires",
                column: "OuvrageId");

            migrationBuilder.CreateIndex(
                name: "IX_Membres_Nom",
                table: "Membres",
                column: "Nom");

            migrationBuilder.CreateIndex(
                name: "IX_Membres_TypeAdherentId",
                table: "Membres",
                column: "TypeAdherentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ouvrages_AuteurId",
                table: "Ouvrages",
                column: "AuteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Ouvrages_Titre",
                table: "Ouvrages",
                column: "Titre");

            migrationBuilder.CreateIndex(
                name: "IX_Penalites_EmpruntId",
                table: "Penalites",
                column: "EmpruntId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalites_ExemplaireId",
                table: "Penalites",
                column: "ExemplaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalites_MembreId",
                table: "Penalites",
                column: "MembreId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesAdherent_Libelle",
                table: "TypesAdherent",
                column: "Libelle",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprunts");

            migrationBuilder.DropTable(
                name: "Penalites");

            migrationBuilder.DropTable(
                name: "Exemplaires");

            migrationBuilder.DropTable(
                name: "Membres");

            migrationBuilder.DropTable(
                name: "Ouvrages");

            migrationBuilder.DropTable(
                name: "TypesAdherent");

            migrationBuilder.DropTable(
                name: "Auteurs");
        }
    }
}
