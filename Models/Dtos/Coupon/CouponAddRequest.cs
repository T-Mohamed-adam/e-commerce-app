namespace TagerProject.Models.Dtos.Coupon
{
    public class CouponAddRequest
    {
        public string? CouponCode { get; set; }
        public int Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
