using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoojaShop.Core.Models
{
    public class BasketItem : BaseEntity
    {
        public string BasketID { get; set; }
        public string ProductID { get; set; }
        public string Quantity { get; set; }
    }
}
