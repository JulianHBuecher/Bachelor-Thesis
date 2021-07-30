using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public class IPSafelistService : IIPSafelistService
    {
        private readonly IRedisCacheService _cache;
        private readonly ILogger<IPSafelistService> _logger;
        private readonly string _cacheKey = "Safelist";

        public IPSafelistService(IRedisCacheService cache, ILogger<IPSafelistService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task AddToSafelist(string ip)
        {
            if (string.IsNullOrEmpty(ip)) { return; }

            var safelist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (safelist.Any() && safelist.Contains(ip))
            {
                _logger.LogInformation($"IP-Address {ip} is already on safelist.");
            }
            else
            {
                safelist.Add(ip);

                await _cache.SetAsync($"{_cacheKey}", safelist);

                _logger.LogInformation($"IP-Address {ip} is now on safelist.");
            }
        }

        public async Task<List<string>> GetSafelistEntries()
        {
            var safelist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            return safelist;
        }

        public async Task<bool> IsOnSafelist(string ip)
        {
            if (string.IsNullOrEmpty(ip)) { return default; }

            var safelist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (safelist.Any() && safelist.Contains(ip))
            {
                _logger.LogInformation($"IP-Address {ip} is on safelist.");
                return true;
            }
            else
            {
                _logger.LogInformation($"IP-Address {ip} is not on safelist.");
                return false;
            }
        }

        public async Task<bool> IsOnSafelist(HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            if (string.IsNullOrEmpty(ip)) { return default; }

            var safelist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (safelist.Any() && safelist.Contains(ip))
            {
                _logger.LogInformation($"IP-Address {ip} is on safelist.");
                return true;
            }
            else
            {
                _logger.LogInformation($"IP-Address {ip} is not on safelist.");
                return false;
            }
        }

        public async Task RemoveFromSafelist(string ip)
        {
            if (string.IsNullOrEmpty(ip)) { return; }

            var safelist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (safelist.Any() && safelist.Contains(ip))
            {
                safelist.Remove(ip);

                await _cache.SetAsync($"{_cacheKey}", safelist);

                _logger.LogInformation($"IP-Address {ip} is removed from safelist.");
            }
            else
            {
                _logger.LogError($"IP-Address {ip} is not in safelist.");
            }
            
        }

        public static string GetClientIPAddress(HttpContext context)
        {
            // Extrahiere die Client IP Adresse des Senders aus dem NGINX Header
            var containsHeaders = context.Request.Headers.TryGetValue("X-Forwarded-For", out var value);

            if (containsHeaders)
            {
                return value.ToString();
            }

            return default;
        }
    }
}
