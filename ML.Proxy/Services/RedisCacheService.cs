using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace ML.Proxy.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value is not null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public T Set<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(15),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
            };

            _cache.SetString(key, JsonConvert.SerializeObject(value), options);

            return value;
        }
    }
}
