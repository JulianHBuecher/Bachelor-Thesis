using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ML.Proxy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Adding the proxy funtions to the app
            var proxyBuilder = services.AddReverseProxy();
            // Initializing the reverse proxy (by configuration out of appsettings.json)
            proxyBuilder.LoadFromConfig(Configuration.GetSection("ML.Proxy"));
            // Adding ThrottlR services for configuring default policy
            // See Reference: https://github.com/Kahbazi/ThrottlR/tree/release/v2
            services.AddThrottlR(options =>
            {
                // Configuring default policy
                options.AddDefaultPolicy(policy =>
                {
                    // Adding a general rule for all ips
                    policy.WithGeneralRule(TimeSpan.FromSeconds(10), 3); // 3 requests could be called every 10 seconds
                });
            }).AddInMemoryCounterStore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Middleware added before UseRouting() will see all requests and can manipulate them
            // before any routing takes place
            app.UseRouting();

            // Middleware for Throttler regarding throttling incoming requests
            app.UseThrottler();

            // Middleware added between UseRouting() and UseEndpoints() can call HttpContext.GetEndpoint()
            // to check which endpoint routing matched the request to (if any), and use
            // any metadata that was associated with that endpoint
            app.UseEndpoints(endpoints =>
            {
                // Register Reverse Proxy Routes and Enable Throttling for these Routes
                endpoints.MapReverseProxy().Throttle();
            });
        }
    }
}
