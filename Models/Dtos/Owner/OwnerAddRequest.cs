using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Owner
{
    public class OwnerAddRequest
    {
        public int CityId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? BusinessName { get; set; }
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string? ConfirmPassword { get; set; }
        public string? TIN { get; set; }
        public string? CommercialRegister { get; set; }

        // Subscription data
        public int PackageId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
