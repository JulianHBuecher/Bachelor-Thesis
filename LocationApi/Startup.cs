using LocationApi.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using System.Linq;

namespace LocationApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.Configure<ForwardedHeadersOptions>(o =>
            {
                o.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", o =>
                {
                    o.Authority = Configuration.GetValue<string>("IdentityServer:Authority");
                    o.Audience = Configuration.GetValue<string>("IdentityServer:Audience");
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidIssuers = Configuration.GetSection("IdentityServer:ValidIssuers").GetChildren().Select(i => i.Value).ToList()
                    };
                    o.RequireHttpsMetadata = Configuration.GetValue<bool>("IdentityServer:RequireHttpsMetadata");
                });
            services.AddAuthorization(o =>
            {
                o.AddPolicy("HasReadScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireScope("weatherdata.read");
                });
            });
            services.AddSingleton<IAuthorizationHandler, CustomScopeHandler>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocationApi", Version = "v1" });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocationApi v1"));
            }

            app.UseForwardedHeaders();

            app.UseRouting();
            app.UseCors("AllowAllHeaders");

            // For Prometheus and Grafana
            app.UseMetricServer();
            app.UseHttpMetrics();
            
            // For Elasticsearch and Kibana
            app.UseSerilogRequestLogging();
            app.UsePathbasedRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
