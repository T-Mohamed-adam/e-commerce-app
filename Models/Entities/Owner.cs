using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagerProject.Models.Entities
{
    public class Owner : IUser
    {
        [Key]
        public  int  Id { get; set; }
        public int CityId { get; set; }
        public int SubscriptionId { get; set; }
        [Required]
        [StringLength(20)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string? LastName { get; set; }
        [Required]
        [StringLength(10)]
        public string? PhoneNumber { get; set; }
        [StringLength(40)]
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? BusinessName { get; set; }
        public string? Password { get; set; }
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string? ConfirmPassword { get; set; }
        public string? TIN { get; set; }
        public string? CommercialRegister { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("CityId")]
        public virtual City? City { get; set; }
        [ForeignKey("SubscriptionId")]
        public virtual Subscription? Subscription { get; set; }
    }
}
