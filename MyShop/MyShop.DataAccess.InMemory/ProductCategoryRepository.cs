using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core;
using MyShop.Core.Models;
using System.Net.Http.Headers;
using System.Reflection;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategorys;

        public ProductCategoryRepository()
        {
            productCategorys = cache["ProductCategory"] as List<ProductCategory>;
            if (productCategorys == null)
            {
                productCategorys = new List<ProductCategory>();
            }

        }

        public void Commit()
        {
            cache["ProductCategory"] = productCategorys;
        }

        public void Insert(ProductCategory p)
        {
            productCategorys.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = productCategorys.Find(p => p.Id == productCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Category not found");
            }
        }

        public ProductCategory Find(String Id)
        {
            ProductCategory productCategory = productCategorys.Find(p => p.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Category not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategorys.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productCategorys.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                productCategorys.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Category not found");
            }
        }
    }
}
