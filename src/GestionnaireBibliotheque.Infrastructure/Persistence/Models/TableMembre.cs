namespace GestionnaireBibliotheque.Infrastructure.Persistence.Models;


public class TableMembre
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;
    public string? Email { get; set; }
    public DateTime DateInscription { get; set; }
    public byte TypeAdherentId { get; set; }
}
