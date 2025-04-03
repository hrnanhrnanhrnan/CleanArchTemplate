using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CleanArchTemplate.Application.Common.Interfaces;
using CleanArchTemplate.Domain.Common;

namespace CleanArchTemplate.Infrastructure.Repositories;

internal abstract class BaseRepository<T>(DbContext context) : IBaseRepository<T>
    where T : EntityBase
{
    protected virtual IQueryable<T> ApplyIncludes(IQueryable<T> dbSet) => dbSet; 

    protected virtual async Task<IEnumerable<T>> Query(Expression<Func<T, bool>>? predicate = null, bool track = false)
    {
        var dbSet = context.Set<T>()
            .AsQueryable();

        if (!track)
        {
           dbSet = dbSet.AsNoTracking();
        }

        dbSet = ApplyIncludes(dbSet);

        return predicate is not null
            ? await dbSet.Where(predicate).ToListAsync()
            : await dbSet.ToListAsync();
    }

    public virtual Task<IEnumerable<T>> GetAllAsync()
        => Query();

    public virtual async Task<T> AddAsync(T entity)
    {
        var addedEntity = await context.AddAsync(entity);
        await SaveChangesAsync();
        return addedEntity.Entity;
    }

    public virtual async Task RemoveAsync(T entity)
    {
        context.Remove(entity);
        await SaveChangesAsync();
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        var updatedEntity = context.Update(entity);
        await SaveChangesAsync();
        return updatedEntity.Entity;
    }

    protected async Task SaveChangesAsync()
        => await context.SaveChangesAsync();

    public async Task<T?> GetById(int id, bool track = false)
    {
        var entities = await Query(x => x.Id == id, track);
        return entities
                .ToList()
                .FirstOrDefault();
    }
}