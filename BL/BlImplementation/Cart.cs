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
                    itemInCart.TotalPrice = product.Price * itemInCart.Amount;
                    currentCart.TotalPrice += product.Price;
                }
            }
            else//not exist
            {
                if(product.AmountInStock>0)//exist in stock
                {//id????????????????????????????????????????????????????????????????fvihbrfgvyuerfyvb
                    BO.OrderItem? orderItemToAdd=new BO.OrderItem() { ID=363636,Name=product.Name,ProductID=product.Id,
                    Price=product.Price,Amount=1,TotalPrice=product.Price};
                    currentCart.Items?.ToList().Add(orderItemToAdd);//add new item to list of items in cart
                    currentCart.TotalPrice += orderItemToAdd.Price;//add price of item to total price
                }
            }
            return currentCart;//return cart after changes
            throw new NotImplementedException();
        }

        public void OrderCreate(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
        {

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
