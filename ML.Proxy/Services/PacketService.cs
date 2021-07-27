using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ML.Proxy.Models;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public class PacketService : IPacketService
    {
        private readonly IRedisCacheService _cache;
        private readonly ILogger<PacketService> _logger;

        private readonly string cacheKey = "Last-Packet";
        private readonly string initialCacheKey = "First-Packet";

        public PacketService(IRedisCacheService cache, ILogger<PacketService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task AddInitialPacket(RawPacketCapture packet, HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            await _cache.SetAsync($"{initialCacheKey}-{ip}", packet);
            await _cache.SetAsync($"{cacheKey}-{ip}", packet);
        }

        public async Task AddNewPacket(RawPacketCapture lastPacket, HttpContext context)
        {
            var timestamp = lastPacket.Timeval.Date;
            var ip = GetClientIPAddress(context);

            // Hinzufügen des alten letzten Paketes für die Historie
            await _cache.UpdateLabelAsync($"{cacheKey}-{ip}", $"{timestamp.ToBinary()}-{ip}");
            // Setzen eines neuen letzten Paketes für die Zeitstempel
            await _cache.SetAsync($"{cacheKey}-{ip}", lastPacket);
        }

        public async Task<(RawPacketCapture initialPacket, RawPacketCapture lastPacket)> GetInitialAndLastPacket(HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            var firstPacket = await _cache.GetAsync<RawPacketCapture>($"{initialCacheKey}-{ip}");
            var lastPacket = await _cache.GetAsync<RawPacketCapture>($"{cacheKey}-{ip}");

            return (firstPacket, lastPacket);
        }

        public async Task<RawPacketCapture> GetInitialPacket(HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            var firstPacket = await _cache.GetAsync<RawPacketCapture>($"{initialCacheKey}-{ip}");
            return firstPacket;
        }

        public async Task<RawPacketCapture> GetLastPacket(HttpContext context)
        {
            var ip = GetClientIPAddress(context);

            var lastPacket = await _cache.GetAsync<RawPacketCapture>($"{cacheKey}-{ip}");
            return lastPacket;
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
