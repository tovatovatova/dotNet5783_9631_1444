
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
    internal static List<User?> UserList { get; } = new List<User?>();
    internal static class Config
    {
        internal const int s_startOrderNumbr = 100000;//Initialization running number for order
        private static int s_nextOrderNumber = s_startOrderNumbr;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }//return and promotes running number for order
        internal const int s_startOrderItemNumber = 100000; //Initialization running number for order item
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }//return and promotes running number for order item
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
        createAndIinitUsers();
    }


    static string[,] productsNames = new string[,] {//matrix for products' category
        { "birthday cake", "high cream cake", "color block cake" } ,
        { "lotus donat", "oreo donat", "mix donat" },
        { "chokolate ice Cream", "vanile ice cream" ,"lotus ice cream"},
        { "mix Desserts", "mini donats", "mix mini cupcakes" },
        { "vafel belgi", "crep", "frozen" }
    };
    static string[] userNames = new string[] { "ortv", "tovatovatova","orokach","powerful", "warrior","hope" };//names of users to log in
    static int[] userPasswords = new int[] { 617617,123456,258528,111111,222222,333333  };

    private static void createAndIinitUsers()
    {
        for (int i= 0;  i< 3; i++) //initiate manegers
        {
            User addUser = new User();
            addUser.UserName = userNames[i];
            addUser.Password= userPasswords[i];
            addUser.Email = userNames[i] + "@gmail.com";
            addUser.Log = LogIn.Maneger;
            UserList.Add(addUser);
        }
        for (int i = 0; i < 3; i++)//initiate customers
        {
            User addUser = new User();
            addUser.UserName = userNames[i+3];
            addUser.Password = userPasswords[i+3];
            addUser.Email = userNames[i+3] + "@gmail.com";
            addUser.Log = LogIn.Customer;
            UserList.Add(addUser);
        }
    }
    private static void createAndIinitProducts()
    {

        for (int i = 0; i < 15; i++)
        {
            Product addProduct = new Product();
            addProduct.Id = 100000 + i;
            addProduct.ProductCategoty = (Category)randNumbers.Next(5);//casting for category
            addProduct.Name = productsNames[(int)addProduct.ProductCategoty, randNumbers.Next(2)];
            addProduct.Price = randNumbers.Next(25, 70);
            if (i < 10 * 0.05)//5% of the products have 0 amount in stock
                addProduct.AmountInStock = 0;
            else
                 addProduct.AmountInStock = randNumbers.Next(1, 10);
            ProductList.Add(addProduct);//add new product to list of products
        }
    }


    private static void createAndIinitOrders()
    {
        string[] names = { "orit", "tova", "reuven", "shimon", "levi" };//arrey for names
        string[] address = { "geva", "ben guryon", "habanim", "yonatan", "rotshild" };//arrey for adresses
        for (int i = 0; i < 20; i++)
        {
            Order addOrder = new Order();
            addOrder.OrderId = Config.NextOrderNumber;//initialize running number
            addOrder.CustomerName = names[i % 5];//from the arrey 
            addOrder.CustomerAddress = address[randNumbers.Next(4)];
            addOrder.CustomerEmail = addOrder.CustomerName + "@gmail.com";//thread of the name with ordinary end of gmail adress
            TimeSpan ts = new TimeSpan(randNumbers.Next(3), randNumbers.Next(23), randNumbers.Next(59), randNumbers.Next(59));
            addOrder.OrderDate = new DateTime(randNumbers.Next(2020, 2023), randNumbers.Next(1, 12), randNumbers.Next(1, 29),randNumbers.Next(0,23), randNumbers.Next(0, 59), randNumbers.Next(0, 59));
            if(i<20*0.8)//80% of the orders will have ship date right after the order date
                 addOrder.ShipDate = addOrder.OrderDate + ts;
            else
                addOrder.ShipDate = null;
            ts = new TimeSpan(randNumbers.Next(3), randNumbers.Next(0), randNumbers.Next(0, 3), randNumbers.Next(0, 23), randNumbers.Next(0, 59));
            if(i < 20*0.6)//60% of the orders will have delavery date(12 orders)
                addOrder.DeliveryDate = addOrder.ShipDate + ts;
           else
                addOrder.DeliveryDate=null;
            OrderList.Add(addOrder);
        }
    }
    private static void createAndIinitOrderItems()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < randNumbers.Next(1, 3); j++)
            {
                OrderItem addItem = new OrderItem();
                addItem.OrderItemId = Config.NextOrderItemNumber;//initialize running number
                addItem.OrderId = 100000 + i;
                addItem.ProductId = (100000) + (i + j) % 9;
                addItem.Price = (ProductList.Find(x => x?.Id == (100000) + (i + j) % 9))?.Price ?? 0;
                addItem.Amount = randNumbers.Next(1, 3);
                OrderItemList.Add(addItem);
            }

        }

    }
}







