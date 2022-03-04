using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;

namespace WebAPi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Configure logger
            var logger = NLog.LogManager
                .LoadConfiguration("nlog.config")
                .GetCurrentClassLogger();

            try
            {
                // Loggin the project initialization
                logger.Info("Initializing application...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                logger.Info("Application shutdown...");
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .UseNLog();
                });
    }
}
