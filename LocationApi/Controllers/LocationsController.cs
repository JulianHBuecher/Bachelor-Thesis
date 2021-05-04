using LocationApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LocationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(ILogger<LocationsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize("HasReadScope")]
        public IEnumerable<Location> Get()
        {
            return new List<Location>()
            {
                new Location
                {
                    Name = "Berlin",
                    Country = "Germany",
                    CountryCode = "DE",
                    Population = 3_645_000
                },
                new Location
                {
                    Name = "Tokyo",
                    Country = "Japan",
                    CountryCode = "JP",
                    Population = 13_960_236
                },
                new Location
                {
                    Name = "Sydney",
                    Country = "Australia",
                    CountryCode = "AU",
                    Population = 5_312_000
                },
                new Location
                {
                    Name = "Graz",
                    Country = "Austria",
                    CountryCode = "AT",
                    Population = 283_869
                },
                new Location
                {
                    Name = "Zurich",
                    Country = "Switzerland",
                    CountryCode = "CH",
                    Population = 402_762
                }
            };
        }
    }
}
