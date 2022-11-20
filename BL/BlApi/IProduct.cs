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
        public  IEnumerable<Product> GetProductList();//manager
        public Product GetProductDetails(int id);//manager
        public void AddProduct(Product newProduct);//manager
        public void DeleteProduct(int id);//manager
        public void UpdateProduct(Product product);//manager

       public  IEnumerable<ProductItem> GetCatalog();//client
      public  ProductItem GetProductByID(int id);//client
        
       



    }
}
