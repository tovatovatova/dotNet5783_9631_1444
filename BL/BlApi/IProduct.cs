
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
   public interface IProduct
    {
        public  IEnumerable<BO.ProsuctForList?> GetProductList();//manager
        public BO.Product GetProductDetails(int id);//manager
        public void AddProduct(BO.Product newProduct);//manager
        public void DeleteProduct(int id);//manager
        public void UpdateProduct(BO.Product product);//manager

      public  IEnumerable<BO.ProductItem?> GetCatalog();//client
      public  BO.ProductItem GetProductByID(Cart cart, int id);//client

        public IEnumerable<BO.ProductItem> GetProductListByCategory(Func<BO.Product?, bool>? filter);






    }
}
