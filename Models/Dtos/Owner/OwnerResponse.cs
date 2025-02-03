
namespace TagerProject.Models.Dtos.Owner
{
    public class OwnerResponse
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int SubscriptionId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? BusinessName { get; set; }
        public string? Password { get; set; }
        public string? TIN { get; set; }
        public string? CommercialRegister { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual TagerProject.Models.Entities.Subscription? Subscription { get; set; }
    }
}
