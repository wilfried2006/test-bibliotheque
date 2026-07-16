using GestionnaireBibliotheque.Domain.Interfaces;
using GestionnaireBibliotheque.Infrastructure.Data;

namespace GestionnaireBibliotheque.Infrastructure.Persistence.Repositories;

public class UnitOfWork(BibliothequeContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
