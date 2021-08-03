using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public class IPBlocklistService : IIPBlocklistService
    {
        private readonly IRedisCacheService _cache;
        private readonly ILogger<IPBlocklistService> _logger;
        private readonly string _cacheKey = "Blocklist";

        public IPBlocklistService(IRedisCacheService cache, ILogger<IPBlocklistService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task BlocklistIP(HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            if (string.IsNullOrEmpty(ip)) { return; }

            var blocklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (blocklist.Any() && blocklist.Contains(ip))
            {
                _logger.LogInformation($"IP-Address {ip} is already on blocklist.");

            }
            else
            {
                blocklist.Add(ip);

                await _cache.SetAsync(_cacheKey, blocklist);

                _logger.LogInformation($"IP-Address {ip} is now on blocklist.");
            }
        }

        public async Task<bool> BlocklistIP(string ip)
        {
            if (string.IsNullOrEmpty(ip)) { return default; }

            var blocklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (blocklist.Any() && blocklist.Contains(ip))
            {
                _logger.LogWarning($"{ip} is already blocklisted.");

                return false;
            }
            else
            {
                blocklist.Add(ip);

                await _cache.SetAsync(_cacheKey, blocklist);

                return true;
            }
        }

        public async Task<(string IPAddress, bool IsBlocklisted)> IsIPBlocklisted(HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            if (string.IsNullOrEmpty(ip)) { return default; }

            var blocklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (blocklist.Any() && blocklist.Contains(ip))
            {
                _logger.LogWarning($"{ip} is blocklisted and regards to potential attacker.");

                return (ip, true);
            }

            _logger.LogInformation($"{ip} is not blocklisted and will be further processed.");

            return (ip, false);
        }

        public async Task<List<string>> GetBlocklist()
        {
            var blocklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            return blocklist;
        }

        public async Task<bool> RemoveIPFromBlocklist(string ip)
        {
            if (string.IsNullOrEmpty(ip)) { return default; }

            var blocklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (blocklist.Any() && !blocklist.Contains(ip))
            {
                _logger.LogWarning($"{ip} is not on blocklist.");

                return false;
            }
            else
            {
                blocklist.Remove(ip);

                await _cache.SetAsync(_cacheKey, blocklist);

                _logger.LogInformation($"{ip} is removed from blocklist.");

                return true;
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
