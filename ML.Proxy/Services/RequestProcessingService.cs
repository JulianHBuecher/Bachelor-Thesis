using Microsoft.Extensions.Logging;
using ML.Proxy.DataModels;
using ML.Proxy.Models;
using PacketDotNet;
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

        public (GoldenEyeTrafficData, LOICTrafficData, SlowlorisTrafficData) Transform(
            DateTime initialTimestamp, 
            DateTime timestampLastPacket, 
            RawPacketCapture request) 
        {

            var packet = Packet.ParsePacket(request.LinkLayerType, request.Data);

            var actualPacketBinaryTimestamp = request.Timeval.Date.ToBinary();
            var lastPacketBinaryTimestamp = timestampLastPacket.ToBinary();
            var initialPacketBinaryTimestamp = initialTimestamp.ToBinary();

            var networkAttack = new NetworkAttack()
            {
                BwdPktLenStd = 0,
                FlowIATMin   = actualPacketBinaryTimestamp - lastPacketBinaryTimestamp,
                FwdIATMin    = actualPacketBinaryTimestamp - lastPacketBinaryTimestamp,
                FlowIATMean  = actualPacketBinaryTimestamp - lastPacketBinaryTimestamp,
                PktSizeAvg   = packet.PayloadPacket.Bytes.Length,
                FlowDuration = lastPacketBinaryTimestamp - initialPacketBinaryTimestamp,
                FlowIATStd   = actualPacketBinaryTimestamp - lastPacketBinaryTimestamp,
                BwdIATMean   = 0,
                FwdIATMean   = actualPacketBinaryTimestamp - lastPacketBinaryTimestamp
            };
            
            return (
                new GoldenEyeTrafficData(networkAttack),
                new LOICTrafficData(networkAttack),
                new SlowlorisTrafficData(networkAttack) 
                );
        }
    }
}
