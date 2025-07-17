using System.Diagnostics.Metrics;

namespace TradeControl.Services
{
    public class TradeMetrics
    {
        private static readonly Meter Meter = new("TradeControl.Metrics", "1.0.0");

        public static readonly Counter<int> B3TotalApiCalls =
            Meter.CreateCounter<int>("b3_api_total_calls", description: "Number of times the B3 API is called");

        public static readonly Histogram<double> B3ApiAvgCallDuration =
            Meter.CreateHistogram<double>("b3_api_avg_duration", description: "the average response time of the B3 API ");
    }
}
