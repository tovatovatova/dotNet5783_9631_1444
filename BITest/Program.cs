using BO;
using System.Security.Cryptography.X509Certificates;
using BlApi;
namespace BITest
{
    public enum Options { PRODUCT = 1, ORDER, CART, EXIT };
    public enum ProductActions { Product_List = 1, ProductDetails, Add, Delete, Update, Catalog, Product_in_Catalog, Exit }
    public enum CartActions { Add = 1, Update, Create }
    public enum OrderActions { Get_Order = 1, Order_List, Update_Ship, Update_Delivery, Order_Tracking, Update_Order, Exit }
    
    internal class Program
    {

       
        public static void ProductOptions()
        {
            ProductActions choice;
            IBI bl = new BL();
            Console.WriteLine(@"Choose one of the following options:
1: list of products
2:details of product
3:add product
4:delete product
5:update product
6:catalog
7:delails of product item");
            if (!ProductActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            while (choice != ProductActions.Exit)
            {
                switch (choice)
                {
                    case ProductActions.Product_List:
                        break;
                    case ProductActions.ProductDetails:
                        break;
                    case ProductActions.Add:
                        break;
                    case ProductActions.Delete:
                        break;
                    case ProductActions.Update:
                        break;
                    case ProductActions.Catalog:
                        break;
                    case ProductActions.Product_in_Catalog:
                        break;
                    case ProductActions.Exit:
                        break;
                    default:
                        break;
                }
            }


        }
        public static void OrderOptions()
        {
            Console.WriteLine(@"Choose one of the following options:
1: oder details
2:list of orders
3:update ship date 
4:update delivery date
5:order tracking
6:update order");
            do
            {

                OrderActions choi;
                if (!OrderActions.TryParse(Console.ReadLine(), out choi)) throw new Exception("wrong input type");
                   
            } while (true);
        }
        public static void CartOptions()
        {

        }
        static void Main(string[] args)
        {
            Console.WriteLine(@"Choose one of the following options:
1: Products
2:Orders
3:Carts
0:Exit");
            Options choice;
            if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            while (choice != Options.EXIT)
            {
                switch (choice)
                {
                    case Options.PRODUCT:
                        ProductOptions();
                        break;
                    case Options.ORDER:
                        OrderOptions();
                        break;
                    case Options.CART:
                        CartOptions();
                        break;
                    default:
                        break;
                }
                Console.WriteLine(@"Choose one of the following options:
1: Products
2:Orders
3:Carts
0:Exit");
                if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            }
        }
    }
}