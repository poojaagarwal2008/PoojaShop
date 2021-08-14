using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoojaShop.Core.Models;

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
