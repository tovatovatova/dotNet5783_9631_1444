
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        
        public BO.Cart AddToCart(BO.Cart currentCart, int id);//client
        public BO.Cart UpdateProductInCart(BO.Cart currentCart, int newAmoount);//client
        public void OrderCreate(BO.Cart currentCart, string CustomerName, string CustomerEmail, string CustomerAddress);//client -close order

    }
}
