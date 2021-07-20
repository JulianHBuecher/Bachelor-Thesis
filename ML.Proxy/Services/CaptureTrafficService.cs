using Microsoft.Extensions.Logging;
using ML.Proxy.Models;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
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

        public RawPacketCapture? CaptureTraffic()
        {
            var ver = Pcap.SharpPcapVersion;
            _logger.LogInformation($"ML.Proxy is using SharpPcap {ver}");

            var devices = LibPcapLiveDeviceList.Instance;

            if (devices.Count < 1)
            {
                _logger.LogError("No devices were found on this machine!");
                return null;
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
                    .FirstOrDefault();

                if (device is not null)
                {
                    _logger.LogInformation($"Device for Capturing found: {device.Interface.FriendlyName}");
                }

                var readTimeoutMilliseconds = 500;

                // Öffnen des Netzwerkinterfaces für das Abhören des Traffics
                device.Open(new DeviceConfiguration 
                { 
                    Mode = DeviceModes.Promiscuous, 
                    ReadTimeout = readTimeoutMilliseconds 
                });


                var rawCapture = device.GetNextPacket(out var cPacket);
                
                var time = cPacket.Header.Timeval.Date;
                var len = cPacket.Data.Length;
                var packet = cPacket.GetPacket();
                
                _logger.LogInformation($"{time.Hour}:{time.Minute}:{time.Second}:{time.Millisecond} Len={len}");
                _logger.LogInformation(packet.ToString());

                _logger.LogInformation(device.Statistics.ToString());

                // Schließen des Netzwerkinterfaces
                // In der Docker-Linux-Umgebung muss das Interface geöffnet bleiben,
                // ansonsten werden nachfolgende Requests erst nach erneutem Öffnen des
                // Interfaces geöffnet
                if (!OperatingSystem.IsLinux())
                {
                    device.Close();
                }


                return new RawPacketCapture(packet);
            }
            catch (Exception e)
            {
                _logger.LogError($"Problem occured by using network interface: \n{e}");
                return null;
            }
        }
    }
}
