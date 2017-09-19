using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uFramework.Interfaces.Business
{
    public interface IBusiness<TEntity>
            where TEntity : class, new()
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        TEntity Get(int id);
        TEntity GetBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> All();
        IEnumerable<TEntity> AllBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Filter(int skip, int take);
        IEnumerable<TEntity> FilterBy(int skip, int take, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        int Count();
        int CountBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
    }
}
