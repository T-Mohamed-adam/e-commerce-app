using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TagerProject.Models.Entities
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PaymentTypeId { get; set; }  
        [Required]
        public string? NameAr { get; set; }
        [Required]
        public string? NameEn { get; set; }
        [Required]
        public string? MembershipNumber { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("PaymentTypeId")]
        public virtual PaymentType? PaymentType { get; set; } 

        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
