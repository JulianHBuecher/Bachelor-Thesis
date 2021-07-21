using ML.Proxy.DataModels;
using ML.Proxy.Models;
using System;

namespace ML.Proxy.Services
{
    public interface IRequestProcessingService
    {
        public (GoldenEyeTrafficData, LOICTrafficData, SlowlorisTrafficData) Transform(DateTime initialTimestamp, DateTime timestamp, RawPacketCapture request);
    }
}
