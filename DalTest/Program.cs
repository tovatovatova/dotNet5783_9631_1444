using Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    /// <summary>
    /// 3 Objects to provide access to dalList classes
    /// </summary>
    //private static DalOrder dalOrder = new DalOrder();
    //private static DalOrderItem dalOrderItem = new DalOrderItem();
    //private static DalProduct dalProduct = new DalProduct();
     static IDal dal = new DalList();

    /// <summary>
    /// function for the options of product check (add,delete,update,and so)
    /// </summary>
    public static void ProductOptions()
    {
        char ch;
        //print the options of checking choice 
        Console.WriteLine("choose one of the following options:");
        Console.WriteLine("a: Add new product");
        Console.WriteLine("b: See product details by insert product code");
        Console.WriteLine("c: See all products");
        Console.WriteLine("d: update product");
        Console.WriteLine("e: delete product");
        Console.WriteLine("h: exit");
        //get the letter which mean the operation the user chose
        ch = char.Parse(Console.ReadLine());
        while (ch != 'h')
        {
            try
            {
                switch (ch)
                {
                    case 'a'://add product to list
                        //get values in order to build new product and add to the list
                        string input1;
                        int x1;
                        double y;
                        Product product = new Product();
                        Console.WriteLine("enter id:");
                        input1 = Console.ReadLine();
                        if (int.TryParse(input1, out x1))
                            product.Id = x1;
                        Console.WriteLine("enter name:");
                        product.Name = Console.ReadLine();
                        Console.WriteLine("enter price:");
                        input1 = Console.ReadLine();
                        if (double.TryParse(input1, out y))
                            product.Price = y;
                        Console.WriteLine("enter amount in stock:");
                        input1 = Console.ReadLine();
                        if (int.TryParse(input1, out x1))
                            product.AmountInStock = x1;
                        Console.WriteLine("enter category number:");
                        if (int.TryParse(input1, out x1))
                            product.ProductCategoty = (Category)x1;
                       dal.Product.Add(product);
                        
                        break;
                    case 'b'://input:id of product. output:details of this product.
                        string input2;
                        int x2;
                        Console.WriteLine("enter id:");
                        input2 = Console.ReadLine();
                        if (int.TryParse(input2, out x2))
                        {
                            Console.WriteLine(dal.Product.GetById(x2));
                        }
                        else throw new Exception("Wrong input");
                        break;
                    case 'c'://prints all the list

                        foreach (var item in dal.Product.GetAll())
                            Console.WriteLine(item);
                        break;
                    case 'd'://update product
                        string input3;
                        int x3;
                        double y3;
                        Product product1 = new Product();
                        Console.WriteLine("enter id:");
                        input3 = Console.ReadLine();
                        if (int.TryParse(input3, out x3))
                            product1.Id = x3;
                        else throw new Exception("Wrong input");
                        Console.WriteLine("enter name:");
                        product1.Name = Console.ReadLine();
                        Console.WriteLine("enter price:");
                        input1 = Console.ReadLine();
                        if (double.TryParse(input3, out y3))
                            product1.Price = y3;
                        Console.WriteLine("enter amount in stock:");
                        input3 = Console.ReadLine();
                        if (int.TryParse(input3, out x3))
                            product1.AmountInStock = x3;
                        Console.WriteLine("enter category number:");
                        if (int.TryParse(input3, out x3))
                            product1.ProductCategoty = (Category)x3;
                        else throw new Exception("Wrong input");
                        dal.Product.Update(product1);
                        break;
                    case 'e'://delete product
                        string input4;
                        int x4;
                        Console.WriteLine("enter id:");
                        input2 = Console.ReadLine();
                        if (int.TryParse(input2, out x4))
                            dal.Product.Delete(x4);
                        else
                            throw new Exception("Wrong input");
                        break;
                    default:
                        break;
                }
            }
            //in case one of the actions was not valid- the exception message print here
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //again get choice from user to continue the checking
            Console.WriteLine("choose one of the following options:");
            Console.WriteLine("a: Add new product");
            Console.WriteLine("b: See product details by insert product code");
            Console.WriteLine("c: See all products");
            Console.WriteLine("d: update product");
            Console.WriteLine("e: delete product");
            Console.WriteLine("h: exit");
            ch = char.Parse(Console.ReadLine());

        }
    }
    /// <summary>
    /// function for the options of order check (add,delete,update,and so)
    /// </summary>
    public static void OrderOptions()
    {
        string Orderch;
        //output for order options
        Console.WriteLine("choose one of the following options:");
        Console.WriteLine("a: Add new order");
        Console.WriteLine("b: See order details by insert order code");
        Console.WriteLine("c: See all orders");
        Console.WriteLine("d: update order");
        Console.WriteLine("e: delete order");
        Console.WriteLine("h: exit");
        Orderch = Console.ReadLine();
        while (Orderch != "h")
        {
            string orderInputstr;
            try
            {
                switch (Orderch)
                {
                    case "a"://add order by get values from user
                        Order newOrder = new Order();
                        Console.WriteLine("insert new Order details");
                        Console.WriteLine("enter order id:");
                        newOrder.OrderId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter customer name:");
                        newOrder.CustomerName = Console.ReadLine();
                        Console.WriteLine("insert address for delivery");
                        newOrder.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("insert email adress");
                        newOrder.CustomerEmail = Console.ReadLine();
                        newOrder.OrderDate = DateTime.Now;
                        newOrder.ShipDate = null;
                        newOrder.DeliveryDate = null;
                        dal.Order.Add(newOrder);
                        break;
                    case "b"://prints order by id

                        int idO;
                        Console.WriteLine("enter id:");
                        orderInputstr = Console.ReadLine();
                        if (int.TryParse(orderInputstr, out idO))
                        {
                            Console.WriteLine(dal.Order.GetById(idO));
                        }
                        else
                            throw new Exception("Wrong input");
                        break;
                    case "c"://prints all the orders
                        foreach (Order order in dal.Order.GetAll())
                            Console.WriteLine(order);
                        break;
                    case "d"://updates order
                        Order newOrder1 = new Order();
                        DateTime date;
                        string input;
                        Console.WriteLine("insert new Order details");
                        Console.WriteLine("enter order id:");
                        newOrder1.OrderId = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter customer name:");
                        newOrder1.CustomerName = Console.ReadLine();
                        Console.WriteLine("insert address for delivery:");
                        newOrder1.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("insert email adress:");
                        newOrder1.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("insert order date in the format ##/##/##");
                        input = Console.ReadLine();
                        if (DateTime.TryParse(input, out date))
                            newOrder1.OrderDate = date;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("insert ship date in the format ##/##/##");
                        input = Console.ReadLine();
                        if (DateTime.TryParse(input, out date))
                            newOrder1.ShipDate = date;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("insert delivery date in the format ##/##/##");


                        input = Console.ReadLine();
                        if (DateTime.TryParse(input, out date))
                            newOrder1.DeliveryDate = date;
                        else
                            throw new Exception("Wrong input");
                        dal.Order.Update(newOrder1);
                        break;
                    case "e"://deletes order
                        int idDelO;
                        Console.WriteLine("enter id:");
                        orderInputstr = Console.ReadLine();
                        if (int.TryParse(orderInputstr, out idDelO))
                            dal.Order.Delete(idDelO);
                        else
                            throw new Exception("Wrong input");
                        break;



                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("choose one of the following options:");
            Console.WriteLine("a: Add new order");
            Console.WriteLine("b: See order details by insert order code");
            Console.WriteLine("c: See all orders");
            Console.WriteLine("d: update order");
            Console.WriteLine("e: delete order");
            Console.WriteLine("h: exit");
            Orderch = Console.ReadLine();
        }




    }
    /// <summary>
    /// function for the options of product check (add,delete,update,and so)
    /// </summary>
    public static void OrderItemOptions()
    {
        String OrderItemch;
        //output for orderitem options
        Console.WriteLine("choose one of the following options:");
        Console.WriteLine("a: Add new order item");
        Console.WriteLine("b: search order item details by order code");
        Console.WriteLine("c: See all order items");
        Console.WriteLine("d: update order item");
        Console.WriteLine("e: delete order item");
        Console.WriteLine("f: print items in order");
        Console.WriteLine("g: search item by order id and product id");
        Console.WriteLine("h: exit");
        OrderItemch = Console.ReadLine();
        while (OrderItemch != "h")
        {
            string strInput;
            try
            {

                switch (OrderItemch)
                {

                    case "a"://add order item
                        int input;
                        double inputD;
                        OrderItem orderItem = new OrderItem();
                        Console.WriteLine("enter order item id:");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out input))
                            orderItem.OrderItemId = input;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("enter product id:");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out input))
                            orderItem.ProductId = input;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("enter order id:");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out input))
                            orderItem.OrderId = input;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("enter price:");
                        strInput = Console.ReadLine();
                        if (double.TryParse(strInput, out inputD))
                            orderItem.Price = inputD;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("enter amount:");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out input))
                            orderItem.Amount = input;
                        else
                            throw new Exception("Wrong input");
                        dal.OrderItem.Add(orderItem);
                        break;
                    case "b"://prints order item by id
                        string itemIdStr2;
                        int inputS;

                        Console.WriteLine("enter order item id:");
                        itemIdStr2 = Console.ReadLine();
                        if (int.TryParse(itemIdStr2, out inputS))
                            Console.WriteLine(dal.OrderItem.GetById(inputS));
                        else
                            throw new Exception("Wrong input");
                        break;
                    case "c"://prints all the order item list
                        foreach (var item in dal.OrderItem.GetAll())
                            Console.WriteLine(item);
                        break;
                    case "d"://updates order item
                        int inputUp;
                        double inputUpDo;
                        Console.WriteLine("insert order Item details to update");
                        OrderItem ordItemToUpD = new OrderItem();
                        Console.WriteLine("insert order item Id");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out inputUp))
                            ordItemToUpD.OrderId = inputUp;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("inser product id");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out inputUp))
                            ordItemToUpD.ProductId = inputUp;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("inser order id");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out inputUp))
                            ordItemToUpD.OrderId = inputUp;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("insert price");
                        strInput = Console.ReadLine();
                        if (double.TryParse(strInput, out inputUpDo))
                            ordItemToUpD.Price = inputUpDo;
                        else
                            throw new Exception("Wrong input");
                        Console.WriteLine("insert item amount");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out inputUp))
                            ordItemToUpD.Amount = inputUp;
                        else
                            throw new Exception("Wrong input");
                        dal.OrderItem.Update(ordItemToUpD);
                        break;//delete order item
                    case "e":
                        Console.WriteLine("insert id of item to delete");
                        strInput = Console.ReadLine();
                        int itemIdDel;
                        if (int.TryParse(strInput, out itemIdDel))
                            dal.OrderItem.Delete(itemIdDel);
                        else
                            throw new Exception("Wrong input");
                        break;
                    case "f"://prints order items of a spsific order
                        int idOr;
                        Console.WriteLine("insert id of order to see the itemss in this order");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out idOr))
                        {
                            foreach (var item in dal.OrderItem.GetItemsInOrder(idOr))
                                Console.WriteLine(item);
                        }
                        else
                            throw new Exception("Wrong input");

                        break;
                    case "g"://prints order item according to tje product it and order id
                        int productC;
                        int orderC;
                        Console.WriteLine("insert product id");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out productC))
                        {
                            Console.WriteLine("insert order id");
                            strInput = Console.ReadLine();
                            if (int.TryParse(strInput, out orderC))
                                Console.WriteLine(dal.OrderItem.GetItemByOrderAndProduct(orderC, productC));
                            else
                                throw new Exception("Wrong input");
                        }
                        else
                            throw new Exception("Wrong input");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("choose one of the following options:");
            Console.WriteLine("a: Add new order item");
            Console.WriteLine("b: search order item details by order code");
            Console.WriteLine("c: See all order items");
            Console.WriteLine("d: update order item");
            Console.WriteLine("e: delete order item");
            Console.WriteLine("f: print items in order");
            Console.WriteLine("g: search item by order id and product id");
            Console.WriteLine("h: exit");
            OrderItemch = Console.ReadLine();
        }
    }

    /// <summary>
    /// The main program-offers menu of 3 object checking
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        //options of what object to check - ordered by numbers :
        Console.WriteLine("Choose one of the following object:");
        Console.WriteLine("1: Products");
        Console.WriteLine("2: Orders");
        Console.WriteLine("3: OrderItems");
        Console.WriteLine("0: Exit");
        int choice = int.Parse(Console.ReadLine());
        //in case the input is not one of the options

        if (choice < 0 || choice > 3) throw new Exception("this option is not exist");
        while (choice != 0)
        {
            try
            {
                switch (choice)
                {
                    case 1:
                        ProductOptions();

                        break;
                    case 2:
                        OrderOptions();
                        break;
                    case 3:
                        OrderItemOptions();
                        break;
                    default:
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Choose one of the following object:");
            Console.WriteLine("1: Products");
            Console.WriteLine("2: Orders");
            Console.WriteLine("3: OrderItems");
            Console.WriteLine("0: Exit");
            if (!int.TryParse(Console.ReadLine(),out choice)) throw new Exception("This option not exist!");

        }

    }
}


