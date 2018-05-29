using System;
using System.Linq;

namespace uFramework.Interfaces.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        TEntity Get(object id);
        TEntity GetBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> All();
        IQueryable<TEntity> AllBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Filter(int skip, int take);
        IQueryable<TEntity> FilterBy(int skip, int take, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        int Count();
        int CountBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
    }
}
