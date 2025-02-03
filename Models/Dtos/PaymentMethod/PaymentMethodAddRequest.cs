namespace TagerProject.Models.Dtos.PaymentMethod
{
    public class PaymentMethodAddRequest
    {
        public int PaymentTypeId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
    }
}
