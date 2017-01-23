using Amazon.ElastiCacheCluster;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;
using System.Runtime.Caching;
using Business;

namespace Api.Utils
{
    public interface ICache {
        string SetItem(User user);
        User GetItem(string token);
    }

    public class Cache : ICache
    {
        private readonly MemcachedClient _MemClient;
        public Cache()
        {
            try
            {
                // instantiate a new client.
                var config = new ElastiCacheClusterConfig();
                _MemClient = new MemcachedClient(config);
            }
            catch (Exception)
            {
                //falling back to IIS Cache
            }
        }

        public string SetItem(User user)
        {
            var token = Guid.NewGuid();
            var freshness = new TimeSpan(0, 0, 20, 0);

            // Store the data for 20 min. in the cluster. 
            // The client will decide which cache host will store this item.
            if (_MemClient != null)
                _MemClient.Store(StoreMode.Set, token.ToString(), user, freshness);
            else
                MemoryCache.Default.Add(token.ToString(), user, new CacheItemPolicy() { SlidingExpiration = freshness });

            return token.ToString();
        }

        public User GetItem(string token)
        {
            return _MemClient.Get<User>(token);
        }
    }
}
