using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace LeetU.Data.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task InsertAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity> GetAsync(long id);
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params string[] includes);
    Task<int> SaveChanges();
    public DbContext Context { get; }
}