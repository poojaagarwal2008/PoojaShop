using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using PoojaShop.Core.Models;

namespace PoojaShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory p)
        {
            ProductCategory productCategoryToUpdate = productCategories.FirstOrDefault(x => x.Id == p.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = p;
            }
            else
            {
                throw new Exception("Product Not Found.");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.FirstOrDefault(x => x.Id == Id);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Not Found.");
            }

        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.FirstOrDefault(x => x.Id == Id);
            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Not Found.");
            }
        }
    }
}
