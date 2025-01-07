using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TagerProject.Models.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? NameAr { get; set; }
        [Required]
        public string? NameEn { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
        [JsonIgnore]
        public virtual ICollection<Owner>? Owners { get; set; }

        [JsonIgnore]
        public virtual ICollection<Employee>? Employees { get; set; }

        [JsonIgnore]

        public virtual ICollection<Customer>? Customers { get; set; }

        [JsonIgnore]
        public virtual ICollection<Supplier>? Suppliers { get; set; }

    }
}
