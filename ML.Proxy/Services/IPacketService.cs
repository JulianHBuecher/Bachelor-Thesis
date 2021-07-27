using Microsoft.AspNetCore.Http;
using ML.Proxy.Models;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public interface IPacketService
    {
        public Task<RawPacketCapture> GetInitialPacket(HttpContext context);
        public Task<RawPacketCapture> GetLastPacket(HttpContext context);
        public Task<(RawPacketCapture initialPacket, RawPacketCapture lastPacket)> GetInitialAndLastPacket(HttpContext context);
        public Task AddNewPacket(RawPacketCapture packet, HttpContext context);
        public Task AddInitialPacket(RawPacketCapture packet, HttpContext context);
    }
}
