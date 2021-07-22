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

            var iatCalculation = actualPacketBinaryTimestamp - lastPacketBinaryTimestamp < 0 ? 0 : actualPacketBinaryTimestamp - lastPacketBinaryTimestamp;
            var flowCalculation = lastPacketBinaryTimestamp - initialPacketBinaryTimestamp < 0 ? 0 : actualPacketBinaryTimestamp - initialPacketBinaryTimestamp;
            
            var networkAttack = new NetworkAttack()
            {
                BwdPktLenStd = 0,
                FlowIATMin   = iatCalculation,
                FwdIATMin    = iatCalculation,
                FlowIATMean  = iatCalculation,
                PktSizeAvg   = packet.PayloadPacket.Bytes.Length,
                FlowDuration = flowCalculation,
                FlowIATStd   = iatCalculation,
                BwdIATMean   = 0,
                FwdIATMean   = iatCalculation,
            };
            
            return (
                new GoldenEyeTrafficData(networkAttack),
                new LOICTrafficData(networkAttack),
                new SlowlorisTrafficData(networkAttack) 
                );
        }
    }
}
