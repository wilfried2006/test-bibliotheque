using System.Reflection;
using GestionnaireBibliotheque.Domain.Interfaces;
using GestionnaireBibliotheque.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionnaireBibliotheque.Infrastructure.Persistence.Repositories;

/// <summary>
/// Base des repositories : traduit entre entités du Domain et entités EF (Data).
/// Les lectures renvoient des entités détachées ; les écritures ré-attachent via ToData.
/// </summary>
public abstract class RepositoryBase<TDomain, TData, TKey>(BibliothequeContext context) : IRepository<TDomain, TKey>
    where TDomain : class
    where TData : class
{
    protected readonly BibliothequeContext Context = context;
    protected DbSet<TData> Set => Context.Set<TData>();

    protected abstract TData ToData(TDomain domain);
    protected abstract TDomain ToDomain(TData data);

    private static readonly PropertyInfo DomainId = typeof(TDomain).GetProperty("Id")!;
    private static readonly PropertyInfo DataId = typeof(TData).GetProperty("Id")!;

    public virtual async Task<TDomain?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var data = await Set.FindAsync([id], cancellationToken);
        if (data is null)
            return null;

        var domain = ToDomain(data);
        Context.Entry(data).State = EntityState.Detached; // les écritures ré-attacheront un Data neuf
        return domain;
    }

    public virtual async Task<IReadOnlyList<TDomain>> ListAsync(CancellationToken cancellationToken = default)
    {
        var data = await Set.AsNoTracking().ToListAsync(cancellationToken);
        return data.Select(ToDomain).ToList();
    }

    public virtual async Task AddAsync(TDomain entity, CancellationToken cancellationToken = default)
    {
        var data = ToData(entity);
        await Set.AddAsync(data, cancellationToken);

        // Après SaveChanges, recopie la clé générée (identity) vers l'entité du domaine.
        void OnSaved(object? sender, SavedChangesEventArgs e)
        {
            Context.SavedChanges -= OnSaved;
            DomainId.SetValue(entity, DataId.GetValue(data));
        }
        Context.SavedChanges += OnSaved;
    }

    public virtual void Update(TDomain entity) => Set.Update(ToData(entity));

    public virtual void Remove(TDomain entity) => Set.Remove(ToData(entity));
}
