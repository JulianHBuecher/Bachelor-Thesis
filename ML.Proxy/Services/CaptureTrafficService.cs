using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

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
            }

            // Here to continue (Error occure)
            var device = devices
                .Where(d => d.Interface.FriendlyName.Contains("Default Switch"))
                .Select(d => d).First();

        }
    }
}
