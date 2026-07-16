using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GestionnaireBibliotheque.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTypesAdherent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TypesAdherent",
                columns: new[] { "Id", "DureeEmpruntJours", "Libelle", "NombreOuvragesMax" },
                values: new object[,]
                {
                    { (byte)1, (byte)21, "Adhérent standard", (byte)3 },
                    { (byte)2, (byte)28, "Adhérent étudiant", (byte)5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypesAdherent",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "TypesAdherent",
                keyColumn: "Id",
                keyValue: (byte)2);
        }
    }
}
