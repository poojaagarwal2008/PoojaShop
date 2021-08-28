using PoojaShop.Core.Models;
using System.Data.Entity;

namespace PoojaShop.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        //To call the DbContext class constructor using the Default Connection string from web.config
        public DataContext()
            : base("DefaultConnection")
        {

        }

        //In order to create database tables for the models
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
