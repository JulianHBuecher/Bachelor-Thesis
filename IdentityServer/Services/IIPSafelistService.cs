using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public interface IIPSafelistService
    {
        public Task AddToSafelist(string ip);
        public Task AddToSafelist(HttpContext context);
        public Task<bool> IsOnSafelist(string ip);
        public Task<bool> IsOnSafelist(HttpContext context);
        public Task RemoveFromSafelist(string ip);
        public Task<List<string>> GetSafelistEntries(); 
    }
}
