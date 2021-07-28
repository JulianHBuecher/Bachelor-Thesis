﻿using Microsoft.AspNetCore.Authorization;
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
        [Authorize("HasReadScope")]
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

        [HttpPost("add")]
        [Authorize("HasWriteScope")]
        public async Task<ActionResult> AddIPToBlacklist([FromQuery] string ip)
        {
            var isAddedToBlackist = await _blacklistService.BlacklistIP(ip);

            if (isAddedToBlackist)
            {
                return Ok($"{ip} is added to blacklist.");
            }
            else
            {
                return BadRequest($"{ip} already exists on blacklist.");
            }

        }

        [HttpDelete("remove")]
        [Authorize("HasDeleteScope")]
        public async Task<ActionResult> RemoveIPFromBlacklist([FromQuery] string ip)
        {
            var isRemovedFromBlacklist = await _blacklistService.RemoveIPFromBlacklist(ip);

            if (isRemovedFromBlacklist)
            {
                return Ok($"{ip} is successfully removed from blacklist.");
            }
            else
            {
                return NotFound($"{ip} is not on blacklist.");
            }
        }
    }
}
