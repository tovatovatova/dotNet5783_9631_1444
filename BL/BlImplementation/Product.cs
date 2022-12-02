using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;


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
            if (newProduct.ID <= 0)//negative id
                throw new BO.BlInvalidInputException("product ID");
            if(newProduct.InStock < 0)//negative amount
                throw new BO.BlInvalidInputException("product amount");
            if (newProduct.Price < 0)//negative price
                throw new BO.BlInvalidInputException("product price");
            if (newProduct.Name == "")//empty string
                throw new BO.BlInvalidInputException("product name");
            try
            {

               DO.Product p= new DO.Product
                {
                    Id = newProduct.ID,
                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    AmountInStock = newProduct.InStock,
                    ProductCategoty = (DO.Category)newProduct.Category
                };
                dal.Product.Add(p);
            }
            catch (DO.DalIdAlreadyExistException ex)
            {
                throw new BO.BlIdAlreadyExistException("add product faild",ex);
            }
           
        }

        public void DeleteProduct(int id)
        {
            if (id <= 0)//negative id
                throw new BO.BlInvalidInputException("product ID");
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
                catch (DO.DalIdDoNotExistException ex)//there is no product with this id
                {
                    throw new BO.BlIdDoNotExistException("product", ex);
                }
            }
            else//exist-cant delete-throw exeption
                throw new BO.BlNullPropertyException("cant delete, exist in orders");////565#$%&^*&

        }

        public IEnumerable<BO.ProductItem> GetCatalog()
        {
            return from DO.Product? doProduct in dal.Product.GetAll()
                   select new BO.ProductItem
                   {
                       ID = doProduct?.Id ?? throw new NullReferenceException("missing id"),
                       Name = doProduct?.Name ?? throw new NullReferenceException("missing name"),
                       Price = doProduct?.Price ?? throw new NullReferenceException("missing price"),
                       Category = (BO.Category)(doProduct?.ProductCategoty ?? throw new NullReferenceException("missing category")),
                       AmountInCart = doProduct?.AmountInStock ?? throw new NullReferenceException("missing name"),
                       InStock = doProduct?.AmountInStock > 0 
                  };
        }
 
        public BO.ProductItem GetProductByID(BO.Cart cart,int id)//client
        {
            DO.Product product;
            try
            {
                product = dal.Product.GetById(id);
            }
            catch(DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("product",ex);
            }
            int amount = 0;

            if (cart.Items != null)
            {
                BO.OrderItem itemIn = cart.Items?.FirstOrDefault(item => item.ProductID == id);//if items in cart null error in runtime!!!!

                if (itemIn != null)
                    amount = itemIn.Amount;
                
            }
            BO.ProductItem item = new BO.ProductItem()
            {
                ID = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category)product.ProductCategoty,
                AmountInCart = amount,//////what to do if the product is not in cart\ if the items in cart is null 
                InStock = product.AmountInStock > 0
            };
            return item;
           
        }

        public BO.Product GetProductDetails(int id)
        {
            DO.Product product;
            try
            {
                product = dal.Product.GetById(id);
            }
            catch(DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("product", ex);
            }
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
                Category = (BO.Category?)item?.ProductCategoty ?? throw new BO.BlWrongCategoryException("worng category"),
                Price = item?.Price ?? 0
            });
        }

        public void UpdateProduct(BO.Product product)
        {
            if (product.ID <= 0)//negative id
                throw new BO.BlInvalidInputException("product ID");
            if (product.InStock < 0)//negative amount
                throw new BO.BlInvalidInputException("product amount");
            if (product.Price < 0)//negative price
                throw new BO.BlInvalidInputException("product price");
            if (product.Name == "")//empty string
                throw new BO.BlInvalidInputException("product name");
            try
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
            catch(DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("product", ex);

            }

        }
    }
}
