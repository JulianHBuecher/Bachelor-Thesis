using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public interface IIPBlocklistService
    { 
        public Task BlocklistIP(HttpContext context);
        public Task<bool> BlocklistIP(string ip);
        public Task<(string IPAddress, bool IsBlocklisted)> IsIPBlocklisted(HttpContext context);
        public Task<List<string>> GetBlocklist();
        public Task<bool> RemoveIPFromBlocklist(string ip);
    }
}
