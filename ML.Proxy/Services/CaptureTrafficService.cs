using Microsoft.Extensions.Logging;
using PacketDotNet;
using Serilog;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.IO;
using System.Linq;

namespace ML.Proxy.Services
{
    public class CaptureTrafficService : ICaptureTrafficService
    {
        private readonly ILogger<CaptureTrafficService> _logger;

        public CaptureTrafficService(ILogger<CaptureTrafficService> logger)
        {
            _logger = logger;
        }

        public void CaptureTraffic()
        {
            var ver = Pcap.SharpPcapVersion;
            _logger.LogInformation($"ML.Proxy is using SharpPcap {ver}");

            var devices = LibPcapLiveDeviceList.Instance;

            if (devices.Count < 1)
            {
                _logger.LogError("No devices were found on this machine!");
                return;
            }
            else
            {
                _logger.LogInformation($"{devices.Count} devices were found.");
                foreach (var d in devices) { _logger.LogInformation($"Found Device Name: {d.Description}"); }
            }

            string networkInterface;
            // Check der Laufzeitumgebung (Windows -> lokales Development)
            if (OperatingSystem.IsWindows())
            {
                networkInterface = "Ethernet";
            }
            // Ansonsten Docker Container (Linux)
            else
            {
                networkInterface = "eth0";
            }

            try
            {
                var device = devices
                    .Where(d => d.Interface.FriendlyName != null 
                        && d.Interface.FriendlyName.Equals(networkInterface))
                    .Select(d => d)
                    .First();

                _logger.LogInformation($"Device for Capturing found: {device.Interface.FriendlyName}");

                var readTimeoutMilliseconds = 500;

                // Öffnen des Netzwerkinterfaces für das Abhören des Traffics
                device.Open(new DeviceConfiguration { ReadTimeout = readTimeoutMilliseconds });

                var rawCapture = device.GetNextPacket(out var p);

                var time = p.Header.Timeval.Date;
                var len = p.Data.Length;
                var packet = p.GetPacket();

                var processedPacket = Packet.ParsePacket(packet.LinkLayerType, packet.Data);

                _logger.LogInformation($"{time.Hour}:{time.Minute}:{time.Second}:{time.Millisecond} Len={len}");
                _logger.LogInformation(packet.ToString());

                _logger.LogInformation(device.Statistics.ToString());

                // Schließen des Netzwerkinterfaces
                device.Close();
            }
            catch (Exception e)
            {
                _logger.LogError($"Problem occured by using network interface: \n{e}");
            }
        }
    }
}
