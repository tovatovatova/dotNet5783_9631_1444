using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;


namespace BlImplementation
{
    internal class Product : IProduct
    {
        DalApi.IDal dal = new Dal.DalList();


        /// <summary>
        /// the function gets product to add to the products list (just if its id not exists) 
        /// </summary>
        /// <param name="newProduct"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddProduct(BO.Product newProduct)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductItem> GetCatalog()
        {
           
            throw new NotImplementedException();
        }

        public ProductItem GetProductByID(int id)
        {
            
            throw new NotImplementedException();
        }

        public BO.Product GetProductDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.ProsuctForList?> GetProductList()
        {
            return from DO.Product? doProduct in dal.Product.GetAll()
                   select new BO.ProsuctForList
                   {
                       ID = doProduct?.Id ?? throw new NullReferenceException("missing id"),
                       Name = doProduct?.Name ?? throw new NullReferenceException("mising name"),
                       Category = (BO.Category?)doProduct?.ProductCategoty ?? throw new NullReferenceException("mis category"),
                       Price = doProduct?.Price ?? 0
                   };
            //throw new NotImplementedException();
        }

        public void UpdateProduct(BO.Product product)
        {
            throw new NotImplementedException();
        }
    }
}
