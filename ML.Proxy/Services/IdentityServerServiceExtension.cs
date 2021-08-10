using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace ML.Proxy.Services
{
    public static class IdentityServerServiceExtension
    {
        public static void AddIdentityServerConnection(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", o =>
                {
                    o.Authority = _configuration.GetValue<string>("IdentityServer:Authority");
                    o.Audience = _configuration.GetValue<string>("IdentityServer:Audience");
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidIssuers = _configuration.GetSection("IdentityServer:ValidIssuers").GetChildren().Select(i => i.Value).ToList()
                    };
                    o.RequireHttpsMetadata = _configuration.GetValue<bool>("IdentityServer:RequireHttpsMetadata");
                });
            services.AddAuthorization(o =>
            {
                o.AddPolicy("HasReadScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireScopeEither("safelist.read", "blacklist.read");
                });
                o.AddPolicy("HasWriteScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireScopeEither("safelist.write", "blacklist.write");
                });
                o.AddPolicy("HasUpdateScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireScopeEither("safelist.update", "blacklist.update");
                });
                o.AddPolicy("HasDeleteScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireScopeEither("safelist.delete", "blacklist.delete");
                });
            });

            // Explicitly adding the ScopeHandlers
            // Otherwise they will not be registered
            services.AddSingleton<IAuthorizationHandler, CustomScopeHandler>();
            services.AddSingleton<IAuthorizationHandler, CustomAnyScopeHandler>();
        }
    }
}
