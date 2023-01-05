using BlApi;
using DalApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        DalApi.IDal dal = DalApi.Factory.Get();
        /// <summary>
        /// gets a cart and id of product-add to cart(checks if its possible...)
        /// </summary>
        /// <param name="currentCart"></param>
        /// <param name="ProductID"></param>
        /// <returns>current cart</returns>
        /// <exception cref="BO.BlIdDoNotExistException">throw if product doesnt exist</exception>
        /// <exception cref="BO.BlNullPropertyException">throw if cant add to cart-there is not enough in stock</exception>
        public BO.Cart AddToCart(BO.Cart currentCart, int ProductID)
        {
            if(currentCart.Items==null)
                currentCart.Items=new List<BO.OrderItem>();
            
            DO.Product product;
            try
            {
                 product= dal.Product.GetById(ProductID);//return product from dal with the given id
            }
            catch (DO.DalIdDoNotExistException ex)//product doesnt exist
            {
                throw new BO.BlIdDoNotExistException("product in cart", ex);
            }

            BO.OrderItem? itemInCart = currentCart.Items.FirstOrDefault(item => item.ID == ProductID);// check if product exists in cart-if so, return the first object
            if (itemInCart != null)//product exists
            {
                int x = currentCart?.Items?.ToList().FindIndex(item => item.ID == ProductID) ?? -1;//find the index of the product in order to edit
                //how to do this without -1??????
                if (product.AmountInStock > itemInCart.Amount)//if the amount in stock is enough to add(at least) one item to cart
                {//update order item details
                    currentCart.Items.ToList().ElementAt(x).Amount += 1;
                    currentCart.Items.ToList().ElementAt(x).Price = product.Price;
                    currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price;
                    currentCart.TotalPrice += product.Price;//add price of the one product we add
                }
            }
            else//product not exist
            {
                if (product.AmountInStock > 0)//exist in stock
                {//id????????????????????????????????????????????????????????????????fvihbrfgvyuerfyvb
                    itemInCart = new BO.OrderItem()//create item in cart
                    {
                        ID = ProductID,
                        Name = product.Name,
                        ProductID = product.Id,
                        Price = product.Price,
                        Amount = 1,
                        TotalPrice = product.Price
                    };
                    currentCart.TotalPrice += itemInCart.Price;//add price of item to total price
                    currentCart.Items = currentCart.Items.Append(itemInCart);
                }
                else//there is not enough from the needed product 
                    throw new BO.BlOutOfStockException("out of stock");
            }
            return currentCart;//return cart after changes
        }
        /// <summary>
        /// gets a cart,productr and a new amount to add to cart from this product. check if the product exist add has enough in stock...
        /// </summary>
        /// <param name="currentCart"></param>
        /// <param name="newAmount"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <exception cref="BO.BlInvalidInputException">throw if product id is negative</exception>
        /// <exception cref="BO.BlIdDoNotExistException">throw if product doesnt exist</exception>
        /// <exception cref="BO.BlNullPropertyException">throw if there is not eough to add to cart in stock</exception>
        public BO.Cart UpdateProductInCart(BO.Cart currentCart, int newAmount, int productId)
        {
            if (productId < 0)//negagive product id
                throw new BO.BlInvalidInputException(" product id");
            DO.Product product;
            try
            {
                 product = dal.Product.GetById(productId);//return product by the given id

            }
            catch (DO.DalIdDoNotExistException ex)//product doesnt exist
            {

                throw new BO.BlIdDoNotExistException("product in cart", ex);
            }
            BO.OrderItem? itemInCart = currentCart.Items.FirstOrDefault(item => item.ID == productId);//gets the first item that has the same given product id
            int x = currentCart?.Items?.ToList().FindIndex(item => item.ID == productId) ?? -1;//find the index of the product in order to edit
            if (itemInCart != null)//there is item of this product
            {
                int amountDifference = newAmount - itemInCart.Amount;//the new amuont to update
                if (newAmount == 0)//the given new amount
                {
                    currentCart.Items = currentCart.Items.Where(item => item.ProductID != productId);//deletes this item from cart
                    currentCart.TotalPrice += product.Price * amountDifference;//update proce(the amount differnce will be a negative number-will reduce the price)
                }
                else if (amountDifference == 0)//nothing to change
                    return currentCart;
                else //the new amount bigger than the old one or little than it
                {
                    if (product.AmountInStock >= newAmount)//if there is enough items in stock
                    {
                        currentCart.Items.ToList().ElementAt(x).Amount = newAmount;
                        currentCart.Items.ToList().ElementAt(x).Price = product.Price;
                        currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price * amountDifference;//add the exstra price after the change
                        currentCart.TotalPrice += amountDifference * product.Price; //add to the cart total price
                    }
                    else
                        throw new BO.BlOutOfStockException("out of stock");


                }
            }//doesnt exist in cart
            else
            {
                if (newAmount != 0)//there is something to add 
                {
                    if (product.AmountInStock >= newAmount)//there is enough in stock
                    {
                        BO.OrderItem itemCart = new BO.OrderItem()
                        {//create new item in order
                            ID = productId,
                            Name = product.Name,
                            ProductID = product.Id,
                            Price = product.Price,
                            Amount = newAmount,
                            TotalPrice = product.Price * newAmount
                        };
                        currentCart.Items = currentCart.Items.Append(itemCart);//add to end of order item list
                        currentCart.TotalPrice = product.Price * newAmount;    //update total price of cart
                    }
                    else
                    throw new BO.BlOutOfStockException("out of stock");
                }
              
            }
            return currentCart;
        }
        /// <summary>
        /// create an order from a cart
        /// </summary>
        /// <param name="cart"></param>
        /// <exception cref="BO.BlNullPropertyException">throw if cant convert to order-no items in cart</exception>
        /// <exception cref="BO.BlInvalidInputException">throw if entered a falese input</exception>
        public int OrderCreate(BO.Cart cart)
        {
            #region input validation
            if (cart.Items.Count() == 0)//no items in cart
                throw new BO.BlNullPropertyException("no items in cart");
            if (cart.CustomerName == " ")//empty name
                throw new BO.BlInvalidInputException("customer name");
            if (cart.CustomerAddress == " ")//empty address
                throw new BO.BlInvalidInputException("customer address");
            if (cart.CustomerEmail == null||cart.CustomerEmail.Contains("@gmail.com")==false)//empty email or worng email
                throw new BO.BlInvalidInputException("customer email");
            if (cart.Items.All(item => item.Amount < 0))//negative amounts of items
                throw new BO.BlInvalidInputException("items amount");
            if (cart.Items.All/*forEach?*/(item => Check(item.ProductID, item.Amount) == true) == false)//there is not enough in stock from the items in the cart
                throw new BO.BlInvalidInputException("product amount in stock to execute an order");
            #endregion
            //all details are correct
            BO.Order boOrder = new BO.Order()//cearte an order
            {
                CustomerAddress = cart.CustomerAddress,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now,
                Status = BO.OrderStatus.Ordered,
                ShipDate = null,
                DeliveryDate = null,
                TotalPrice = cart.TotalPrice
                //id will be added later...
            };

            //boOrder.Items = cart.Items.Where(item => dal.Product.GetById(item.ProductID).AmountInStock >= item.Amount);
            boOrder.Items = cart.Items.Select(item=>item);//add all items to the the new order(validation was already checked)
            //if (boOrder.Items.Count() != cart.Items.Count())
                //throw new Exception("you need to update your cart!");

            DO.Order newOrder = new DO.Order()//create order in DO
            {
                CustomerName = boOrder.CustomerName,
                CustomerAddress = boOrder.CustomerAddress,
                CustomerEmail = boOrder.CustomerEmail,
                DeliveryDate = boOrder.DeliveryDate,
                ShipDate = boOrder.ShipDate,
                OrderDate = boOrder.OrderDate,
            };
            boOrder.Id = dal.Order.Add(newOrder);//add to order list in DO and gets an order id
            IEnumerable<DO.OrderItem> doOrderItems;
            doOrderItems = from BO.OrderItem itemInOrder in boOrder.Items/*cart.Items*///runs on the items in order                
                           select new DO.OrderItem()//for each order item creates an order item in DO
                           {
                               OrderId = boOrder.Id,
                               ProductId = itemInOrder.ProductID,
                               Price = itemInOrder.Price,
                               Amount = itemInOrder.Amount,
                           };
            doOrderItems.Select(x => dal.OrderItem.Add(x)).ToList();//add all order items to list of order item in DO
            boOrder.Items.ToList().ForEach(item => UpdateAmount(item.ProductID, item.Amount));//update the amount in stock of each product after reservation
            return boOrder.Id;//return id of this order
        }
        /// <summary>
        /// check if there is enough in stock from specific product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        /// <returns>true/false</returns>
        /// <exception cref="BO.BlIdDoNotExistException">throw if product doesnt exist</exception>
        bool Check(int productId, int amount)
        {
            DO.Product prod;
            try
            {
                prod = dal.Product.GetById(productId);//return product by id
            }
            catch (DO.DalIdDoNotExistException ex)
            {

                throw new BO.BlIdDoNotExistException("product in cart",ex);
            }
            if (prod.AmountInStock >= amount)//there is enough in stock
                return true;

            return false;
        }
        /// <summary>
        /// update the new amount of products in cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="amount"></param>
        /// <exception cref="BO.BlIdDoNotExistException">throw if product doesnt exist</exception>
        void UpdateAmount(int productId, int amount)
        {
            DO.Product prod;
            try
            {
                prod = dal.Product.GetById(productId);//return product by id
            }
            catch (DO.DalIdDoNotExistException ex)
            {
                throw new BO.BlIdDoNotExistException("product in cart", ex);
               
            }       
            prod.AmountInStock -= amount;
            dal.Product.Update(prod);
        }
    }
}


