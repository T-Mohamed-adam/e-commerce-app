using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TagerProject.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Category id is required")]
        public int CategoryId { get; set; }
        [Required]
        public int TaxId { get; set; }
        [Required]
        public int UnitId { get; set; }
        [Required]
        public string? NameAr { get; set; }
        [Required]
        public string? NameEn { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal PriceIncludeTax { get; set; }
        [Required]
        public string? MembershipNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        [ForeignKey("BrandId")]
        public virtual Brand? Brand { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [ForeignKey("TaxId")]
        public virtual Tax? Tax { get; set; }

        [ForeignKey("UnitId")]
        public virtual Unit? Unit { get; set; }


        [JsonIgnore]
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        [JsonIgnore]
        public virtual ICollection<PurchaseItem>?  PurchaseItems { get; set; }
        [JsonIgnore]
        public virtual ICollection<Inventory>? Inventories { get; set; }


    }
}
