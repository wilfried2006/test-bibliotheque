using GestionnaireBibliotheque.BlazorWasm.Domain.Emprunts;
using GestionnaireBibliotheque.BlazorWasm.Domain.Membres;
using GestionnaireBibliotheque.BlazorWasm.Domain.Penalites;

namespace GestionnaireBibliotheque.BlazorWasm.Domain.Services;

/// <summary>Service de domaine pour calculer les pénalités liées aux emprunts en retard.</summary>
public class CalculPenaliteService
{
    private const decimal MONTANT_PAR_JOUR = 1.50m;
    private const decimal MONTANT_MAX_PENALITE = 50.00m;

    public Montant Calculer(Emprunt emprunt, TypeAdherent typeAdherent)
    {
        if (!emprunt.EstEnRetard)
            return Montant.Create(0);

        var joursRetard = emprunt.JoursRetard;
        var tauxMembre = ObtenirTaux(typeAdherent);
        var montantBrut = joursRetard * MONTANT_PAR_JOUR * tauxMembre;

        return Montant.Create(Math.Min(montantBrut, MONTANT_MAX_PENALITE));
    }

    private decimal ObtenirTaux(TypeAdherent type)
        => type.Libelle switch
        {
            "Premium" => 0.5m,      // 50% de réduction pour les premium
            "Étudiant" => 1.5m,     // 150% pour les étudiants
            _ => 1.0m               // 100% par défaut
        };
}
