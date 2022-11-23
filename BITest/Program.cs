using System.Security.Cryptography.X509Certificates;

namespace BITest
{
    internal class Program
    {
       // enum

        public static void ProductOptions()
        {

        }
        public static void OrderOptions()
        {

        }
        public static void CartOptions()
        {

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Choose one of the following object:");
            Console.WriteLine("1: Products");
            Console.WriteLine("2: Orders");
            Console.WriteLine("3: Carts");
            Console.WriteLine("0: Exit");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            while (choice!=0)
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
                        CartOptions();
                        break ;
                    default:
                        break;
                }
                Console.WriteLine("Choose one of the following object:");
                Console.WriteLine("1: Products");
                Console.WriteLine("2: Orders");
                Console.WriteLine("3: Carts");
                Console.WriteLine("0: Exit");
                if (!int.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            }
        }
    }
}