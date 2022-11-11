using Dal;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {

        DalOrder dalOrder = new DalOrder();
        DalOrderItem dalOrderItem = new DalOrderItem();
        DalProduct dalProduct = new DalProduct();
        //foreach (var item in dalOrder.GetAll())
        //    Console.WriteLine(item);
        //foreach (Product item in dalProduct.GetAll())
        //    Console.WriteLine(item);

        Console.WriteLine("Choose one of the following object:");
        Console.WriteLine("1: Products");
        Console.WriteLine("2: Orders");
        Console.WriteLine("3: OrderItems");
        Console.WriteLine("0: Exit");
        int choice = int.Parse(Console.ReadLine());
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
        void ProductOptions()
        {
            char ch;
            Console.WriteLine("choose one of the following options:");
            Console.WriteLine("a: Add new product");
            Console.WriteLine("b: See product details by insert product code");
            Console.WriteLine("c: See all products");
            Console.WriteLine("d: update product");
            Console.WriteLine("e: delete product");
            Console.WriteLine("h: exit");
            ch = char.Parse(Console.ReadLine());
            while (ch != 'h')
            {

                switch (ch)
                {
                    case 'a':

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
                            product.Price = x1;
                        Console.WriteLine("enter amount in stock:");
                        input1 = Console.ReadLine();
                        if (int.TryParse(input1, out x1))
                            product.AmountInStock = x1;
                        Console.WriteLine("enter category number:");
                        if (int.TryParse(input1, out x1))
                            product.ProductCategoty = (Category)x1;
                        dalProduct.Add(product);
                        break;
                    case 'b':
                        string input2;
                        int x2;
                        Console.WriteLine("enter id:");
                        input2 = Console.ReadLine();
                        if (int.TryParse(input2, out x2))
                        {
                            Console.WriteLine(dalProduct.GetById(x2));
                        }
                        break;
                    case 'c':

                        foreach (var item in dalProduct.GetAll())
                            Console.WriteLine(item);
                        break;
                    case 'd':
                        string input3;
                        int x3;
                        double y3;
                        Product product1 = new Product();
                        Console.WriteLine("enter id:");
                        input3 = Console.ReadLine();
                        if (int.TryParse(input3, out x3))
                            product1.Id = x3;
                        Console.WriteLine("enter name:");
                        product1.Name = Console.ReadLine();
                        Console.WriteLine("enter price:");
                        input1 = Console.ReadLine();
                        if (double.TryParse(input3, out y3))
                            product1.Price = x3;
                        Console.WriteLine("enter amount in stock:");
                        input3 = Console.ReadLine();
                        if (int.TryParse(input3, out x3))
                            product1.AmountInStock = x3;
                        Console.WriteLine("enter category number:");
                        if (int.TryParse(input3, out x3))
                            product1.ProductCategoty = (Category)x3;
                        dalProduct.UpDate(product1);
                        break;
                    case 'e':
                        string input4;
                        int x4;
                        Console.WriteLine("enter id:");
                        input2 = Console.ReadLine();
                        if (int.TryParse(input2, out x4))
                            dalProduct.Delete(x4);
                        break;
                    default:
                        break;
                }
                ch = char.Parse(Console.ReadLine());

            }
            // Console.WriteLine("");
        }
        void OrderOptions()
        {
            char Orderch;
            Console.WriteLine("choose one of the following options:");
            Console.WriteLine("a: Add new order");
            Console.WriteLine("b: See order details by insert order code");
            Console.WriteLine("c: See all orders");
            Console.WriteLine("d: update order");
            Console.WriteLine("e: delete order");
            Console.WriteLine("h: exit");
            Orderch = char.Parse(Console.ReadLine());
            while (Orderch != 'h')
            {
                switch (Orderch)
                {
                    case 'a':
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
                        dalOrder.Add(newOrder);
                        break;
                    case 'b':



                    default:
                        break;
                }
                Orderch = char.Parse(Console.ReadLine());

            }




        }
        void OrderItemOptions()
        {
            String OrderItemch;
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
                int productId;
                //int itemId;
                
                //int orderId;
                //int amount;
                double price;
                string strInput;
                switch (OrderItemch)
                {
                    case "a":
                        //   string input1;
                        int input;
                        double inputD;
                        OrderItem orderItem = new OrderItem();
                        Console.WriteLine("enter order item id:");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out input))
                            orderItem.OrderItemId = input;
                        Console.WriteLine("enter product id:");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out input))
                            orderItem.ProductId = input;
                        Console.WriteLine("enter order id:");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out input))
                            orderItem.OrderId =input;
                        Console.WriteLine("enter price:");
                        strInput = Console.ReadLine();
                        if (double.TryParse(strInput, out inputD))
                            orderItem.Price = inputD;
                        Console.WriteLine("enter amount:");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out input))
                            orderItem.Amount = input;
                        dalOrderItem.Add(orderItem);
                        break;
                    case "b":
                        string itemIdStr2;
                        int inputS;
                       
                        Console.WriteLine("enter order item id:");
                        itemIdStr2 = Console.ReadLine();
                        if (int.TryParse(itemIdStr2, out inputS))
                            Console.WriteLine(dalOrderItem.GetById(inputS));
                        break;
                    case "c":
                        foreach (var item in dalOrderItem.GetAll())
                            Console.WriteLine(item);
                        break;
                    case "d":
                        int inputUp;
                        double inputUpDo;
                        Console.WriteLine("insert order Item details to update");
                        OrderItem ordItemToUpD = new OrderItem();
                        Console.WriteLine("insert order item Id");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out inputUp))
                            ordItemToUpD.OrderId = inputUp;
                        Console.WriteLine("inser product id");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out inputUp))
                            ordItemToUpD.ProductId = inputUp;
                        Console.WriteLine("inser order id");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out inputUp))
                            ordItemToUpD.OrderId = inputUp;
                        Console.WriteLine("insert price");
                        strInput = Console.ReadLine();
                        if (double.TryParse(strInput, out inputUpDo))
                            ordItemToUpD.Price = inputUpDo;
                        Console.WriteLine("insert item amount");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out inputUp))
                            ordItemToUpD.Amount = inputUp;
                        dalOrderItem.Update(ordItemToUpD);
                        break;
                    case "e":
                        Console.WriteLine("insert id of item to delete");
                        strInput = Console.ReadLine();
                        int itemIdDel  ;
                        if (int.TryParse(strInput, out itemIdDel))
                            dalOrder.Delete(itemIdDel);
                        break;
                    case "f":
                        int idOr;
                        Console.WriteLine("insert id of order to see the itemss in this order");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out idOr))
                            foreach (var item in dalOrderItem.GetItemsInOrder(idOr))
                                Console.WriteLine(item);

                        break;
                    case "g":
                        int productC;
                        int orderC;
                        Console.WriteLine("insert product id");
                        strInput = Console.ReadLine();
                        if (int.TryParse(strInput, out productC))
                        {
                            Console.WriteLine("insert order id");
                            strInput = Console.ReadLine();
                            if (int.TryParse(strInput, out orderC))
                                dalOrderItem.GetItemByOrderAndProduct(orderC, productC);
                        }
                        break;
                        case "h":
                        return;
                        break;
                    default:
                        break;
                }
                OrderItemch = Console.ReadLine();
            }




        }
    }
}

