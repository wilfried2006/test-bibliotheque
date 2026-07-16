namespace GestionnaireBibliotheque.Infrastructure.Persistence.Models;

public class TableOuvrage
{
    public int Id { get; set; }
    public string Titre { get; set; } = null!;
    public int AuteurId { get; set; }
}
