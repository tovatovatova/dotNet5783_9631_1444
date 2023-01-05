
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        
        public BO.Cart AddToCart(BO.Cart currentCart, int ProductID);//client
        public BO.Cart UpdateProductInCart(BO.Cart currentCart, int newAmount,int productId);//client
        public int OrderCreate(BO.Cart currentCart/*, string CustomerName, string CustomerEmail, string CustomerAddress*/);//client -close order

    }
}
