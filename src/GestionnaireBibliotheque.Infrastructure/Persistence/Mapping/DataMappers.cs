using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Enums;
using GestionnaireBibliotheque.Infrastructure.Persistence.Models;

namespace GestionnaireBibliotheque.Infrastructure.Persistence.Mapping;

/// <summary>Conversions entre entités du Domain et entités EF (Data).</summary>
internal static class DataMappers
{
    // --- Auteur ---
    public static Auteur ToDomain(this TableAuteur d) => Auteur.Reconstituer(d.Id, d.Nom, d.Prenom);
    public static TableAuteur ToData(this Auteur a) => new() { Id = a.Id, Nom = a.Nom, Prenom = a.Prenom };

    // --- TypeAdherent ---
    public static TypeAdherent ToDomain(this TableTypeAdherent d)
        => TypeAdherent.Reconstituer(d.Id, d.Libelle, d.NombreOuvragesMax, d.DureeEmpruntJours);
    public static TableTypeAdherent ToData(this TypeAdherent t)
        => new() { Id = t.Id, Libelle = t.Libelle, NombreOuvragesMax = t.NombreOuvragesMax, DureeEmpruntJours = t.DureeEmpruntJours };

    // --- Ouvrage ---
    public static Ouvrage ToDomain(this TableOuvrage d) => Ouvrage.Reconstituer(d.Id, d.Titre, d.AuteurId);
    public static TableOuvrage ToData(this Ouvrage o) => new() { Id = o.Id, Titre = o.Titre, AuteurId = o.AuteurId };

    // --- Exemplaire ---
    public static Exemplaire ToDomain(this TableExemplaire d)
        => Exemplaire.Reconstituer(d.Id, d.OuvrageId, d.CodeBarre, d.EtatDisponibilite);
    public static TableExemplaire ToData(this Exemplaire e)
        => new() { Id = e.Id, OuvrageId = e.OuvrageId, CodeBarre = e.CodeBarre, EtatDisponibilite = e.EtatDisponibilite };

    // --- Membre ---
    public static Membre ToDomain(this TableMembre d)
        => Membre.Reconstituer(d.Id, d.Nom, d.Prenom, d.Email, d.DateInscription, d.TypeAdherentId);
    public static TableMembre ToData(this Membre m)
        => new() { Id = m.Id, Nom = m.Nom, Prenom = m.Prenom, Email = m.Email?.Valeur, DateInscription = m.DateInscription, TypeAdherentId = m.TypeAdherentId };

    // --- Emprunt ---
    public static Emprunt ToDomain(this TableEmprunt d)
        => Emprunt.Reconstituer(d.Id, d.ExemplaireId, d.MembreId, d.DateEmprunt, d.DateRetourPrevue, d.DateRetourReel, d.EtatEmprunt, d.RowVersion);
    public static TableEmprunt ToData(this Emprunt e)
        => new()
        {
            Id = e.Id, ExemplaireId = e.ExemplaireId, MembreId = e.MembreId,
            DateEmprunt = e.DateEmprunt, DateRetourPrevue = e.DateRetourPrevue, DateRetourReel = e.DateRetourReel,
            EtatEmprunt = e.EtatEmprunt, RowVersion = e.RowVersion
        };

    // --- Penalite ---
    public static Penalite ToDomain(this TablePenalite d)
        => Penalite.Reconstituer(d.Id, d.MembreId, d.ExemplaireId, d.EmpruntId, d.JoursRetard, d.Montant, d.DatePenalite, d.Statut);
    public static TablePenalite ToData(this Penalite p)
        => new() { Id = p.Id, MembreId = p.MembreId, ExemplaireId = p.ExemplaireId, EmpruntId = p.EmpruntId, JoursRetard = p.JoursRetard, Montant = p.Montant.Valeur, DatePenalite = p.DatePenalite, Statut = p.Statut };
}
