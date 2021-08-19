using PoojaShop.Core.Models;
using System.Collections.Generic;

namespace PoojaShop.Core.ViewModels
{
    //We may have used the same model as ProductManagerViewModel but sometimes Product List and Product Manager
    //may differ as in the things we want to show in each so it is a good practice to create separate View Models
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
