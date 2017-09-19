using System.Runtime.Caching;

namespace uFramework.Cache.Repositories
{
    public class MemoryCacheRepository : CacheRepository<System.Runtime.Caching.MemoryCache, uFramework.Cache.Entities.CacheEntry>, uFramework.Cache.Interfaces.IMemoryCacheRepository
    {
        protected override System.Runtime.Caching.MemoryCache Context
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public void Trim(int percent)
        {
            Context.Trim(percent);
        }
    }
}
