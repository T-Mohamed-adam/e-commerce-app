using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TagerProject.Models.Entities
{
    public class Tax
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? NameAr { get; set; }
        [Required]
        public string? NameEn { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public string? MembershipNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public virtual ICollection<Product>? Products { get; set; }
    }
}
