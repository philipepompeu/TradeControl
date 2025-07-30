using OpenTelemetry.Metrics;

namespace TradeControl.Configurations
{
    public static class TelemetryDependency
    {
        public static IServiceCollection AddTelemetry(this IServiceCollection services) 
        {

            services.AddHealthChecks();

            services.AddOpenTelemetry().WithMetrics(
                metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddPrometheusExporter()
                        .AddMeter("TradeControl.Metrics");
                });

            return services;
        }
    }
}
