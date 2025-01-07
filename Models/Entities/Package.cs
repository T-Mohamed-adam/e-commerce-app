using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TagerProject.Models.Entities
{
    public class Package
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? NameAr { get; set; }
        [Required]
        public string? NameEn { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public virtual ICollection<Package>? Packages { get; set; }
    }
}
