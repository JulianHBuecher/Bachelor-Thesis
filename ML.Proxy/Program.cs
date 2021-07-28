using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ML.Proxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                })
                .UseSerilog((ctx, cfg) =>
                {
                    cfg.ReadFrom.Configuration(ctx.Configuration);
                    cfg.Enrich.WithProperty("EnvironmentName", ctx.HostingEnvironment.EnvironmentName);
                })
                .ConfigureAppConfiguration((ctx, cfg) =>
                {
                    // Read my appsettings.json in Runtime
                    cfg.AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
