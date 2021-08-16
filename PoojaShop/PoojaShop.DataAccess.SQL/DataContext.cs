using PoojaShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoojaShop.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        //To call the DbContext class constructor using the Default Connection string from web.config
        public DataContext()
            :base("DefaultConnection")
        {

        }

        //In order to create database tables for the models
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
