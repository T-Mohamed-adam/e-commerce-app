using TagerProject.ServiceContracts;
using Microsoft.Extensions.Logging;

namespace TagerProject.BackgroundServices
{
    public class SubscriptionHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SubscriptionHostedService> _logger;

        public SubscriptionHostedService(IServiceProvider serviceProvider, ILogger<SubscriptionHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // To make sure the background service is handled properly during shutdown
            stoppingToken.Register(() =>
            {
                _logger.LogInformation("Background service is stopping...");
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var subscriptionService = scope.ServiceProvider.GetRequiredService<ISubscriptionService>();
                        await subscriptionService.DeactivateExpiredSubscriptions();
                    }
                }
                catch (Exception ex)
                {
                    // Log the error and continue trying on the next iteration
                    _logger.LogError(ex, "An error occurred while deactivating expired subscriptions.");
                }

                // Delay between iterations with a cancellation token to stop gracefully
                await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
            }

            _logger.LogInformation("Background service has stopped.");
        }
    }
}
