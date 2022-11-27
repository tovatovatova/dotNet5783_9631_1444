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
            DO.Product product = dal.Product.GetById(ProductID);//74963987$R^&^&
            BO.OrderItem? itemInCart = currentCart.Items?.FirstOrDefault(item => item.ID == ProductID);
            if (itemInCart!=null)
            {
                if(product.AmountInStock>0)//or amountisstock>currentcart amount 
                {
                    itemInCart.Amount += 1;
                    itemInCart.TotalPrice = product.Price * itemInCart.Amount;
                    currentCart.TotalPrice += product.Price;
                }

            }
            //if exist in cart
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
