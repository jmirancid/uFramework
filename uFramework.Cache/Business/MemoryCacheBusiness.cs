using System;
using uFramework.Cache.Interfaces;

namespace uFramework.Cache.Business
{
    public class MemoryCacheBusiness : CacheBusiness<uFramework.Cache.Entities.CacheEntry, IMemoryCacheRepository>
    {
        public override Lazy<IMemoryCacheRepository> Repository
        {
            get
            {
                if (base._repository == null)
                    base._repository = new Lazy<IMemoryCacheRepository>(() => new uFramework.Cache.Repositories.MemoryCacheRepository());

                return base._repository;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Trim(int percent)
        {
            Repository.Value.Trim(percent);
        }
    }
}
