using ML.Proxy.Models;

namespace ML.Proxy.Services
{
    public interface ICaptureTrafficService
    {
#nullable enable
        public RawPacketCapture? CaptureTraffic();
    }
}
