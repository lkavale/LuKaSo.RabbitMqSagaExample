using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace LuKaSo.RabbitMqSagaExample.StrategyB
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddLog4Net($"log4net.config");
                    logging.SetMinimumLevel(LogLevel.Debug);
                });
        }
    }
}
