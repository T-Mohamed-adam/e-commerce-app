using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.Coupon
{
    public class CouponResponse
    {
        public int Id { get; set; }
        public string? CouponCode { get; set; }
        public int Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
