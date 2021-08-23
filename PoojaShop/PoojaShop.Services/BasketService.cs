using PoojaShop.Core.Contracts;
using PoojaShop.Core.Models;
using PoojaShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PoojaShop.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";
        public BasketService(IRepository<Product> ProductContext, IRepository<Basket> BasketContext)
        {
            this.productContext = ProductContext;
            this.basketContext = BasketContext;

        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketID = cookie.Value;
                if(!string.IsNullOrEmpty(basketID))
                {
                    basket = basketContext.Find(basketID);
                }
                else
                {
                    if(createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }

                }

            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }

            }
            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddToBasket(HttpContextBase httpContext, string productID)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(b => b.ProductID == productID);
            if(item==null)
            {
                item = new BasketItem
                {
                    BasketID = basket.Id,
                    ProductID = productID,
                    Quantity = 1
                };

                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity += 1;

            }

            basketContext.commit();
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(b => b.Id == itemId);
            if(item!=null)
            {
                basket.BasketItems.Remove(item);
                basketContext.commit();
            }
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            if (basket != null)
            {
                var result = (from b in basket.BasketItems
                              join p in productContext.Collection() on b.ProductID equals p.Id
                              select new BasketItemViewModel
                              {
                                  Id = b.Id,
                                  Quantity = b.Quantity,
                                  ProductName = p.Name,
                                  Price = p.Price,
                                  Image = p.Image,
                              }).ToList();
                return result;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }

        public BasketSummaryViewModel BasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0,0);

            if(basket!=null)
            {
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quantity).Sum();

                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in productContext.Collection() on item.ProductID equals p.Id
                                        select p.Price).Sum();

                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero;
                return model;
            }
            else
            {
                return model;
            }

        }
    }
}
