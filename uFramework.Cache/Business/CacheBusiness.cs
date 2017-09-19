using System;
using System.Collections.Generic;
using System.Linq;
using uFramework.Cache.Interfaces;
using uFramework.Interfaces.Business;

namespace uFramework.Cache.Business
{
    public abstract class CacheBusiness<TEntry, TRepository> : IBusiness<TEntry>
        where TEntry : class, new()
        where TRepository : ICacheRepository<TEntry>
    {
        protected Lazy<TRepository> _repository;

        public abstract Lazy<TRepository> Repository { get; set; }

        public virtual void Create(TEntry entity)
        {
            this.Repository.Value.Create(entity);
        }

        public virtual void Update(TEntry entity)
        {
            this.Repository.Value.Update(entity);
        }

        public virtual void Delete(TEntry entity)
        {
            this.Repository.Value.Delete(entity);
        }

        public virtual TEntry Get(int id)
        {
            return this.Repository.Value.Get(id);
        }

        public virtual TEntry Get(string id)
        {
            return this.Repository.Value.Get(id);
        }

        public virtual TEntry GetBy(System.Linq.Expressions.Expression<Func<TEntry, bool>> predicate)
        {
            return this.Repository.Value.GetBy(predicate);
        }

        public virtual IEnumerable<TEntry> All()
        {
            return this.Repository.Value.All().ToList();
        }

        public virtual IEnumerable<TEntry> AllBy(System.Linq.Expressions.Expression<Func<TEntry, bool>> predicate)
        {
            return this.Repository.Value.AllBy(predicate).ToList();
        }

        public virtual IEnumerable<TEntry> Filter(int skip, int take)
        {
            return this.Repository.Value.Filter(skip, take).ToList();
        }

        public virtual IEnumerable<TEntry> FilterBy(int skip, int take, System.Linq.Expressions.Expression<Func<TEntry, bool>> predicate)
        {
            return this.Repository.Value.FilterBy(skip, take, predicate).ToList();
        }

        public virtual int Count()
        {
            return this.Repository.Value.Count();
        }

        public virtual int CountBy(System.Linq.Expressions.Expression<Func<TEntry, bool>> predicate)
        {
            return this.Repository.Value.CountBy(predicate);
        }
    }
}
