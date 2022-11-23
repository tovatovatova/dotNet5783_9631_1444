﻿using BO;
using System.Security.Cryptography.X509Certificates;

namespace BITest
{
    internal class Program
    {
        enum Options {PRODUCT=1,ORDER,CART};
        enum ProductActions {Product_List=1,ProductDetails,Add,Delete,Update,Catalog,Product_in_Catalog}
        enum CartOptions {Add=1,Update,Create }
        enum OrderOption { Get_Order=1,Order_List,Update_Ship,Update_Delivery,Order_Tracking,Update_Order}

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
                        break;
                    case ProductActions.ProductDetails:
                        break;
                    case ProductActions.Add:
                        break;
                    case ProductActions.Delete:
                        break;
                    case ProductActions.Update:
                        break;
                    case ProductActions.Catalog:
                        break;
                    case ProductActions.Product_in_Catalog:
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

        }
        public static void CartOptions()
        {

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
            while (choice!=Options.EXIT)
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