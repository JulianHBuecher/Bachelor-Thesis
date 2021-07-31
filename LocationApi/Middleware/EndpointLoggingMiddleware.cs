using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LocationApi.Middleware
{
    public class EndpointLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<EndpointLoggingMiddleware> _logger;

        public EndpointLoggingMiddleware(RequestDelegate next, ILogger<EndpointLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Invoking Endpoint with Path {context.Request.Path}");

            await _next(context);
        }
    }

    public static class PathbasedRequestLoggingBuilderExtensions
    {
        public static IApplicationBuilder UsePathbasedRequestLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<EndpointLoggingMiddleware>();
        }
    }

}
