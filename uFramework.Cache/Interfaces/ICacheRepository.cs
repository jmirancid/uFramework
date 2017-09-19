using uFramework.Interfaces.Repositories;

namespace uFramework.Cache.Interfaces
{
    public interface ICacheRepository<TEntry> : IRepository<TEntry>
        where TEntry : class, new()
    {
        TEntry Get(string id);
    }
}
