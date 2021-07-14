using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public class RequestProcessingService : IRequestProcessingService
    {
        private readonly ILogger<RequestProcessingService> _logger;

        public RequestProcessingService(ILogger<RequestProcessingService> logger)
        {
            _logger = logger;
        }

        public async Task<T> TransformRequest<T>(HttpRequest request) where T : class, new()
        {
            // Implementierung einer Methode zur Konvertierung eines HttpRequests in das gewünschte Format
            var payloadSize = request.Body.Length;

            T transformedRequest = new T();

            return transformedRequest;
        }
    }
}
