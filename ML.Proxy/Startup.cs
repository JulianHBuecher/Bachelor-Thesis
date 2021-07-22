using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ML.Proxy.Middleware;
using ML.Proxy.Services;
using System;

namespace ML.Proxy
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //ThreadPool.SetMinThreads(20, 20);

            // Adding the proxy funtions to the app
            var proxyBuilder = services.AddReverseProxy();
            // Initializing the reverse proxy (by configuration out of appsettings.json)
            proxyBuilder.LoadFromConfig(_configuration.GetSection("ML.Proxy"));

            if (_hostEnvironment.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    //options.Configuration = _configuration.GetValue<string>("Redis:Connection-String");
                    options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions
                    {
                        EndPoints = { _configuration.GetValue<string>("Redis:Connection-String") },
                        AbortOnConnectFail = true
                    };
                });
            }

            // Adding a PredictionEnginePool for loading the model in a thread-safe environment
            // Laden der ML-Modelle von Remote
            services.AddMachineLearningAttackPrediction(_configuration);

            // Adding ThrottlR services for configuring default policy
            // See Reference: https://github.com/Kahbazi/ThrottlR/tree/release/v2
            services.AddThrottlR(options =>
            {
                // Configuring default policy
                options.AddDefaultPolicy(policy =>
                {
                    // Adding a general rule for all ips
                    //policy.WithGeneralRule(TimeSpan.FromSeconds(10), 3); // 3 requests could be called every 10 seconds
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

            // Middleware for Prediction of Attack
            app.UseAttackPredictionMiddleware();

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
