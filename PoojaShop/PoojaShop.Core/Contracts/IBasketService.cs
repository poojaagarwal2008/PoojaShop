using PoojaShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PoojaShop.Core.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string productID);
        void RemoveFromBasket(HttpContextBase httpContext, string itemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);
        BasketSummaryViewModel BasketSummary(HttpContextBase httpContext);
    }
}
