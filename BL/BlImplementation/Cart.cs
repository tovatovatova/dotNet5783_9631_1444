using BlApi;

using Dal;
using DalApi;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        DalApi.IDal dal = new Dal.DalList();
        public BO.Cart AddToCart(BO.Cart currentCart, int ProductID)
        {
            DO.Product product = dal.Product.GetById(ProductID) ;
             BO.OrderItem? itemInCart = currentCart.Items.FirstOrDefault(item => item.ID == ProductID);// check if product exists in cart
            if (itemInCart != null)//product exists
            {
                int x = currentCart?.Items?.ToList().FindIndex(item => item.ID == ProductID) ?? -1;//find the index of the product in order to edit
                if (product.AmountInStock > itemInCart.Amount)//if the amount in stock is enough to add(at least) one item to cart
                    {
                        currentCart.Items.ToList().ElementAt(x).Amount += 1;
                    currentCart.Items.ToList().ElementAt(x).Price = product.Price;
                    currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price;
                    currentCart.TotalPrice += product.Price;//add price of the one product we add
                    }
            }
            else//not exist
            {
                if (product.AmountInStock > 0)//exist in stock
                {//id????????????????????????????????????????????????????????????????fvihbrfgvyuerfyvb
                    itemInCart = new BO.OrderItem()
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
                else throw new Exception("not enough in stock");
            }
            return currentCart;//return cart after changes
        }
        public BO.Cart UpdateProductInCart(BO.Cart currentCart, int newAmount,int productId)
        {
            if (newAmount < 0) throw new Exception("nagative amount is invalid");
            DO.Product product = dal.Product.GetById(productId);
            BO.OrderItem? itemInCart = currentCart.Items.FirstOrDefault(item => item.ID == productId);
            int x = currentCart?.Items?.ToList().FindIndex(item => item.ID == productId) ?? -1;//find the index of the product in order to edit
            if (itemInCart != null)
            {
                int amountDifference = newAmount - itemInCart.Amount;
                if (newAmount == 0)
                {
                    currentCart.Items = currentCart.Items.ToList().Where(prod=> prod.ProductID==productId);
                }
                else if (amountDifference == 0) 
                    return currentCart; 
                else if (amountDifference > 0)//the new amount bigger than the old one
                {
                    if (product.AmountInStock >= newAmount)//if there is enough items in stock
                    {
                        currentCart.Items.ToList().ElementAt(x).Amount = newAmount;
                        currentCart.Items.ToList().ElementAt(x).Price = product.Price;
                        currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price*amountDifference;//add the exstra price after the change
                        currentCart.TotalPrice+=amountDifference*product.Price; //add to the cart total price
                    }
                }
                else if (amountDifference < 0)//if the new amount little than the old one
                {
                    if (newAmount <= product.AmountInStock)
                    {
                        currentCart.Items.ToList().ElementAt(x).Amount = newAmount;
                        currentCart.Items.ToList().ElementAt(x).Price = product.Price;
                        currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price * amountDifference;
                        currentCart.TotalPrice += amountDifference * product.Price; //sub from the cart total price
                        //%^^%%^^^ need to check in stock??????
                    }
                }
                
            }
            return currentCart;
          //  throw new NotImplementedException();
        }
        public void OrderCreate(BO.Cart cart)
        {
            BO.Order order = new BO.Order()
            {
                CustomerAddress = cart.CustomerAddress,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now
            };
            DO.Order orderToAdd = new DO.Order()
            {
                CustomerAddress = cart.CustomerAddress,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now
            };
            order.Id = dal.Order.Add(orderToAdd);//add the id
            foreach (var orderItem in cart.Items)
            {
                DO.OrderItem itemInCart = new DO.OrderItem() {/*orderItemId*/ProductId = orderItem.ProductID, OrderId = orderItem.ID };//ID ???
                dal.OrderItem.Add(itemInCart);//add to order item and put order item id 
                DO.Product product = dal.Product.GetById(itemInCart.ProductId);//return the product of the specific item
                if (product.AmountInStock > itemInCart.Amount)//if there are enough products in stock
                {
                    product.AmountInStock -= itemInCart.Amount;//reduce amount from stock
                }
                else //there's not enough
                {
                    throw new Exception("not enough in stock please update your order");///648$$^&&(^^*)
                }
            }



        }
    }

}
