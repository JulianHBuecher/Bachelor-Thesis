using ML.Proxy.Models;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public interface IPacketService
    {
        public Task<RawPacketCapture> GetInitialPacket();
        public Task<RawPacketCapture> GetLastPacket();
        public Task<(RawPacketCapture initialPacket, RawPacketCapture lastPacket)> GetInitialAndLastPacket();
        public Task AddNewPacket(RawPacketCapture packet);
        public Task AddInitialPacket(RawPacketCapture packet);
    }
}
