using GestionnaireBibliotheque.Domain.Entities;

namespace GestionnaireBibliotheque.Domain.Interfaces;

public interface IAuteurRepository : IRepository<Auteur, int>;

public interface ITypeAdherentRepository : IRepository<TypeAdherent, byte>;

public interface IOuvrageRepository : IRepository<Ouvrage, int>;

public interface IExemplaireRepository : IRepository<Exemplaire, int>
{
    Task<IReadOnlyList<Exemplaire>> ListerParOuvrageAsync(int ouvrageId, CancellationToken cancellationToken = default);
    Task<Exemplaire?> PremierDisponibleAsync(int ouvrageId, CancellationToken cancellationToken = default);
}

public interface IMembreRepository : IRepository<Membre, int>;

public interface IEmpruntRepository : IRepository<Emprunt, int>
{
    Task<int> CompterEmpruntsActifsAsync(int membreId, CancellationToken cancellationToken = default);
}

public interface IPenaliteRepository : IRepository<Penalite, int>
{
    Task<IReadOnlyList<Penalite>> ListerParMembreAsync(int membreId, CancellationToken cancellationToken = default);
}
