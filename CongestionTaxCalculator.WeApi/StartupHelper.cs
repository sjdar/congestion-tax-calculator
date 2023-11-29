using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CongestionTaxCalculator.WeApi
{
    public static class StartupHelper
    {
        public static void AddLogging(this IApplicationBuilder app, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            Log.Logger.Information("App Start");
        }
    }
}
