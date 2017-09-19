using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uFramework.Cache.Interfaces
{
    public interface IMemoryCacheRepository : ICacheRepository<uFramework.Cache.Entities.CacheEntry>
    {
        void Trim(int percentage);
    }
}
