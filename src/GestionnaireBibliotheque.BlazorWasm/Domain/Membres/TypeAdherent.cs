namespace GestionnaireBibliotheque.BlazorWasm.Domain.Membres;

public class TypeAdherent
{
    public byte Id { get; set; }
    public required string Libelle { get; set; }
    public int NombreOuvragesMax { get; set; }
    public int DureeEmpruntJours { get; set; }
}
