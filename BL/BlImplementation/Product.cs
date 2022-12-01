using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using BO;


namespace BlImplementation
{
    internal class Product : IProduct
    {//לא לשכוח לעשות תקינות קלט
        DalApi.IDal dal = new Dal.DalList();


        /// <summary>
        /// the function gets product to add to the products list (just if its id not exists) 
        /// </summary>
        /// <param name="newProduct"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddProduct(BO.Product newProduct)
        {
               dal.Product.Add(new DO.Product() { Id=newProduct.ID, Name=newProduct.Name,
               Price=newProduct.Price,AmountInStock=newProduct.InStock,
               ProductCategoty=(DO.Category)newProduct.Category });
        }

        public void DeleteProduct(int id)
        {
            IEnumerable<DO.OrderItem>? idOfOrderItem = from DO.Order order in dal.Order.GetAll()//runs on list of order
                                                       from DO.OrderItem item in dal.OrderItem.GetAll()//runs on list of order item
                                                       where (order.OrderId == item.OrderId)
                                                       where(item.ProductId == id)
                                                       select item;
            //idOfOrderItem has  all the orderItem with the given productId
            if (idOfOrderItem.Count() == 0)// the given productId doesnt exist in orders-we can delete it
            {
                try
                {
                    dal.Product.Delete(id);
                }
                catch (Exception e)//there is no product with this id
                {
                    Console.WriteLine(e.Message);
                }
            }
            else//exist-cant delete-throw exeption
                throw new Exception("cant delete, exist in orders");

        }

        public IEnumerable<BO.ProductItem> GetCatalog()
        {
            return from DO.Product? doProduct in dal.Product.GetAll()
                   select new BO.ProductItem
                   {
                       ID = doProduct?.Id ?? throw new NullReferenceException("missing id"),
                       Name = doProduct?.Name ?? throw new NullReferenceException("mising name"),
                       Price = doProduct?.Price ?? throw new NullReferenceException("missing price"),
                       Category = (BO.Category)(doProduct?.ProductCategoty ?? throw new NullReferenceException("missing category")),
                     AmountInCart = doProduct?.AmountInStock ?? throw new NullReferenceException("mising name"),
                       InStock = doProduct?.AmountInStock > 0 
                  };
        }
 
        public BO.ProductItem GetProductByID(BO.Cart cart,int id)//client
        {
            DO.Product product = dal.Product.GetById(id);
            BO.OrderItem itemIn = cart.Items.FirstOrDefault(item => item.ProductID == id);//if items in cart null error in runtime!!!!
            int amount;
            if (itemIn == null || cart.Items == null)
                amount = 0;
            else
                amount = itemIn.Amount;
                BO.ProductItem item = new BO.ProductItem()
                {
                    ID = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (BO.Category)(product.ProductCategoty),
                  AmountInCart = amount,//////what to do if the product is not in cart\ if the items in cart is null 
                    InStock = product.AmountInStock > 0
                };
                return item;
            //throw new NotImplementedException();
        }

        public BO.Product GetProductDetails(int id)
        {
            DO.Product product = dal.Product.GetById(id);
            return new BO.Product()
            {
                ID = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category)product.ProductCategoty,
                InStock = product.AmountInStock
            };
        }

        public IEnumerable<BO.ProsuctForList?> GetProductList()
        {
            return dal.Product.GetAll().Select(item => new BO.ProsuctForList
            {
                ID = item?.Id ?? throw new NullReferenceException("missing id"),
                Name = item?.Name ?? throw new NullReferenceException("mising name"),
                Category = (BO.Category?)item?.ProductCategoty ?? throw new NullReferenceException("mis category"),
                Price = item?.Price ?? 0
            });
        }

        public void UpdateProduct(BO.Product product)
        {
            dal.Product.Update(new DO.Product()
            {
                Id = product.ID,
                Name = product.Name,
                Price = product.Price,
                AmountInStock = product.InStock,
                ProductCategoty = (DO.Category)(BO.Category)product.Category
            });
         
        }
    }
}
