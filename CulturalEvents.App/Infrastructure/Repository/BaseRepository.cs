using System.Linq.Expressions;
using CulturalEvents.App.Core.Abstraction;
using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CulturalEvents.App.Infrastructure.Repository;

public class BaseRepository<T>(DbSet<T> set) : IBaseEntityRepository<T> where T : BaseEntityAuditableEntity
{
    public T[] Get(Expression<Func<T, bool>> predicate)
    {
        return set.Where(predicate).ToArray();
    }

    public Task<T[]> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return set.Where(predicate).ToArrayAsync();
    }

    public T[] Get()
    {
        return set.ToArray();
    }

    public Task<T[]> GetAsync()
    {
        return set.ToArrayAsync();
    }

    public T? Get(int id)
    {
        return set.Find(id);
    }

    public ValueTask<T?> GetAsync(int id)
    {
        return set.FindAsync(id);
    }

    public bool Add(T value)
    {
        return set.Add(value).State == EntityState.Added;
    }

    public async ValueTask<bool> AddAsync(T value)
    {
        return (await set.AddAsync(value)).State == EntityState.Added;
    }

    public bool Update(T value)
    {
        return set.Update(value).State == EntityState.Modified;
    }

    public bool Delete(int id)
    {
        return set.Remove(Get(id) ?? throw new InvalidOperationException()).State == EntityState.Deleted;
    }

    public async ValueTask<bool> DeleteAsync(int id)
    {
        return set.Remove(await GetAsync(id) ?? throw new InvalidOperationException()).State == EntityState.Deleted;
    }
}