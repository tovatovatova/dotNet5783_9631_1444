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
        

        public BO.Cart AddToCart(BO.Cart currentCart, int ProductID)
        {
           
            throw new NotImplementedException();
        }

        public void OrderCreate(BO.Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress)
        {
            throw new NotImplementedException();
        }

        public BO.Cart UpdateProductInCart(BO.Cart currentCart, int newAmount)
        {
            throw new NotImplementedException();
        }
    }
}
