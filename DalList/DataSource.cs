
using DO;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Dal;

internal static class DataSource
{
    static readonly Random randNumbers = new Random();
    internal static List<Product?> ProductList { get; } = new List<Product?>();
    internal static List<Order?> OrderList { get; } = new List<Order?>();
    internal static List<OrderItem?> OrderItemList { get; } = new List<OrderItem?>();
    internal static class Config
    {
        internal const int s_startOrderNumbr = 100000;
        private static int s_nextOrderNumber = s_startOrderNumbr;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
        internal const int s_startOrderItemNumber = 100000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }
    }



    static DataSource()
    {
        s_Initialize();
    }
    private static void s_Initialize()
    {
        createAndIinitProducts();
        createAndIinitOrders();
        createAndIinitOrderItems();
    }

    //public static string[] arrCakes = { "birthday cake", "high cream cake", "color block cake" };
    //public static string[] arrDonats = { "lotus donat", "oreo donat", "mix donat" };
    //public static string[] arrDesserts = { "chokolate ice Cream", "vanile ice cream" };
    //public static string[] arrGifts = { "mix Desserts", "mini donats", "mix mini cupcakes" };
    //public static string[] arrSpecials = { "vafel belgi", "crep", "frozen" };
    static string[,] productsNames = new string[,] { 
        { "birthday cake", "high cream cake", "color block cake" } ,
        { "lotus donat", "oreo donat", "mix donat" },
        { "chokolate ice Cream", "vanile ice cream" ,"lotus ice cream"},
        { "mix Desserts", "mini donats", "mix mini cupcakes" },{ "vafel belgi", "crep", "frozen" }
    };
    private static void createAndIinitProducts()
    {
        Product addProduct = new Product();
        for (int i = 0; i < 10; i++)
        {
            int x=
            ProductList.Add(new Product
            {
                Id = randNumbers.Next(100000,9999999),
                ProductCategoty=(Category)randNumbers.Next(4),
                Name = productsNames[,xxxxxx]
                
           }


            int newId = randNumbers.Next(999999);
            while (ProductList.Exists(x => x.Value.Id == newId))
            {
                newId = randNumbers.Next(999999);
            }
            addProduct.Id =newId;
            
            addProduct.ProductCategoty = (Category)randNumbers.Next(4);
            addProduct.Name = productsNames[(int)addProduct.ProductCategoty, randNumbers.Next(2)];
            addProduct.Price = randNumbers.Next(25, 70);
            addProduct.AmountInStock = randNumbers.Next(1, 10);
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// 

    private static void createAndIinitOrders()
    {
        string[] names = { "orit", "tova", "reuven", "shimon", "levi" };
        for (int i = 0; i < 20; i++)
        {
            Order addOrder = new Order();
            addOrder.OrderId = Config.NextOrderNumber;
            addOrder.CustomerName = names[i / 5];
            addOrder.CustomerEmail = addOrder.CustomerName + "@gmail.com";
            TimeSpan ts = new TimeSpan(randNumbers.Next(1, 5));
            addOrder.OrderDate = new DateTime((randNumbers.Next(2020, DateTime.Today.Year)), (randNumbers.Next(1, 12)), (randNumbers.Next(1, 29)));
            addOrder.ShipDate = addOrder.OrderDate + ts;
            addOrder.DeliveryDate = addOrder.ShipDate + ts;
        }
    }
    private static void createAndIinitOrderItems()
    {
        OrderItem addItem = new OrderItem();
        
    }

}
