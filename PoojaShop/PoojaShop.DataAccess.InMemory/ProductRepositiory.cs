using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using PoojaShop.Core.Models;

namespace PoojaShop.DataAccess.InMemory
{
    public class ProductRepositiory
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>;

        public ProductRepositiory()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product p)
        {
            Product productToUpdate = products.FirstOrDefault(x => x.Id == p.Id);
            if (productToUpdate != null)
            {
                productToUpdate = p;
            }
            else
            {
                throw new Exception("Product Not Found.");
            }
        }

        public Product Find(string Id)
        {
            Product product = products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found.");
            }

        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productToDelete = products.FirstOrDefault(x => x.Id == Id);
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found.");
            }
        }
    }
}
