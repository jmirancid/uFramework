using System;
using System.Linq;
using System.Runtime.Caching;
using uFramework.Cache.Interfaces;

namespace uFramework.Cache.Repositories
{
    public abstract class CacheRepository<TCache, TEntry> : ICacheRepository<TEntry>, IDisposable
        where TEntry : uFramework.Cache.Entities.CacheEntry, new()
        where TCache : ObjectCache
    {
        protected abstract TCache Context { get; }

        public virtual void Create(TEntry entity)
        {
            var policy =
                new CacheItemPolicy();

            if (entity.Sliding)
                policy.SlidingExpiration = entity.Expiry.TimeOfDay;
            else
                policy.AbsoluteExpiration = new DateTimeOffset(entity.Expiry);

            Context.Add(entity.Key, entity.Value, policy);
        }

        public virtual void Update(TEntry entity)
        {
            var policy =
                new CacheItemPolicy();

            if (entity.Sliding)
                policy.SlidingExpiration = entity.Expiry.TimeOfDay;
            else
                policy.AbsoluteExpiration = new DateTimeOffset(entity.Expiry);

            Context.Set(entity.Key, entity.Value, policy);
        }

        public virtual void Delete(TEntry entity)
        {
            Context.Remove(entity.Key);
        }
    
        public virtual TEntry Get(int id)
        {
            throw new NotImplementedException();
        }

        public virtual TEntry Get(string id)
        {
            var c =
                Context.GetCacheItem(id);

            if (c == null)
                return null;

            return new TEntry() { Key = c.Key, Value = c.Value };
        }

        public virtual TEntry GetBy(System.Linq.Expressions.Expression<Func<TEntry, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntry> All()
        {
            return Context.Select(kvp => new TEntry() { Key = kvp.Key, Value = kvp.Value }).AsQueryable();
        }

        public virtual IQueryable<TEntry> AllBy(System.Linq.Expressions.Expression<Func<TEntry, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntry> Filter(int skip, int take)
        {
            return All().Skip(skip).Take(take);
        }

        public virtual IQueryable<TEntry> FilterBy(int skip, int take, System.Linq.Expressions.Expression<Func<TEntry, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual int Count()
        {
            return Context.Count();
        }

        public virtual int CountBy(System.Linq.Expressions.Expression<Func<TEntry, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
