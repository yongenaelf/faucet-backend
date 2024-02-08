using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DeFiPulse.Web
{
    public class Program
    {
        public static int Main(string[] args)
        {
            ILogger<Program> logger = NullLogger<Program>.Instance;

            try
            {
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                if (logger == NullLogger<Program>.Instance)
                {
                    Console.WriteLine(ex);
                }

                logger.LogCritical(ex, "Host terminated unexpectedly!");
                return 1;
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac();
    }
}
