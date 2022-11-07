
using DO;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Dal;

internal sealed class DataSource
{
    static readonly Random numbers=new Random();
    internal List<Product?> ProductList { get; } = new List<Product?>();
    internal List<Order?> OrderList { get; } = new List<Order?>();
    internal List<OrderItem?> OrderItemList { get; } = new List<OrderItem?>();
    private void addProduct(string? idNum,string? nameProd,double priceProd,bool inStockProd)
    {
      Product product;
        product.Id = idNum;
        ProductList.Add(product);
    }
}
