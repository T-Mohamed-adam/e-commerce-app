

namespace TagerProject.Models.Dtos.Subscription
{
    public class SubscriptionResponse
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual TagerProject.Models.Entities.Package? Package { get; set; }
    }
    }
