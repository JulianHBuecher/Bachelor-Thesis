using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ML.Proxy.Services;
using System.Threading.Tasks;

namespace ML.Proxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SafelistController : ControllerBase
    {
        private readonly ILogger<SafelistController> _logger;
        private readonly IIPSafelistService _safelist;

        public SafelistController(ILogger<SafelistController> logger, IIPSafelistService safelist)
        {
            _logger = logger;
            _safelist = safelist;
        }

        [HttpGet]
        public async Task<ActionResult> GetSafelist()
        {
            var entries = await _safelist.GetSafelistEntries();

            if (entries is not null || entries.Count is not 0)
            {
                return Ok(entries);
            }

            return NotFound("No entries exist!");
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddToSafelist([FromQuery] string ip)
        {
            var isOnSafelist = await _safelist.IsOnSafelist(ip);
            
            if (isOnSafelist)
            {
                return BadRequest("IP is already on safelist.");
            }

            await _safelist.AddToSafelist(ip);

            _logger.LogInformation($"IP Address {ip} is added to safelist.");

            return Ok("Added to safelist");
        }

        [HttpPost("remove")]
        public async Task<ActionResult> RemoveFromSafelist([FromQuery] string ip)
        {
            var isOnSafelist = await _safelist.IsOnSafelist(ip);

            if (!isOnSafelist)
            {
                return BadRequest("IP is not on safelist.");
            }

            await _safelist.RemoveFromSafelist(ip);

            _logger.LogInformation($"IP Address {ip} removed from safelist.");

            return Ok("IP removed from safelist.");
        }
    }
}
