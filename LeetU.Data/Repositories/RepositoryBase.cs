using System.Linq.Expressions;
using LeetU.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeetU.Data.Repositories;

/// <summary>
/// Contains all the basic CRUD operations for all Entities. All repositories inherit from this class.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    public DbContext Context { get; }

    protected RepositoryBase(DbContext context)
    {
        Context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task InsertAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task<TEntity> GetAsync(long id)
    {
        return (await _dbSet.FindAsync(id))!;
    }

    /// <summary>
    /// Provides a way of searching a repository by providing a search expression
    /// </summary>
    /// <param name="filter">The search expression (ie, x => x.Id == entityId)</param>
    /// <param name="orderBy">The orderby expression</param>
    /// <param name="includes">Specify what entity relationships to include as defined by the key constraints in the database</param>
    /// <returns></returns>
    public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params string[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (filter != null)
            query = query.Where(filter);

        return orderBy != null ? orderBy(query).ToList() : query.ToList();
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        var rowsAffected = await Context.SaveChangesAsync();
        return rowsAffected;
    }
}