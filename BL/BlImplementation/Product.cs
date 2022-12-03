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
        /// <exception cref="BO.BlInvalidInputException">throw if enteres false input</exception>
        /// <exception cref="BO.BlIdAlreadyExistException">throw if product already exist</exception>
        public void AddProduct(BO.Product newProduct)
        {
            //input validation
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
                //create product of DO 
               DO.Product p= new DO.Product
                {
                    Id = newProduct.ID,
                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    AmountInStock = newProduct.InStock,
                    ProductCategoty = (DO.Category)newProduct.Category
                };
                dal.Product.Add(p);//add to list of product
            }
            catch (DO.DalIdAlreadyExistException ex)//product already exist
            {
                throw new BO.BlIdAlreadyExistException("add product faild",ex);
            }
           
        }
        /// <summary>
        /// deletes product by the given id
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="BO.BlInvalidInputException">throw if worng input</exception>
        /// <exception cref="BO.BlIdDoNotExistException">throw if product doesnt exist</exception>
        /// <exception cref="BO.BlNullPropertyException">throw if cant delte becouse roduct exist in orders</exception>
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
                    dal.Product.Delete(id);//deletes product 
                }
                catch (DO.DalIdDoNotExistException ex)//there is no product with this id
                {
                    throw new BO.BlIdDoNotExistException("product", ex);
                }
            }
            else//exist-cant delete-throw exeption
                throw new BO.BlNullPropertyException("cant delete, exist in orders");////565#$%&^*&

        }
        
        /// <summary>
        /// return product item in cart by the given id
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <returns>item</returns>
        /// <exception cref="BO.BlIdDoNotExistException">throw if product doesnt exist</exception>
        public BO.ProductItem GetProductByID(BO.Cart cart,int id)//client
        {
            DO.Product product;
            try
            {
                product = dal.Product.GetById(id);//return product
            }
            catch(DO.DalIdDoNotExistException ex)//product doesnt exist
            {
                throw new BO.BlIdDoNotExistException("product",ex);
            }
            int amount = 0;

            if (cart.Items != null)//there are items in cart
            {
                BO.OrderItem itemIn = cart.Items?.FirstOrDefault(item => item.ProductID == id);//if items in cart null error in runtime!!!!

                if (itemIn != null)
                    amount = itemIn.Amount;
                
            }
            BO.ProductItem item = new BO.ProductItem()
            {//creates product item
                ID = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category)product.ProductCategoty,
                AmountInCart = amount,//////what to do if the product is not in cart\ if the items in cart is null 
                InStock = product.AmountInStock > 0
            };
            return item;
           
        }
        /// <summary>
        /// return product details of the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BO.product</returns>
        /// <exception cref="BO.BlIdDoNotExistException">throw if product doesnt exist</exception>
        public BO.Product GetProductDetails(int id)
        {
            DO.Product product;
            try
            {
                product = dal.Product.GetById(id);//return product
            }
            catch(DO.DalIdDoNotExistException ex)//doesnt exist
            {
                throw new BO.BlIdDoNotExistException("product", ex);
            }
            return new BO.Product()
            {//creates product of BO and return
                ID = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category)product.ProductCategoty,
                InStock = product.AmountInStock
            };
        }
        /// <summary>
        /// build an ienumerable of product for list and return
        /// </summary>
        /// <returns>IEnumerable<BO.ProsuctForList?></returns>
        /// <exception cref="BO.BlNullPropertyException">throw if cant convert to product for list</exception>
        /// <exception cref="BO.BlWrongCategoryException">throw i=if cant convert category</exception>
        public IEnumerable<BO.ProsuctForList?> GetProductList()
        {
            return dal.Product.GetAll().Select(item => new BO.ProsuctForList//runs on all the product and for each one of them create a product for list and return
            {
                ID = item?.Id ?? throw new BO.BlNullPropertyException("missing id"),
                Name = item?.Name ?? throw new BO.BlNullPropertyException("mising name"),
                Category = (BO.Category?)item?.ProductCategoty ?? throw new BO.BlWrongCategoryException("worng category"),
                Price = item?.Price ?? 0
            });
        }
        /// <summary>
        /// update product
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BO.BlInvalidInputException">throw if worng input</exception>
        /// <exception cref="BO.BlIdDoNotExistException">thorw if product doesnt exist</exception>
        public void UpdateProduct(BO.Product product)
        {
            //input validation
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
                dal.Product.Update(new DO.Product()//send to update in DO
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    AmountInStock = product.InStock,
                    ProductCategoty = (DO.Category)(BO.Category)product.Category
                });
            }
            catch(DO.DalIdDoNotExistException ex)//prduct doesnt exist
            {
                throw new BO.BlIdDoNotExistException("product", ex);

            }

        }
        // <summary>
        /// runs on all the products, creates ienumerable of product item and return
        /// </summary>
        /// <returns>IEnumerable<BO.ProductItem></returns>
        /// <exception cref="NullReferenceException">throw if couldnt convert</exception>
        public IEnumerable<BO.ProductItem> GetCatalog()
        {
            return from DO.Product? doProduct in dal.Product.GetAll()
                   select new BO.ProductItem
                   {
                       ID = doProduct?.Id ?? throw new NullReferenceException("missing id"),
                       Name = doProduct?.Name ?? throw new NullReferenceException("missing name"),
                       Price = doProduct?.Price ?? throw new NullReferenceException("missing price"),
                       Category = (BO.Category)(doProduct?.ProductCategoty ?? throw new NullReferenceException("missing category")),
                       AmountInCart = 0,//we cant know here the amount in cart
                       InStock = doProduct?.AmountInStock > 0
                   };
        }
    }
}
