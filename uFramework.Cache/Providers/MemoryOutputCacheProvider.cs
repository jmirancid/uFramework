using System;
using System.IO;
using System.Web.Caching;
using uFramework.Cache.Business;
using uFramework.Cache.Entities;

namespace uFramework.Cache.Providers
{
    public class MemoryOutputCacheProvider : OutputCacheProvider
    {
        private MemoryCacheBusiness _biz = new MemoryCacheBusiness();

        public override object Add(string key, object entry, DateTime utcExpiry)
        {
            var e =
                new CacheEntry()
                {
                    Key = Path.GetFileName(key),
                    Value = entry,
                    Expiry = utcExpiry,
                    Sliding = true
                };

            _biz.Create(e);

            return entry;
        }

        public override object Get(string key)
        {
            var e =
                _biz.Get(key);

            if (e == null || e.Expiry <= DateTime.Now)
            {
                Remove(key);
                return null;
            }

            return e.Value;
        }

        public override void Remove(string key)
        {
            var e =
                new CacheEntry() { Key = key };

            _biz.Delete(e);
        }

        public override void Set(string key, object entry, DateTime utcExpiry)
        {
            var e =
                new CacheEntry()
                {
                    Key = Path.GetFileName(key),
                    Value = entry,
                    Expiry = utcExpiry,
                    Sliding = true
                };

            _biz.Update(e);
        }
    }
}
