using System.Linq.Expressions;
using CulturalEvents.App.Core.Entity;

namespace CulturalEvents.App.Core.Abstraction;

public interface IBaseEntityRepository<T> where T : BaseEntity
{
    T[] Get(Expression<Func<T, bool>> predicate);
    Task<T[]> GetAsync(Expression<Func<T, bool>> predicate);
    T[] Get();
    Task<T[]> GetAsync();
    Option<T> Get(int id);
    ValueTask<Option<T>> GetAsync(int id);
    Result<T> Add(T value);
    ValueTask<Result<T>> AddAsync(T value);
    Result<T> Update(T value);
    Result<T> Delete(int id);
    ValueTask<Result<T>> DeleteAsync(int id);
}