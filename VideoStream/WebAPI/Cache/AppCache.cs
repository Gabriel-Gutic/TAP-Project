using BusinessLayer.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Cache
{
    public class AppCache : IAppCache
    {
        IMemoryCache _memoryCache;

        public AppCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Store(string key, object value, int seconds)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds)
            };

            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        public bool TryGet(string key, out object? value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }
    }
}
