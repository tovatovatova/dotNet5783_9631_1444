using BO;
using System.Security.Cryptography.X509Certificates;
using BlApi;
using BlImplementation;


namespace BITest
{
    public enum Options { EXIT, PRODUCT, ORDER, CART };
    public enum ProductActions { Exit, Product_List , ProductDetails, Add, Delete, Update, Catalog, Product_in_Catalog  }
    public enum CartActions { Exit, Add , Update, Create }
    public enum OrderActions { Exit, Get_Order , Order_List, Update_Ship, Update_Delivery, Order_Tracking }
    internal class Program
    {
        static IBl bl = BlApi.Factory.Get();

        static Cart newCart=new Cart() { CustomerAddress = "geva 20", CustomerEmail = "Avraham", CustomerName = "Avraham", Items =new List<OrderItem>() , TotalPrice = 0 };
   


        public static void ProductOptions()
        {
           
            ProductActions choice;
            Console.WriteLine(@"Choose one of the following options:
1: list of products
2:details of product
3:add product
4:delete product
5:update product
6:show catalog
7:delails of product from catalog
0:exit");
            try
            {
                if (!ProductActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
                while (choice != ProductActions.Exit)
                {

                    switch (choice)
                    {

                        case ProductActions.Product_List:
                            var lst = bl.Product.GetProductList();
                            foreach (var item in lst)
                                Console.WriteLine(item);
                            break;
                        case ProductActions.ProductDetails:
                            int id;
                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException ("wrong input type");
                            Console.WriteLine(bl.Product.GetProductDetails(id));
                            break;
                        case ProductActions.Add:
                            Product addProduct = new Product();
                            double price;
                           string x;
                            Category category;
                            int stock;

                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException ("wrong input type");
                            addProduct.ID = id;
                            Console.WriteLine("enter name of product:");
                            addProduct.Name = Console.ReadLine();
                            Console.WriteLine("enter price of product:");
                            if (!double.TryParse(Console.ReadLine(), out price)) throw new FormatException ("wrong input type");
                            addProduct.Price = price;
                            Console.WriteLine("enter category of product:");
                            x= Console.ReadLine();
                            if (!Category.TryParse(x, out category)) throw new FormatException ("wrong input type");
                            if ((int)category < 0 || (int)category > 4)
                                throw new ArgumentException("category doenst exist");
                            addProduct.Category = (Category)(category);
                            Console.WriteLine("enter amount in stock of product:");
                            if (!int.TryParse(Console.ReadLine(), out stock)) throw new FormatException ("wrong input type");
                            addProduct.InStock = stock;
                            bl.Product.AddProduct(addProduct);
                            break;
                        case ProductActions.Delete:
                            Console.WriteLine("enter id to delete product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException ("wrong input type");
                            bl.Product.DeleteProduct(id);
                            break;
                        case ProductActions.Update:
                            Product updateProduct = new Product();
                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException ("wrong input type");
                            updateProduct.ID = id;
                            Console.WriteLine("enter name of product:");
                            updateProduct.Name = Console.ReadLine();
                            Console.WriteLine("enter price of product:");
                            if (!double.TryParse(Console.ReadLine(), out price)) throw new FormatException ("wrong input type");
                            updateProduct.Price = price;
                            Console.WriteLine("enter category of product:");
                            if (!Category.TryParse(Console.ReadLine(), out category)) throw new FormatException ("wrong input type");
                            updateProduct.Category = (Category)category;
                            Console.WriteLine("enter amount in stock of product:");
                            if (!int.TryParse(Console.ReadLine(), out stock)) throw new FormatException ("wrong input type");
                            updateProduct.InStock = stock;
                            bl.Product.UpdateProduct(updateProduct);
                            break;
                        case ProductActions.Catalog:
                            foreach (var item in bl.Product.GetCatalog())
                                Console.WriteLine(item);
                            break;
                        case ProductActions.Product_in_Catalog:
                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new FormatException ("wrong input type");
                            Console.WriteLine(bl.Product.GetProductByID(newCart, id));
                            break;
                        case ProductActions.Exit:
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine("");
                    Console.WriteLine(@"Choose one of the following options:
1: list of products
2:details of product
3:add product
4:delete product
5:update product
6:show catalog
7:delails of product item
0:exit");
                    if (!ProductActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
                }
            }
            catch (BO.BlIdAlreadyExistException ex) {Console.WriteLine(ex); }
            catch(BO.BlIdDoNotExistException ex) { Console.WriteLine(ex); }
            catch (BO.BlIncorrectDateException ex) { Console.WriteLine(ex); }
            catch(BO.BlInvalidInputException ex) { Console.WriteLine(ex); }
            catch(BO.BlNullPropertyException ex) { Console.WriteLine(ex); }
            catch(BO.BlWrongCategoryException ex) { Console.WriteLine(ex); }
            catch (FormatException ex) { Console.WriteLine(ex); }
            catch(ArgumentException ex) { Console.WriteLine(ex); }
        }
        public static void OrderOptions()
        {
            int id;

            OrderActions choi=OrderActions.Exit;

            do
            {
                Console.WriteLine(@"Choose one of the following options:
1: order details
2:list of orders
3:update ship date 
4:update delivery date
5:order tracking
0:exit");
                try
                {
                    if (!OrderActions.TryParse(Console.ReadLine(), out choi)) throw new FormatException ("wrong input type");
                    switch (choi)
                    {
                        case OrderActions.Get_Order:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.GetOrderByID(id));
                            break;
                        case OrderActions.Order_List:
                            Console.WriteLine(String.Join(" ", bl.Order.GetOrderList()));
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
                        
                        case OrderActions.Exit:
                            break;
                        default:
                            break;
                    }

                }
                catch (BO.BlIdAlreadyExistException ex) { Console.WriteLine(ex); }
                catch (BO.BlIdDoNotExistException ex) { Console.WriteLine(ex); }
                catch (BO.BlIncorrectDateException ex) { Console.WriteLine(ex); }
                catch (BO.BlInvalidInputException ex) { Console.WriteLine(ex); }
                catch (BO.BlNullPropertyException ex) { Console.WriteLine(ex); }
                catch (BO.BlWrongCategoryException ex) { Console.WriteLine(ex); }
                catch (FormatException ex) { Console.WriteLine(ex); }
                catch (ArgumentException ex) { Console.WriteLine(ex); }

            } while (choi != OrderActions.Exit);
            
        }
        public static void CartOptions()
        {
            CartActions choice;
            Console.WriteLine(@"Choose one of the following options:
1:add product to cart
2:update amount of product in cart
3:create a new order:
0:exit");
            try
            {
                if (!CartActions.TryParse(Console.ReadLine(), out choice)) throw new FormatException ("wrong input type");
                while (choice != CartActions.Exit)
                {
                    switch (choice)
                    {
                        case CartActions.Add:
                            int id, amount;
                           
                            Console.WriteLine("enter id of product to add to cart:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Cart.AddToCart(newCart, id));
                            break;
                        case CartActions.Update:
                            
                            Console.WriteLine("enter id of product to add to cart:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine("enter new amount of product:");
                            if (!int.TryParse(Console.ReadLine(), out amount)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Cart.UpdateProductInCart(newCart, amount, id));
                            break;
                        case CartActions.Create:
                            
                            bl.Cart.OrderCreate(newCart);
                            break;
                        case CartActions.Exit:
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(@"Choose one of the following options:
1:add product to cart
2:update amount of product in cart
3:create a new order:
0:exit");
                    if (!CartActions.TryParse(Console.ReadLine(), out choice)) throw new FormatException("wrong input type");

                }
            }
            catch (BO.BlIdAlreadyExistException ex) { Console.WriteLine(ex); }
            catch (BO.BlIdDoNotExistException ex) { Console.WriteLine(ex); }
            catch (BO.BlIncorrectDateException ex) { Console.WriteLine(ex); }
            catch (BO.BlInvalidInputException ex) { Console.WriteLine(ex); }
            catch (BO.BlNullPropertyException ex) { Console.WriteLine(ex); }
            catch (BO.BlWrongCategoryException ex) { Console.WriteLine(ex); }
            catch (FormatException ex) { Console.WriteLine(ex); }
            catch (ArgumentException ex) { Console.WriteLine(ex); }
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
                    case Options.EXIT:
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
            //try-catch for tryparse in main
        }
    }
}