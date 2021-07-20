using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

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

        public void Update(string key, string newKey)
        {
            var value = _cache.GetString(key);

            if(value is not null)
            {
                _cache.SetString(newKey, value);
            }
            else
            {
                throw new Exception("Key does not exist in the cache.");
            }

        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _cache.GetStringAsync(key);

            if (value is not null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public async Task<T> SetAsync<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(15),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
            };

            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value), options);

            return value;
        }
    }
}
