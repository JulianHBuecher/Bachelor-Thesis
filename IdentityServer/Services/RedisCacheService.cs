using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        private static DistributedCacheEntryOptions _options = new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(3),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value is not null)
            {
                return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }

            return default;
        }

        public void Set<T>(string key, T value)
        {
            _cache.SetString(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }),
            _options);
        }

        public void UpdateLabel(string key, string newKey)
        {
            var value = _cache.GetString(key);

            if (value is not null)
            {
                _cache.SetString(newKey, value, _options);
            }
            else
            {
                throw new Exception("Key does not exist in the cache.");
            }
        }

        public void Remove(string key)
        {
            var value = _cache.GetString(key);

            if (value is not null)
            {
                _cache.Remove(key);
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
                return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }

            return default;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }), _options);
        }

        public async Task UpdateLabelAsync(string key, string newKey)
        {
            var value = await _cache.GetStringAsync(key);

            if (value is not null)
            {
                await _cache.SetStringAsync(newKey, value, _options);
            }
            else
            {
                throw new Exception("Key does not exist in the cache.");
            }
        }

        public async Task RemoveAsync(string key)
        {
            var value = await _cache.GetStringAsync(key);

            if (value is not null)
            {
                await _cache.RemoveAsync(key);
            }
            else
            {
                throw new Exception("Key does not exist in the cache.");
            }
        }
    }
}
