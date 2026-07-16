using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Exceptions;

namespace GestionnaireBibliotheque.Domain.Services;

/// <summary>
/// Service de domaine portant la politique d'emprunt : contrôle du quota du type
/// d'adhérent, marquage de l'exemplaire et calcul de l'échéance.
/// </summary>
public static class ServiceEmprunt
{
    public static Emprunt Emprunter(
        Membre membre,
        TypeAdherent type,
        int nombreEmpruntsActifs,
        Exemplaire exemplaire,
        DateTime aujourdhui)
    {
        if (nombreEmpruntsActifs >= type.NombreOuvragesMax)
            throw new QuotaEmpruntsAtteintException(membre.Id, type.NombreOuvragesMax);

        exemplaire.Emprunter();

        return Emprunt.Creer(
            exemplaire.Id,
            membre.Id,
            aujourdhui,
            aujourdhui.AddDays(type.DureeEmpruntJours));
    }
}
