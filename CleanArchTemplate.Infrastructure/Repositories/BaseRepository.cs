using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CleanArchTemplate.Application.Common.Interfaces;
using CleanArchTemplate.Domain.Common;

namespace CleanArchTemplate.Infrastructure.Repositories;

internal abstract class BaseRepository<T>(DbContext context) : IBaseRepository<T>
    where T : EntityBase
{
    protected virtual IQueryable<T> ApplyIncludes(IQueryable<T> dbSet) => dbSet; 

    protected virtual async Task<IEnumerable<T>> Query(Expression<Func<T, bool>>? predicate = null, bool track = false, CancellationToken token = default)
    {
        var dbSet = context.Set<T>()
            .AsQueryable();

        if (!track)
        {
           dbSet = dbSet.AsNoTracking();
        }

        dbSet = ApplyIncludes(dbSet);

        return predicate is not null
            ? await dbSet.Where(predicate).ToListAsync(token)
            : await dbSet.ToListAsync(token);
    }

    public virtual Task<IEnumerable<T>> GetAllAsync(CancellationToken token = default)
        => Query(token: token);

    public virtual async Task<T> AddAsync(T entity, CancellationToken token = default)
    {
        var addedEntity = await context.AddAsync(entity, token);
        await SaveChangesAsync(token);
        return addedEntity.Entity;
    }

    public virtual async Task RemoveAsync(T entity, CancellationToken token = default)
    {
        context.Remove(entity);
        await SaveChangesAsync(token);
    }

    public virtual async Task<T> UpdateAsync(T entity, CancellationToken token = default)
    {
        var updatedEntity = context.Update(entity);
        await SaveChangesAsync(token);
        return updatedEntity.Entity;
    }

    protected async Task SaveChangesAsync(CancellationToken token = default)
        => await context.SaveChangesAsync(token);

    public async Task<T?> GetByIdAsync(int id, bool track = false, CancellationToken token = default)
    {
        var entities = await Query(x => x.Id == id, track);
        return entities
                .ToList()
                .FirstOrDefault();
    }
}