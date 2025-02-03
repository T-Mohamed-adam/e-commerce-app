namespace TagerProject.Models.Dtos.PaymentMethod
{
    public class PaymentMethodUpdateRequest
    {
        public int PaymentTypeId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public bool IsActive { get; set; }
    }
}
