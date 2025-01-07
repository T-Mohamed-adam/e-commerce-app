namespace TagerProject.Models.Dtos.Discount
{
    public class DiscountUpdateRequest
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public int Value { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
