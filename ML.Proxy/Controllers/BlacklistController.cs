using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ML.Proxy.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Proxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlacklistController : ControllerBase
    {
        private readonly ILogger<BlacklistController> _logger;
        private readonly IIPBlacklistService _blacklistService;

        public BlacklistController(ILogger<BlacklistController> logger, IIPBlacklistService blacklistService)
        {
            _logger = logger;
            _blacklistService = blacklistService;
        }

        [HttpGet]
        public async Task<ActionResult> GetBlacklist()
        {
            var blacklist = await _blacklistService.GetBlacklist();

            if (blacklist.Any())
            {
                return Ok(blacklist);
            }
            else
            {
                return NotFound("No Addresses are blacklisted.");
            }
        }
    }
}
