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
                if (product.AmountInStock>0)//or amountisstock>currentcart amount 
                {
                    itemInCart.Amount += 1;
                    itemInCart.TotalPrice = product.Price * itemInCart.Amount;
                    currentCart.TotalPrice += product.Price;
                }
            }
            else//not exist
            {
                if(product.AmountInStock>0)//exist in stock
                {
                    BO.OrderItem? orderItemToAdd=new BO.OrderItem() { ID=-1,Name=product.Name,ProductID=product.Id,
                    Price=product.Price,Amount=1,TotalPrice=product.Price};
                    DO
                    dal.OrderItem.Add(orderItemToAdd);
                    //currentCart.Items.ToList().Add();
                }
            }
            throw new NotImplementedException();
        }

        public void OrderCreate(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
        {
            throw new NotImplementedException();
        }

        public BO.Cart UpdateProductInCart(BO.Cart currentCart, int newAmount,int productId)
        {
            throw new NotImplementedException();
        }
    }
}
