using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Entities
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? CouponCode { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
