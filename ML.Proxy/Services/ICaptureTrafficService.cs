using ML.Proxy.Models;

namespace ML.Proxy.Services
{
    public interface ICaptureTrafficService
    {
        public RawPacketCapture? CaptureTraffic();
    }
}
