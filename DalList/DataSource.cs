
using DO;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Dal;

internal sealed class DataSource
{
    static readonly Random numbers = new Random();
    internal List<Product?> ProductList { get; } = new List<Product?>();
    internal List<Order?> OrderList { get; } = new List<Order?>();
    internal List<OrderItem?> OrderItemList { get; } = new List<OrderItem?>();
    internal static class Config
    {
        internal const int s_startOrderNumbr = 100000;
        private static int s_nextOrderNumber = s_startOrderNumbr;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
        internal const int s_startOrderItemNumber = 100000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }
    }
        
    private void addProduct(int productId,string productName,double productPrice,bool inStock)
    {
      Product newProduct=new Product();
        newProduct.Id = productId;
        newProduct.Name = productName;
        newProduct.Price = productPrice;
        newProduct.InStock = inStock;

        
    }

    private DataSource()
    {
        s_Initialize();
    }
    private void s_Initialize()
    {
        createAndIinitProducts();
        createAndIinitOrders();
        createAndIinitOrderItems();
    }

}
