using CleanArchTemplate.Domain.Common;

namespace CleanArchTemplate.Application.Common.Interfaces;

public interface IBaseRepository<T> 
    where T : EntityBase
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetById(int id, bool track = false);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}