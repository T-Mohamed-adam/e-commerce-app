using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Customer
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
