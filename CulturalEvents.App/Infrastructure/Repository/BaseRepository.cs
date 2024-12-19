using System.Linq.Expressions;
using CulturalEvents.App.Core;
using CulturalEvents.App.Core.Abstraction;
using CulturalEvents.App.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace CulturalEvents.App.Infrastructure.Repository;

public class BaseRepository<T> : IBaseEntityRepository<T> where T : BaseAuditableEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _set;
    public BaseRepository(DbContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }
    public T[] Get(Expression<Func<T, bool>> predicate)
    {
        return _set.AsNoTracking().Where(predicate).ToArray();
    }

    public Task<T[]> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return _set.AsNoTracking().Where(predicate).ToArrayAsync();
    }

    public T[] Get()
    {
        return _set.AsNoTracking().ToArray();
    }

    public Task<T[]> GetAsync()
    {
        return _set.AsNoTracking().ToArrayAsync();
    }

    public Option<T> Get(int id)
    {
        return _set.Find(id);
    }

    public async ValueTask<Option<T>> GetAsync(int id)
    {
        return await _set.FindAsync(id);
    }

    public Result<T> Add(T value)
    {
        var tmp= _set.Add(value);
        try
        {
            _context.SaveChanges();
            return tmp.Entity;
        }
        catch (Exception ex)
        {
            tmp.State = EntityState.Detached;
            return ex;
        }
    }

    public async ValueTask<Result<T>> AddAsync(T value)
    {
        var tmp= await _set.AddAsync(value);
        try
        {
            await _context.SaveChangesAsync();
            return tmp.Entity;
        }
        catch (Exception ex)
        {
            tmp.State = EntityState.Detached;
            return ex;
        }
    }

    public Result<T> Update(T value)
    {
        var tmp=_set.Update(value);
        try
        {
            _context.SaveChanges();
            return tmp.Entity;
        }
        catch (Exception ex)
        {
            tmp.State = EntityState.Detached;
            return ex;
        }
    }

    public Result<T> Delete(int id)
    {
        var entity = Get(id);
        if (!entity.IsSome) return new InvalidOperationException();
        var tmp=_set.Remove(entity);
        try
        {
            _context.SaveChanges();
            return tmp.Entity;
        }
        catch (Exception ex)
        {
            tmp.State = EntityState.Detached;
            return ex;
        }
    }

    public async ValueTask<Result<T>> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (!entity.IsSome) return new InvalidOperationException();
        var tmp=_set.Remove(entity);
        try
        {
            await _context.SaveChangesAsync();
            return tmp.Entity;
        }
        catch (Exception ex)
        {
            tmp.State = EntityState.Detached;
            return ex;
        }
    }
}