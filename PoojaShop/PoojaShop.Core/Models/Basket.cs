using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoojaShop.Core.Models
{
    public class Basket : BaseEntity
    {
        //lazy loading (asynchronous loading) - initialize the products only when actually needed
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
    }
}
