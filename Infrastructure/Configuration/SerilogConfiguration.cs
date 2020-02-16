namespace Microsoft.Extensions.Configuration
{
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class SerilogConfiguration<TStartup> where TStartup : class
    {
        public static void CreateSerilogLogger(IHostEnvironment hostingEnvironment, LoggerConfiguration config)
        {
            var configuration = ConfigureBuilder<TStartup>.GetConfiguration(hostingEnvironment);
            var instrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            config
                .ReadFrom.Configuration(configuration)
                .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Events, Serilog.Events.LogEventLevel.Information)
                .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces, Serilog.Events.LogEventLevel.Information)
                ;
        }
    }
}
