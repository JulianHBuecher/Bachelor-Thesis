using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public interface IRequestProcessingService
    {
        public Task<T> TransformRequest<T>(HttpRequest request) where T : class, new();
    }
}
