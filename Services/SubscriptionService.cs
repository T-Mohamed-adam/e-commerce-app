using Microsoft.EntityFrameworkCore;
using TagerProject.Data;
using TagerProject.ServiceContracts;

namespace TagerProject.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SubscriptionService> _logger;
        private readonly IEmailService _emailService;

        public SubscriptionService(ApplicationDbContext dbContext, ILogger<SubscriptionService> logger, IEmailService emailService) 
        {
            _dbContext = dbContext;
            _logger = logger;
            _emailService = emailService;
        }
        public async Task DeactivateExpiredSubscriptions()
        {
            var now = DateTime.UtcNow;

            // Find owner with expired subscriptions
            var expiredSubscriptions = await _dbContext.Subscriptions.Where(s => s.EndDate <= now).ToListAsync();

            foreach (var expiredSubscription in expiredSubscriptions) 
            {
                var owner = await _dbContext.Owners
                    .Where(o => o.SubscriptionId == expiredSubscription.Id && o.IsActive == true)
                    .FirstOrDefaultAsync();

               
                if (owner is not null) 
                {
                     owner.IsActive = false;
                    _logger.LogInformation("The owner account is inactived now");

                    await _emailService.SendEmail(
                        "mo.adam.ksa@gmail.com",
                        "Subscription End",
                        $"Dear {owner!.FirstName} {owner!.LastName}, your subscription has expired. " +
                        $"To avoid any interruption in service, please renew your subscription."
                        );
                }

            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
