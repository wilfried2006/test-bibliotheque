using GestionnaireBibliotheque.Domain.Entities;
using GestionnaireBibliotheque.Domain.Enums;
using GestionnaireBibliotheque.Domain.Interfaces;
using GestionnaireBibliotheque.Infrastructure.Data;
using GestionnaireBibliotheque.Infrastructure.Persistence.Mapping;
using GestionnaireBibliotheque.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionnaireBibliotheque.Infrastructure.Persistence.Repositories;

public class AuteurRepository(BibliothequeContext context)
    : RepositoryBase<Auteur, TableAuteur, int>(context), IAuteurRepository
{
    protected override TableAuteur ToData(Auteur d) => d.ToData();
    protected override Auteur ToDomain(TableAuteur d) => d.ToDomain();
}

public class TypeAdherentRepository(BibliothequeContext context)
    : RepositoryBase<TypeAdherent, TableTypeAdherent, byte>(context), ITypeAdherentRepository
{
    protected override TableTypeAdherent ToData(TypeAdherent d) => d.ToData();
    protected override TypeAdherent ToDomain(TableTypeAdherent d) => d.ToDomain();
}

public class OuvrageRepository(BibliothequeContext context)
    : RepositoryBase<Ouvrage, TableOuvrage, int>(context), IOuvrageRepository
{
    protected override TableOuvrage ToData(Ouvrage d) => d.ToData();
    protected override Ouvrage ToDomain(TableOuvrage d) => d.ToDomain();
}

public class MembreRepository(BibliothequeContext context)
    : RepositoryBase<Membre, TableMembre, int>(context), IMembreRepository
{
    protected override TableMembre ToData(Membre d) => d.ToData();
    protected override Membre ToDomain(TableMembre d) => d.ToDomain();
}

public class ExemplaireRepository(BibliothequeContext context)
    : RepositoryBase<Exemplaire, TableExemplaire, int>(context), IExemplaireRepository
{
    protected override TableExemplaire ToData(Exemplaire d) => d.ToData();
    protected override Exemplaire ToDomain(TableExemplaire d) => d.ToDomain();

    public async Task<IReadOnlyList<Exemplaire>> ListerParOuvrageAsync(int ouvrageId, CancellationToken cancellationToken = default)
        => (await Set.AsNoTracking().Where(e => e.OuvrageId == ouvrageId).ToListAsync(cancellationToken))
            .Select(ToDomain).ToList();

    public async Task<Exemplaire?> PremierDisponibleAsync(int ouvrageId, CancellationToken cancellationToken = default)
    {
        var data = await Set.AsNoTracking()
            .Where(e => e.OuvrageId == ouvrageId && e.EtatDisponibilite == EtatDisponibilite.Disponible)
            .OrderBy(e => e.Id)
            .FirstOrDefaultAsync(cancellationToken);
        return data is null ? null : ToDomain(data);
    }
}

public class EmpruntRepository(BibliothequeContext context)
    : RepositoryBase<Emprunt, TableEmprunt, int>(context), IEmpruntRepository
{
    protected override TableEmprunt ToData(Emprunt d) => d.ToData();
    protected override Emprunt ToDomain(TableEmprunt d) => d.ToDomain();

    public async Task<int> CompterEmpruntsActifsAsync(int membreId, CancellationToken cancellationToken = default)
        => await Set.CountAsync(e => e.MembreId == membreId && e.EtatEmprunt == EtatEmprunt.Actif, cancellationToken);
}

public class PenaliteRepository(BibliothequeContext context)
    : RepositoryBase<Penalite, TablePenalite, int>(context), IPenaliteRepository
{
    protected override TablePenalite ToData(Penalite d) => d.ToData();
    protected override Penalite ToDomain(TablePenalite d) => d.ToDomain();

    public async Task<IReadOnlyList<Penalite>> ListerParMembreAsync(int membreId, CancellationToken cancellationToken = default)
        => (await Set.AsNoTracking().Where(p => p.MembreId == membreId).ToListAsync(cancellationToken))
            .Select(ToDomain).ToList();
}
