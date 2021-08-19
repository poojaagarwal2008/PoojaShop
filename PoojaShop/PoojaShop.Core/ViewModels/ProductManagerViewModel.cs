using PoojaShop.Core.Models;
using System.Collections.Generic;

namespace PoojaShop.Core.ViewModels
{
    //Diff between View and Vuew Model is that in View Model, the data is not saved. It is purely used
    //to transport data between the controller and view.
    public class ProductManagerViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
