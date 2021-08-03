using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ML.Proxy.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Proxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlocklistController : ControllerBase
    {
        private readonly ILogger<BlocklistController> _logger;
        private readonly IIPBlocklistService _blocklistService;

        public BlocklistController(ILogger<BlocklistController> logger, IIPBlocklistService blacklistService)
        {
            _logger = logger;
            _blocklistService = blacklistService;
        }

        [HttpGet]
        [Authorize("HasReadScope")]
        public async Task<ActionResult> GetBlocklist()
        {
            var blacklist = await _blocklistService.GetBlocklist();

            if (blacklist.Any())
            {
                return Ok(blacklist);
            }
            else
            {
                return NotFound("No Addresses are blocklisted.");
            }
        }

        [HttpPost("add")]
        [Authorize("HasWriteScope")]
        public async Task<ActionResult> AddIPToBlocklist([FromQuery] string ip)
        {
            var isAddedToBlackist = await _blocklistService.BlocklistIP(ip);

            if (isAddedToBlackist)
            {
                return Ok($"{ip} is added to blocklist.");
            }
            else
            {
                return BadRequest($"{ip} already exists on blocklist.");
            }

        }

        [HttpDelete("remove")]
        [Authorize("HasDeleteScope")]
        public async Task<ActionResult> RemoveIPFromBlocklist([FromQuery] string ip)
        {
            var isRemovedFromBlocklist = await _blocklistService.RemoveIPFromBlocklist(ip);

            if (isRemovedFromBlocklist)
            {
                return Ok($"{ip} is successfully removed from blocklist.");
            }
            else
            {
                return NotFound($"{ip} is not on blocklist.");
            }
        }
    }
}
