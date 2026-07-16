namespace GestionnaireBibliotheque.Infrastructure.Persistence.Models;


public class TableTypeAdherent
{
    public byte Id { get; set; }
    public string Libelle { get; set; } = null!;
    public byte NombreOuvragesMax { get; set; }
    public byte DureeEmpruntJours { get; set; }
}
