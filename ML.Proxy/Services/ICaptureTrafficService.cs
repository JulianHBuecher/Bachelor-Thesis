using SharpPcap;

namespace ML.Proxy.Services
{
    public interface ICaptureTrafficService
    {
        public RawCapture? CaptureTraffic();
    }
}
