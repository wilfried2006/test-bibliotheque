using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionnaireBibliotheque.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmpruntsPenalites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 15 cas d'emprunt STRICTEMENT cohérents, ancrés sur la date d'application.
            //
            // Colonnes du jeu de cas c(ExemplaireId, MembreId, DE, DRP, RR, Etat) :
            //   - DE / DRP / RR = décalages en jours par rapport à aujourd'hui (RR = NULL si non rendu).
            //   - DRP = DE + durée du type (membre impair = standard 21 j, pair = étudiant 28 j).
            //   - Etat (EtatEmprunt en byte) : 0 = Actif, 1 = Clôturé, 2 = En retard, déduit des dates :
            //       Actif   ⟺ RR NULL ; Clôturé ⟺ RR ≤ DRP ; Retard ⟺ RR > DRP.
            //
            // Couverture : Clôturé (1,2,3,14) ; Retard (4,5,6,7,8,15) ; Actif non échu (9,10) ;
            //              Actif hors délai → pénalité « En cours » calculée (11,12,13).
            migrationBuilder.Sql(@"
DECLARE @today date = CAST(GETUTCDATE() AS date);

INSERT INTO Emprunts (ExemplaireId, MembreId, DateEmprunt, DateRetourPrevue, DateRetourReel, EtatEmprunt)
SELECT c.ExemplaireId, c.MembreId,
       DATEADD(day, c.DE,  @today),
       DATEADD(day, c.DRP, @today),
       DATEADD(day, c.RR,  @today),
       c.Etat
FROM (VALUES
    -- Clôturés (rendus dans les délais) — pas de pénalité
    (1,  1,  -40, -19, -25, 1),
    (2,  2,  -50, -22, -30, 1),
    (3,  3,  -30,  -9,  -9, 1),   -- rendu le jour même de l'échéance
    (14, 14, -60, -32, -35, 1),
    -- Retard (rendus après l'échéance) — pénalité persistée
    (4,  4,  -40, -12,  -7, 2),   -- 5 j de retard
    (5,  5,  -45, -24, -14, 2),   -- 10 j
    (6,  6, -100, -72, -12, 2),   -- 60 j (montant > plafond)
    (7,  7,  -35, -14, -11, 2),   -- 3 j (payé)
    (8,  8,  -70, -42, -22, 2),   -- 20 j (payé)
    (15, 15, -25,  -4,  -3, 2),   -- 1 j
    -- Actifs non échus (échéance future) — exemplaire emprunté
    (9,  9,   -5,  16, NULL, 0),
    (10, 10,  -2,  26, NULL, 0),
    -- Actifs hors délai (non rendus, échéance dépassée) — pénalité « En cours » calculée
    (11, 11, -29,  -8, NULL, 0),   -- 8 j de retard en cours
    (12, 12, -58, -30, NULL, 0),   -- 30 j
    (13, 13, -81, -60, NULL, 0)    -- 60 j
) AS c(ExemplaireId, MembreId, DE, DRP, RR, Etat);

-- Cohérence de disponibilité : tout exemplaire d'un emprunt actif passe à « Emprunté » (1).
UPDATE e SET e.EtatDisponibilite = 1
FROM Exemplaires e
JOIN Emprunts emp ON emp.ExemplaireId = e.Id
WHERE emp.DateRetourReel IS NULL;

-- Pénalités persistées pour les emprunts en Retard :
-- jours = rendu − échéance, montant = 0,20 € × jours, lien EmpruntId/Membre/Exemplaire.
INSERT INTO Penalites (MembreId, ExemplaireId, EmpruntId, JoursRetard, Montant, DatePenalite, Statut)
SELECT emp.MembreId, emp.ExemplaireId, emp.Id,
       DATEDIFF(day, emp.DateRetourPrevue, emp.DateRetourReel),
       CAST(0.20 AS decimal(10,2)) * DATEDIFF(day, emp.DateRetourPrevue, emp.DateRetourReel),
       emp.DateRetourReel,
       p.Statut
FROM Emprunts emp
JOIN (VALUES
    (4,  1),   -- À payer
    (5,  1),
    (6,  1),
    (15, 1),
    (7,  2),   -- Payé
    (8,  2)
) AS p(ExemplaireId, Statut) ON p.ExemplaireId = emp.ExemplaireId;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM Penalites;
DELETE FROM Emprunts;
UPDATE Exemplaires SET EtatDisponibilite = 0;
");
        }
    }
}
