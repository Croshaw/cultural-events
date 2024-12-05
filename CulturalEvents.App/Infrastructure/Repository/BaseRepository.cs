using System.Linq.Expressions;
using CulturalEvents.App.Core;
using CulturalEvents.App.Core.Abstraction;
using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CulturalEvents.App.Infrastructure.Repository;

public class BaseRepository<T>(DbSet<T> set) : IBaseEntityRepository<T> where T : BaseAuditableEntity
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

    public Option<T> Get(int id)
    {
        return set.Find(id);
    }

    public async ValueTask<Option<T>> GetAsync(int id)
    {
        return await set.FindAsync(id);
    }

    public Result<T> Add(T value)
    {
        return set.Add(value).Entity;
    }

    public async ValueTask<Result<T>> AddAsync(T value)
    {
        return (await set.AddAsync(value)).Entity;
    }

    public Result<T> Update(T value)
    {
        return set.Update(value).Entity;
    }

    public Result<T> Delete(int id)
    {
        var entity = Get(id);
        return entity.IsSome ? set.Remove(entity).Entity : new InvalidOperationException();
    }

    public async ValueTask<Result<T>> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        return entity.IsSome ? set.Remove(entity).Entity : new InvalidOperationException();
    }
}