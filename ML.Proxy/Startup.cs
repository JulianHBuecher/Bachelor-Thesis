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

            // Middleware added between UseRouting() and UseEndpoints() can call HttpContext.GetEndpoint()
            // to check which endpoint routing matched the request to (if any), and use
            // any metadata that was associated with that endpoint
            app.UseEndpoints(endpoints =>
            {
                // Register Reverse Proxy Routes
                endpoints.MapReverseProxy();
            });
        }
    }
}
