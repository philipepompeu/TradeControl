
namespace TradeControl.Services
{
    public class ConsolidatePositionService : BackgroundService
    {
        private readonly ILogger<ConsolidatePositionService> _logger;

        public ConsolidatePositionService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConsolidatePositionService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("⏳ PriceMonitorService iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("🔍 Verificando preços às {Time}", DateTimeOffset.Now);
                
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            _logger.LogInformation("🛑 PriceMonitorService finalizado.");

        }
    }
}
