using System.ComponentModel.DataAnnotations;

namespace PoojaShop.Core.Models
{
    public class Product : BaseEntity
    {
        [StringLength(20)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 1000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

    }
}
