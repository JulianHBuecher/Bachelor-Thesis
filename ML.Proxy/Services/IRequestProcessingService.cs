using ML.Proxy.Models;
using SharpPcap;
using System;

namespace ML.Proxy.Services
{
    public interface IRequestProcessingService
    {
        public (T1, T2, T3) Transform<T1, T2, T3>(DateTime timestamp, RawPacketCapture request) 
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new();
    }
}
