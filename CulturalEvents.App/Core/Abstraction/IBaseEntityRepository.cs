using System.Linq.Expressions;
using CulturalEvents.App.Core.Entity;

namespace CulturalEvents.App.Core.Abstraction;

public interface IBaseEntityRepository<T> where T : BaseEntity
{
    T[] Get(Expression<Func<T, bool>> predicate);
    Task<T[]> GetAsync(Expression<Func<T, bool>> predicate);
    T[] Get();
    Task<T[]> GetAsync();
    T? Get(int id);
    ValueTask<T?> GetAsync(int id);
    bool Add(T value);
    ValueTask<bool> AddAsync(T value);
    bool Update(T value);
    bool Delete(int id);
    ValueTask<bool> DeleteAsync(int id);
}