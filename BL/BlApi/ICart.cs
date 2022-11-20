using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        public Cart AddToCart(Cart currentCart, int id);//client
        public Cart UpdateProductInCart(Cart currentCart, int newAmoount);//client
        public void OrderCreate(Cart cart, string CustomerName, string CustomerEmail, string CustomerAddress);//client -close order

    }
}
