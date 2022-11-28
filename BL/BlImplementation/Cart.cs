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
            
            DO.Product product = dal.Product.GetById(ProductID) ;
            
            BO.OrderItem? itemInCart = currentCart.Items?.FirstOrDefault(item => item.ID == ProductID);
            if (itemInCart != null)
            {
                int x = currentCart?.Items?.ToList().FindIndex(item => item.ID == ProductID) ?? -1;
              
                    if (product.AmountInStock > itemInCart.Amount)
                    {
                        currentCart.Items.ToList().ElementAt(x).Amount += 1;
                        currentCart.Items.ToList().ElementAt(x).TotalPrice += product.Price;
                        currentCart.TotalPrice += product.Price;
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
                    if (currentCart.Items == null)
                    {
                      
                       // List<OrderItem> lst = new List<BOrderItem>();
                       // lst.Add(itemInCart);
                        //currentCart.Items = lst;
                    }
                    // currentCart.Items.ToList().Add(itemInCart);
                    //foreach(var item in currentCart.Items)
                    //{
                    //    Console.WriteLine(item);
                    //}

                }
            }
            return currentCart;//return cart after changes
            
        }

        public void OrderCreate(BO.Cart cart, string customerName, string customerEmail, string customerAddress)
        {
            BO.Order order = new BO.Order() { CustomerAddress = customerAddress,
            CustomerName=customerName,CustomerEmail=customerEmail,OrderDate=DateTime.Now};
            DO.Order orderToAdd=new DO.Order() {
                CustomerAddress = customerAddress,
                CustomerName = customerName,
                CustomerEmail = customerEmail,
                OrderDate = DateTime.Now
            };
            order.Id=dal.Order.Add(orderToAdd);//add the id
            foreach (var orderItem in cart.Items)
            {
                DO.OrderItem itemInCart = new DO.OrderItem() {/*orderItemId*/ProductId = orderItem.ProductID, OrderId = orderItem.ID };//ID ???
                dal.OrderItem.Add(itemInCart);//add to order item and put order item id 
                /////////
            }


            throw new NotImplementedException();
        }

        public BO.Cart UpdateProductInCart(BO.Cart currentCart, int newAmount,int productId)
        {
            if (newAmount < 0) throw new Exception(" cant be nagative amount");
            DO.Product product = dal.Product.GetById(productId);
            BO.OrderItem? itemInCart = currentCart.Items?.FirstOrDefault(item => item.ID == productId);
            if (itemInCart != null)
            {
                int amountDifference = newAmount - itemInCart.Amount;
                if (newAmount == 0)
                {
                    currentCart.Items.ToList().Remove(itemInCart);
                }
                else if (amountDifference == 0) 
                    return currentCart; 
                else if (amountDifference > 0)//the new amount bigger than the old one
                {
                    if (product.AmountInStock >= newAmount)//if there is enough items in stock
                    {
                        itemInCart.Amount = newAmount;
                        itemInCart.TotalPrice += product.Price * amountDifference;
                        currentCart.TotalPrice += product.Price * amountDifference;
                    }
                }
                else if (amountDifference < 0)//if the new amount little than the old one
                {
                    if (newAmount - product.AmountInStock > 0)
                    {
                        itemInCart.Amount = newAmount;
                        itemInCart.TotalPrice += product.Price * (newAmount - itemInCart.Amount);
                        currentCart.TotalPrice += product.Price * (newAmount - itemInCart.Amount);
                        //%^^%%^^^ need to check in stock??????
                    }
                }
                
            }
            return currentCart;
          //  throw new NotImplementedException();
        }
    }
}
