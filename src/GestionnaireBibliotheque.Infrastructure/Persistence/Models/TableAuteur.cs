namespace GestionnaireBibliotheque.Infrastructure.Persistence.Models;


public class TableAuteur
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;
}
