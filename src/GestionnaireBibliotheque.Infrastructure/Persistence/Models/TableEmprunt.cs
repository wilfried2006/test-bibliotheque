using GestionnaireBibliotheque.Domain.Enums;

namespace GestionnaireBibliotheque.Infrastructure.Persistence.Models;


public class TableEmprunt
{
    public int Id { get; set; }
    public int ExemplaireId { get; set; }
    public int MembreId { get; set; }
    public DateTime DateEmprunt { get; set; }
    public DateTime DateRetourPrevue { get; set; }
    public DateTime? DateRetourReel { get; set; }
    public EtatEmprunt EtatEmprunt { get; set; }
    public byte[] RowVersion { get; set; } = [];
}
