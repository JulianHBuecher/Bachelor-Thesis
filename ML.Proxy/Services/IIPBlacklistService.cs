using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public interface IIPBlacklistService
    { 
        public Task BlacklistIP(HttpContext context);
        public Task<(string IPAddress, bool IsBlacklisted)> IsIPBlacklisted(HttpContext context);
        public Task<List<string>> GetBlacklist();
    }
}
