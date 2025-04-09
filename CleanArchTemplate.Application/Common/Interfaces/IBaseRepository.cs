using CleanArchTemplate.Domain.Common;

namespace CleanArchTemplate.Application.Common.Interfaces;

public interface IBaseRepository<T> 
    where T : EntityBase
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default);
    Task<T?> GetByIdAsync(int id, bool track = false, CancellationToken token = default);
    Task<T> AddAsync(T entity, CancellationToken token = default);
    Task<T> UpdateAsync(T entity, CancellationToken token = default);
    Task RemoveAsync(T entity, CancellationToken token = default);
}