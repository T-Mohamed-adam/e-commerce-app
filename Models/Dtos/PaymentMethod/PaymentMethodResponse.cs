using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.PaymentMethod
{
    public class PaymentMethodResponse
    {
        public int Id { get; set; }
        public int PaymentTypeId { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? MembershipNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
