using GestionnaireBibliotheque.Domain.Enums;

namespace GestionnaireBibliotheque.Infrastructure.Persistence.Models;

public class TableExemplaire
{
    public int Id { get; set; }
    public int OuvrageId { get; set; }
    public Guid CodeBarre { get; set; }
    public EtatDisponibilite EtatDisponibilite { get; set; }
}
