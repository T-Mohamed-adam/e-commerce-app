namespace TagerProject.ServiceContracts
{
    public interface ISubscriptionService
    {
        public Task DeactivateExpiredSubscriptions();
    }
}
