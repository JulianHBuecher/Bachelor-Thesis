using Microsoft.AspNetCore.Builder;

namespace ML.Proxy.Middleware
{
    public static class PredictionMiddlewareBuilderExtension
    {
        public static IApplicationBuilder UseAttackPredictionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PredictionMiddleware>();
        }
    }
}
