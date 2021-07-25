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

        public async Task AddInitialPacket(RawPacketCapture packet)
        {
            await _cache.SetAsync(initialCacheKey, packet);
            await _cache.SetAsync(cacheKey, packet);
        }

        public async Task AddNewPacket(RawPacketCapture lastPacket)
        {
            var timestamp = lastPacket.Timeval.Date;

            // Hinzufügen des alten letzten Paketes für die Historie
            await _cache.UpdateLabelAsync(cacheKey, timestamp.ToBinary().ToString());
            // Setzen eines neuen letzten Paketes für die Zeitstempel
            await _cache.SetAsync(cacheKey, lastPacket);
        }

        public async Task<(RawPacketCapture initialPacket, RawPacketCapture lastPacket)> GetInitialAndLastPacket()
        {
            var firstPacket = await _cache.GetAsync<RawPacketCapture>(initialCacheKey);
            var lastPacket = await _cache.GetAsync<RawPacketCapture>(cacheKey);

            return (firstPacket, lastPacket);
        }

        public async Task<RawPacketCapture> GetInitialPacket()
        {
            var firstPacket = await _cache.GetAsync<RawPacketCapture>(initialCacheKey);
            return firstPacket;
        }

        public async Task<RawPacketCapture> GetLastPacket()
        {
            var lastPacket = await _cache.GetAsync<RawPacketCapture>(cacheKey);
            return lastPacket;
        }
    }
}
