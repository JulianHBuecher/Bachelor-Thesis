using PacketDotNet;
using SharpPcap;
using System.Text.Json.Serialization;

namespace ML.Proxy.Models
{
    public class RawPacketCapture
    {
        public LinkLayers LinkLayerType { get; set; }
        public PosixTimeval Timeval { get; set; }
        public byte[] Data { get; set; }
        public int? PacketLength { get; set; } = null;

        public RawPacketCapture() { }

        [JsonConstructor]
        public RawPacketCapture(RawCapture capture)
        {
            LinkLayerType = capture.LinkLayerType;
            Timeval = capture.Timeval;
            Data = capture.Data;
            PacketLength ??= capture.PacketLength;
        }
    }
}
