
using Dal;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {
        enum OP { A,B,C}
        enum Options { ADD, SEARCH, PRINT_ALL, UPDATE, DELETE, EXIT, SEARCH_ORDER_ITEMS, SEARCH_BY_ORDER_AND_PRODUCT };
    DalOrder dalOrder = new DalOrder();
    DalOrderItem dalOrderItem = new DalOrderItem();
    DalProduct dalProduct = new DalProduct();
    //foreach (var item in dalOrder.GetAll())
    //    Console.WriteLine(item);


    Console.WriteLine("Choose one of the following object:");
        Console.WriteLine("1: Products");
        Console.WriteLine("2: Orders");
        Console.WriteLine("3: OrderItems");
        Console.WriteLine("0: Exit");

        void ProductOptions()
    {
        char ch;
        Console.WriteLine("choose one of the following options:");
        Console.WriteLine("a: Add new product");
        Console.WriteLine("b: See product details by insert product code");
        Console.WriteLine("c: See all products");
        Console.WriteLine("d: update product");
        Console.WriteLine("e: delete product");
        Console.WriteLine("g: exit");

        ch = char.Parse(Console.ReadLine());
        while (ch != 0)
        {
            switch (ch)
            {
                default:
                    break;
            }
        }
        // Console.WriteLine("");
    }


}

//dalOrder.Add(new DO.Order() {1000 })


