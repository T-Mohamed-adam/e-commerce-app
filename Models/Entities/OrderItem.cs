using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TagerProject.Models.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }
}
