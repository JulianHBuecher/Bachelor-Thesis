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
#nullable enable
        public RawPacketCapture? CaptureTraffic()
        {
            var devices = LibPcapLiveDeviceList.Instance;

            if (devices.Count < 1)
            {
                _logger.LogError("No devices were found on this machine!");
                return null;
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
                
                    // Öffnen des Netzwerkinterfaces für das Abhören des Traffics
                    device.Open(new DeviceConfiguration 
                    { 
                        Mode = DeviceModes.Promiscuous, 
                        ReadTimeout = 500 
                    });

                    var rawCapture = device.GetNextPacket(out var cPacket);
                
                    if (cPacket.Header is not null)
                    {
                        var time = cPacket.Header.Timeval.Date;
                        var len = cPacket.Data.Length;
                        var packet = cPacket.GetPacket();

                        _logger.LogInformation($"{time.Hour}:{time.Minute}:{time.Second}:{time.Millisecond} Len={len}");
                        _logger.LogInformation(packet.ToString());

                        _logger.LogInformation(device.Statistics.ToString());

                        // Schließen des Netzwerkinterfaces
                        // In der Docker-Linux-Umgebung muss das Interface geöffnet bleiben,
                        // ansonsten werden nachfolgende Requests erst nach erneutem Öffnen des
                        // Interfaces abgehört
                        if (!OperatingSystem.IsLinux())
                        {
                            device.Close();
                        }
                    
                        return new RawPacketCapture(packet);
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                _logger.LogError($"Problem occured by using network interface: \n{e}");
                return null;
            }
        }
    }
}
