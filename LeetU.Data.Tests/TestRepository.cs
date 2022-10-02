using LeetU.Data.Context;
using LeetU.Data.Repositories;

namespace LeetU.Data.Tests;

public class TestRepository<TEntity> : RepositoryBase<TEntity>
    where TEntity : class
{
    public TestRepository(StudentContext context) : base(context)
    {
    }
}