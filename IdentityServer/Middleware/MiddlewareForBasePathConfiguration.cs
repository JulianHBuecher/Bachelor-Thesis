using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public static class MiddlewareForBasePathConfiguration
    {
        public static void UseConfigureBasePath(this IApplicationBuilder app, IConfiguration Configuration)
        {
            app.Use(async (context, next) =>
            {
                if (IsFromProxy(context, Configuration) && GetConfigurationValue(Configuration, "BasepathConfig:ProxyBasePath", out var proxyHeader))
                {
                    SetBasePath(context, proxyHeader);
                }
                else
                {
                    // BasePath will not be extended
                }
                await next();
            });
        }
        public static bool IsFromProxy(HttpContext context, IConfiguration Configuration)
        {
            if (GetConfigurationValue(Configuration, "BasepathConfig:ProxyHeader", out var proxyHeader))
            {
                return context.Request.Headers.ContainsKey(proxyHeader);
            } 
            else
            {
                //Log.Information($"ProxyHeader has not accured!");
                return false;
            }
        }
        public static bool GetConfigurationValue(IConfiguration Configuration, string key, out string value)
        {
            value = Configuration[key];
            return !string.IsNullOrWhiteSpace(value);
        }
        public static void SetBasePath(HttpContext context, PathString newBasePath)
        {
            //Log.Information($"BasePath adjusted with {newBasePath}");
            context.Request.PathBase = newBasePath;
        }
    }
}
