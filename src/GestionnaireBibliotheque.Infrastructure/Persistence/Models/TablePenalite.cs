namespace GestionnaireBibliotheque.Infrastructure.Persistence.Models;

public class TablePenalite
{
    public int Id { get; set; }
    public int MembreId { get; set; }
    public int ExemplaireId { get; set; }
    public int JoursRetard { get; set; }
    public decimal Montant { get; set; }
    public DateTime DatePenalite { get; set; }
}
