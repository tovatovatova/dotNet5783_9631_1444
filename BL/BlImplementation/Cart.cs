using BlApi;
using Dal;
using DalApi;
using System;
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
            DO.Product product = dal.Product.GetById(ProductID);
            BO.OrderItem? itemInCart = currentCart.Items?.FirstOrDefault(item => item.ID == ProductID);
            if (itemInCart!=null)//if exist in cart

            {
                if (product.AmountInStock>itemInCart.Amount)//or amountisstock>currentcart amount 
                {
                    itemInCart.Amount += 1;
                    itemInCart.TotalPrice += product.Price;
                    currentCart.TotalPrice += product.Price;

                }
            }
            else//not exist
            {
                if(product.AmountInStock>0)//exist in stock
                {//id????????????????????????????????????????????????????????????????fvihbrfgvyuerfyvb
                   itemInCart=new BO.OrderItem() { ID=363636,Name=product.Name,ProductID=product.Id,
                    Price=product.Price,Amount=1,TotalPrice=product.Price};
                    currentCart.TotalPrice += itemInCart.Price;//add price of item to total price
                }
            }
            currentCart.Items?.ToList().Add(itemInCart);
            return currentCart;//return cart after changes
            throw new NotImplementedException();
        }

        public void OrderCreate(BO.Cart cart/*, string customerName, string customerEmail, string customerAddress*/)
        {
            BO.Order order = new BO.Order() { CustomerAddress = cart.CustomerAddress,
            CustomerName=cart.CustomerName,CustomerEmail=cart.CustomerEmail,OrderDate=DateTime.Now};
            DO.Order orderToAdd=new DO.Order() {
                CustomerAddress = cart.CustomerAddress,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now
            };
            order.Id=dal.Order.Add(orderToAdd);//add the id
            foreach (var orderItem in cart.Items)
            {
                DO.OrderItem itemInCart = new DO.OrderItem() {/*orderItemId*/ProductId = orderItem.ProductID, OrderId = orderItem.ID };//ID ???
                dal.OrderItem.Add(itemInCart);//add to order item and put order item id 
                DO.Product product = dal.Product.GetById(itemInCart.ProductId);//return the product of the specific item
                if(product.AmountInStock>itemInCart.Amount)//if there are enough products in stock
                {
                    product.AmountInStock-=itemInCart.Amount;//reduce amount from stock
                }
                else //there's not enough
                { 
                    throw new NotImplementedException();///648$$^&&(^^*)
                }
            }


            throw new NotImplementedException();
        }

        public BO.Cart UpdateProductInCart(BO.Cart currentCart, int newAmount,int productId)
        {
            
            throw new NotImplementedException();
        }
    }
}
