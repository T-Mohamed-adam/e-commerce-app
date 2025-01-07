using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public int CityId { get; set; }
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
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("CityId")]
        public virtual City? City { get; set; }
    }
}
