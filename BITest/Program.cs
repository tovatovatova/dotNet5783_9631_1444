using BO;
using System.Security.Cryptography.X509Certificates;
using BlApi;
using BlImplementation;


namespace BITest
{
    public enum Options { PRODUCT = 1, ORDER, CART, EXIT };
    public enum ProductActions { Product_List = 1, ProductDetails, Add, Delete, Update, Catalog, Product_in_Catalog, Exit }
    public enum CartActions { Add = 1, Update, Create,Exit }
    public enum OrderActions { Get_Order = 1, Order_List, Update_Ship, Update_Delivery, Order_Tracking, Update_Order, Exit }
    internal class Program
    {
        static IBl bl = new Bl();
        public static void ProductOptions()
        {
            ProductActions choice;
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
                        foreach (var item in bl.Product.GetProductList())
                            Console.WriteLine(item); 
                        break;
                    case ProductActions.ProductDetails:
                        int id;
                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        Console.WriteLine(bl.Product.GetProductDetails(id)); 
                        break;
                    case ProductActions.Add:
                        Product addProduct = new Product();
                        double price;
                        int category;
                        int stock;
                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        addProduct.ID= id;
                        Console.WriteLine("enter name of product:");
                        addProduct.Name=Console.ReadLine();
                        Console.WriteLine("enter price of product:");
                        if (!double.TryParse(Console.ReadLine(), out price)) throw new Exception("wrong input type");
                        addProduct.Price= price;
                        Console.WriteLine("enter category of product:");
                        if (!int.TryParse(Console.ReadLine(), out category)) throw new Exception("wrong input type");
                        addProduct.Category = (Category)(category);
                        Console.WriteLine("enter amount in stock of product:");
                        if (!int.TryParse(Console.ReadLine(), out stock)) throw new Exception("wrong input type");
                        addProduct.InStock = stock;
                        bl.Product.AddProduct(addProduct);
                        break;
                    case ProductActions.Delete:
                        Console.WriteLine("enter id to delete product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        bl.Product.DeleteProduct(id);
                        break;
                    case ProductActions.Update:
                        Product updateProduct = new Product();
                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        updateProduct.ID = id;
                        Console.WriteLine("enter name of product:");
                        updateProduct.Name = Console.ReadLine();
                        Console.WriteLine("enter price of product:");
                        if (!double.TryParse(Console.ReadLine(), out price)) throw new Exception("wrong input type");
                        updateProduct.Price = price;
                        Console.WriteLine("enter category of product:");
                        if (!int.TryParse(Console.ReadLine(), out category)) throw new Exception("wrong input type");
                        updateProduct.Category = (Category)(category);
                        Console.WriteLine("enter amount in stock of product:");
                        if (!int.TryParse(Console.ReadLine(), out stock)) throw new Exception("wrong input type");
                        updateProduct.InStock = stock;
                        bl.Product.UpdateProduct(updateProduct);
                        break;
                    case ProductActions.Catalog:
                        foreach (var item in bl.Product.GetCatalog())
                            Console.WriteLine(item);
                        break;
                    case ProductActions.Product_in_Catalog:
                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        Console.WriteLine(bl.Product.GetProductByID(id));
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
            int id;
            Console.WriteLine(@"Choose one of the following options:
1: oder details
2:list of orders
3:update ship date 
4:update delivery date
5:order tracking
6:update order");
            OrderActions choi=OrderActions.Exit;

            do
            {
                try
                {
                    if (!OrderActions.TryParse(Console.ReadLine(), out choi)) throw new Exception("wrong input type");
                    switch (choi)
                    {
                        case OrderActions.Get_Order:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.GetOrderByID(id));
                            break;
                        case OrderActions.Order_List:
                            Console.WriteLine(String.Join("", bl.Order.GetOrderList()));
                            break;
                        case OrderActions.Update_Ship:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.UpdateShip(id));
                            break;
                        case OrderActions.Update_Delivery:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.UpdateDelivery(id));
                            break;
                        case OrderActions.Order_Tracking:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.OrderTracking(id));
                            break;
                        case OrderActions.Update_Order:

                            break;
                        case OrderActions.Exit:
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (choi != OrderActions.Exit);
            
        }
        public static void CartOptions()
        {
            CartActions choice;
            Console.WriteLine(@"Choose one of the following options:
1:enter id of product to add to cart:
2:enter amount of products to add to cart:
3:create a new order:");
            if (!CartActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
            while (choice != CartActions.Exit)
            {
                switch (choice)
                {
                    case CartActions.Add:

                        break;
                    case CartActions.Update:

                        break;
                    case CartActions.Create:

                        break;
                    case CartActions.Exit:
                        break;
                    default:
                        break;
                }
                Console.WriteLine(@"Choose one of the following options:
1:enter id of product to add to cart:
2:enter amount of products to add to cart:
3:create a new order:");
                if (!CartActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");


            }

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