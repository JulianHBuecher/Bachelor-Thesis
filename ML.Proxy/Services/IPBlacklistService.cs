using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

            var value = await _cache.GetAsync<string>($"{_cacheKey}-{ip}");

            if (value is not null)
            {
                _logger.LogInformation($"IP-Address {ip} is already on blacklist.");
            }
            else
            {
                await _cache.SetAsync($"{_cacheKey}-{ip}", ip);

                _logger.LogInformation($"IP-Address {ip} is now on blacklist.");
            }
        }

        public async Task<(string IPAddress, bool IsBlacklisted)> IsIPBlacklisted(HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            if (string.IsNullOrEmpty(ip)) { return default; }

            _logger.LogInformation($"X-Forwarded-Header contains: {ip} and the Remote IP Address is: {ip}");

            var value = await _cache.GetAsync<string>($"{_cacheKey}-{ip}");

            if (value is not null)
            {
                _logger.LogWarning($"{ip} is blacklisted and regards to potential attacker.");
                return (ip, true);
            }

            _logger.LogInformation($"{ip} is not blacklisted and will be further processed.");
            return (ip, false);
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
