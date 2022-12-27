
using PL.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;




namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BlApi.IBl bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        
        }
        /// <summary>
        /// when pressing the admin buttum open the product list window file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
           //need to see order and product buttuns
        }

        private void btnTracking_Click(object sender, RoutedEventArgs e)
        {
            OrderTrackingWindow orderTrackingWindow = new OrderTrackingWindow(sender,e,(txtOrderId.Text));
            orderTrackingWindow.Show();
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductsListWindow products = new ProductsListWindow();
            products.Show();
        }

        private void btnOrder_Click_1(object sender, RoutedEventArgs e)
        {
            OrderForListWindow orders = new OrderForListWindow();
            orders.Show();
        }
    }
}
