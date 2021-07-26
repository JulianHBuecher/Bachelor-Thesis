﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
                    policy.RequireScopeEither("safelist.read","blacklist.read");
                });
                o.AddPolicy("HasWriteScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireScope("safelist.write");
                });
                o.AddPolicy("HasUpdateScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireScope("safelist.update");
                });
                o.AddPolicy("HasDeleteScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireScope("safelist.delete");
                });
            });

            // Explicitly adding the ScopeHandlers
            // Otherwise they will not be registered
            services.AddSingleton<IAuthorizationHandler, CustomScopeHandler>();
            services.AddSingleton<IAuthorizationHandler, CustomAnyScopeHandler>();
        }
    }
}
