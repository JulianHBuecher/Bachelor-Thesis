using Microsoft.Extensions.Logging;
using ML.Proxy.DataModels;
using ML.Proxy.Models;
using PacketDotNet;
using SharpPcap;
using System;

namespace ML.Proxy.Services
{
    public class RequestProcessingService : IRequestProcessingService
    {
        private readonly ILogger<RequestProcessingService> _logger;

        public RequestProcessingService(ILogger<RequestProcessingService> logger)
        {
            _logger = logger;
        }

        public (T1, T2, T3) Transform<T1, T2, T3>(DateTime timestamp, RawPacketCapture request) 
            where T1 : class, new()
            where T2 : class, new()
            where T3 : class, new()
        {

            var packet = Packet.ParsePacket(request.LinkLayerType, request.Data);

            var networkAttack = new NetworkAttack()
            {
                BwdPktLenStd = 0,
                FlowIATMin   = (float)(request.Timeval.Date - timestamp).TotalMilliseconds,
                FwdIATMin    = (float)(request.Timeval.Date - timestamp).TotalMilliseconds,
                FlowIATMean  = (float)(request.Timeval.Date - timestamp).TotalMilliseconds,
                //PktSizeAvg   = request.PacketLength,
                // Hier könnten noch die Anzahl der bisherigen eingegangenen Pakete dividiert werden.
                // Diese könnten im Redis Cache vorgehalten werden
                PktSizeAvg = packet.PayloadPacket.Bytes.Length,
                FlowDuration = (float)(timestamp - request.Timeval.Date).TotalMilliseconds,
                FlowIATStd   = (float)(request.Timeval.Date - timestamp).TotalMilliseconds,
                BwdIATMean   = 0,
                FwdIATMean   = (float)(request.Timeval.Date - timestamp).TotalMilliseconds
            };
            
            return (
                new GoldenEyeTrafficData(networkAttack) as T1, 
                new LOICTrafficData(networkAttack) as T2, 
                new SlowlorisTrafficData(networkAttack) as T3
                );
        }
    }
}
