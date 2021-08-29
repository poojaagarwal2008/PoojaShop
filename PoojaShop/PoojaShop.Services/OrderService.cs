using PoojaShop.Core.Contracts;
using PoojaShop.Core.Models;
using PoojaShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoojaShop.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> orderContext;
        public OrderService(IRepository<Order> OrderContext)
        {
            this.orderContext = OrderContext;

        }
        public void CreateOrder(Order order, List<BasketItemViewModel> basketItems)
        {
            foreach(var item in basketItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductID = item.Id,
                    ProductName = item.ProductName,
                    Image = item.Image,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }
            orderContext.Insert(order);
            orderContext.commit();
        }

        public List<Order> GetOrders()
        {
            return orderContext.Collection().ToList();
        }

        public Order GetOrder(string Id)
        {
            return orderContext.Find(Id);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            orderContext.Update(updatedOrder);
            orderContext.commit();
        }
    }
}
