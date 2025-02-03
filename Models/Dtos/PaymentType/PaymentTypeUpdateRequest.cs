using System.ComponentModel.DataAnnotations;

namespace TagerProject.Models.Dtos.PaymentType
{
    public class PaymentTypeUpdateRequest
    {
        [Required]
        [Display(Name = "Arabic Name")]
        public string? NameAr { get; set; }
        [Required]
        [Display(Name = "English Name")]
        public string? NameEn { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
