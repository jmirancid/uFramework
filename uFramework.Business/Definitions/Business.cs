using System;
using System.Collections.Generic;
using System.Linq;
using uFramework.Interfaces.Business;
using uFramework.Interfaces.Repositories;

namespace uFramework.Business.Definitions
{
    public abstract class Business<TEntity, TRepository> : IBusiness<TEntity>
        where TEntity : class, new()
        where TRepository : IRepository<TEntity>
    {
        public Lazy<TRepository> Repository { get; set; }

        public Business()
        {
            this.Repository = new Lazy<TRepository>(() => { return uFramework.Repositories.Factory.Resolve<TRepository>(); });
        }

        public virtual void Create(TEntity entity)
        {
            this.Repository.Value.Create(entity);
        }

        public virtual void Update(TEntity entity)
        {
            this.Repository.Value.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            this.Repository.Value.Delete(entity);
        }

        public virtual TEntity Get(object id)
        {
            return this.Repository.Value.Get(id);
        }

        public virtual TEntity GetBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.Value.GetBy(predicate);
        }

        public virtual IEnumerable<TEntity> All()
        {
            return this.Repository.Value.All().ToList();
        }

        public virtual IEnumerable<TEntity> AllBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.Value.AllBy(predicate).ToList();
        }

        public virtual IEnumerable<TEntity> Filter(int skip, int take)
        {
            return this.Repository.Value.Filter(skip, take).ToList();
        }

        public virtual IEnumerable<TEntity> FilterBy(int skip, int take, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.Value.FilterBy(skip, take, predicate).ToList();
        }

        public virtual int Count()
        {
            return this.Repository.Value.Count();
        }

        public virtual int CountBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return this.Repository.Value.CountBy(predicate);
        }
    }

}
