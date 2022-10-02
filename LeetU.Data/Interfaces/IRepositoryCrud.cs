using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace LeetU.Data.Interfaces;

public interface IRepositoryCrud<TEntity> where TEntity : class
{
    Task InsertAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity> GetAsync(long id);
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params string[] includes);
    Task<int> SaveChangesAsync();
    public DbContext Context { get; }
}