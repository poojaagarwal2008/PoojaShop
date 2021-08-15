using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using PoojaShop.Core.Models;

//Generic Class
//Initially we took a generic class T but there was nothing to specify that T class has an ID.
//So in the Find and Delete methods we wont be able to use ID. TO solve this problem we need to create
//another base class[BaseEntity] having this ID and inherit all classes from that base class.
namespace PoojaShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> where T: BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            //In order to keep track of which class repository this is
            className = typeof(T).Name;

            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = items.FirstOrDefault(x => x.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(className + " Not Found.");
            }
        }

        public T Find(string Id)
        {
            T t = items.FirstOrDefault(x => x.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + " Not Found.");
            }

        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tToDelete = items.FirstOrDefault(x => x.Id == Id);
            if (tToDelete != null)
            {
                items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(className + " Not Found.");
            }
        }

    }
}
