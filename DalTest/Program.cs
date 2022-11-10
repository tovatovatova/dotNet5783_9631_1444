
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
        foreach (var item in dalOrder.GetAll())
            Console.WriteLine(item);

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
                        dalProduct.GetAll();
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
            }
            // Console.WriteLine("");
        }
        void OrderOptions()
        {
            char ch;
            Console.WriteLine("choose one of the following options:");
            Console.WriteLine("a: Add new order");
            Console.WriteLine("b: See order details by insert order code");
            Console.WriteLine("c: See all orders");
            Console.WriteLine("d: update order");
            Console.WriteLine("e: delete order");
            Console.WriteLine("h: exit");
            ch = char.Parse(Console.ReadLine());
            while (ch!='h')
            {
                switch (ch)
                {
                    case 'a':
                        Console.WriteLine("enter id:");
                        break;
                    default:
                        break;
                }
            }




        }
        void OrderItemOptions()
        {
            char ch;
            Console.WriteLine("choose one of the following options:");
            Console.WriteLine("a: Add new order item");
            Console.WriteLine("b: See order item details by insert order code");
            Console.WriteLine("c: See all order items");
            Console.WriteLine("d: update order item");
            Console.WriteLine("e: delete order item");
            Console.WriteLine("h: exit");
            ch=char.Parse(Console.ReadLine());
            while (ch!='h')
            {
                switch (ch)
                {
                    case 'a':
                        string input1;
                        int x1;
                        double y1;
                        OrderItem orderItem = new OrderItem();
                        Console.WriteLine("enter order item id:");
                        input1 = Console.ReadLine();
                        if (int.TryParse(input1, out x1))
                            orderItem.OrderItemId = x1;
                        Console.WriteLine("enter product id:");
                        input1 = Console.ReadLine();
                        if (int.TryParse(input1, out x1))
                            orderItem.ProductId = x1;
                        Console.WriteLine("enter order id:");
                        input1 = Console.ReadLine();
                        if (int.TryParse(input1, out x1))
                            orderItem.OrderId = x1;
                        Console.WriteLine("enter price:");
                        input1= Console.ReadLine();
                        if(double.TryParse(input1 , out y1))
                            orderItem.Price = y1;
                        Console.WriteLine("enter amount:");
                        input1 = Console.ReadLine();
                        if (int.TryParse(input1, out x1))
                            orderItem.Amount = x1;
                        dalOrderItem.Add(orderItem);
                        break;
                    case 'b':
                        string input2;
                        int x2;
                        Console.WriteLine("enter order item id:");
                        input2= Console.ReadLine();
                        if (int.TryParse(input2, out x2))
                            Console.WriteLine(dalOrderItem.GetById(x2));
                        break;
                    case 'c':
                        ////
                        break;
                    default:
                        break;
                }
            }




        }
    }
}

//dalOrder.Add(new DO.Order() {1000 })


