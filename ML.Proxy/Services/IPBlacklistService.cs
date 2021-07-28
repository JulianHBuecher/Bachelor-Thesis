using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public class IPBlacklistService : IIPBlacklistService
    {
        private readonly IRedisCacheService _cache;
        private readonly ILogger<IPBlacklistService> _logger;
        private readonly string _cacheKey = "Attacker-IP";

        public IPBlacklistService(IRedisCacheService cache, ILogger<IPBlacklistService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task BlacklistIP(HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            if (string.IsNullOrEmpty(ip)) { return; }

            var blacklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (blacklist.Any() && blacklist.Contains(ip))
            {
                _logger.LogInformation($"IP-Address {ip} is already on blacklist.");

            }
            else
            {
                blacklist.Add(ip);

                await _cache.SetAsync(_cacheKey, blacklist);

                _logger.LogInformation($"IP-Address {ip} is now on blacklist.");
            }
        }

        public async Task<bool> BlacklistIP(string ip)
        {
            if (string.IsNullOrEmpty(ip)) { return default; }

            var blacklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (blacklist.Any() && blacklist.Contains(ip))
            {
                _logger.LogWarning($"{ip} is already blacklisted.");

                return false;
            }
            else
            {
                blacklist.Add(ip);

                await _cache.SetAsync(_cacheKey, blacklist);

                return true;
            }
        }

        public async Task<(string IPAddress, bool IsBlacklisted)> IsIPBlacklisted(HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            if (string.IsNullOrEmpty(ip)) { return default; }

            var blacklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (blacklist.Any() && blacklist.Contains(ip))
            {
                _logger.LogWarning($"{ip} is blacklisted and regards to potential attacker.");

                return (ip, true);
            }

            _logger.LogInformation($"{ip} is not blacklisted and will be further processed.");

            return (ip, false);
        }

        public async Task<List<string>> GetBlacklist()
        {
            var blacklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            return blacklist;
        }

        public async Task<bool> RemoveIPFromBlacklist(string ip)
        {
            if (string.IsNullOrEmpty(ip)) { return default; }

            var blacklist = await _cache.GetAsync<List<string>>(_cacheKey) ?? new List<string>();

            if (blacklist.Any() && !blacklist.Contains(ip))
            {
                _logger.LogWarning($"{ip} is not on blacklist.");

                return false;
            }
            else
            {
                blacklist.Remove(ip);

                await _cache.SetAsync(_cacheKey, blacklist);

                _logger.LogInformation($"{ip} is removed from blacklist.");

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
