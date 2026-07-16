using GestionnaireBibliotheque.BlazorWasm.Domain.Emprunts;
using GestionnaireBibliotheque.BlazorWasm.Domain.Membres;
using GestionnaireBibliotheque.BlazorWasm.Models;

namespace GestionnaireBibliotheque.BlazorWasm.Domain.Services;

/// <summary>Service de domaine pour vérifier les limites et contraintes d'emprunt.</summary>
public class VerificationLimitesEmpruntService
{
    public EmpruntPermission VerifierPermissionEmprunt(
        Membre membre,
        TypeAdherent typeAdherent,
        List<EmpruntDto> empruntsEnCours,
        List<PenaliteDto> penalites)
    {
        var validation = new EmpruntPermission();

        // Vérification 1: Limites d'emprunts simultanés
        var nbEmpruntsCours = empruntsEnCours.Count(e => e.DateRetourReel == null && e.MembreId == membre.Id);
        if (nbEmpruntsCours >= typeAdherent.NombreOuvragesMax)
        {
            validation.Erreurs.Add(
                $"Limite d'emprunts atteinte : {nbEmpruntsCours}/{typeAdherent.NombreOuvragesMax}");
        }

        // Vérification 2: Pénalités non payées
        var totalPenalites = penalites
            .Where(p => p.MembreId == membre.Id)
            .Sum(p => p.Montant);

        if (totalPenalites > 100)
        {
            validation.Erreurs.Add(
                $"Pénalités en cours : {totalPenalites:C}. Règlez vos dettes avant de nouveaux emprunts.");
        }

        // Vérification 3: Retards antérieurs
        var empruntsEnRetard = empruntsEnCours.Count(e =>
            e.MembreId == membre.Id &&
            e.DateRetourReel.HasValue &&
            e.DateRetourReel > e.DateRetourPrevue);

        if (empruntsEnRetard >= 3)
        {
            validation.Erreurs.Add(
                $"Trop de retards détectés ({empruntsEnRetard}). Accès restreint.");
        }

        return validation;
    }
}

public class EmpruntPermission
{
    public List<string> Erreurs { get; set; } = new();
    public bool EstAutorise => Erreurs.Count == 0;
}
