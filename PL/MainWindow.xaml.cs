
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
            btnOrder.Visibility = Visibility.Visible;
            btnProduct.Visibility = Visibility.Visible; 
        }

        private void btnTracking_Click(object sender, RoutedEventArgs e)
        {
            txtidd.Visibility = Visibility.Visible;
            lblTracking.Visibility = Visibility.Visible;
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

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            ProductCatalogWindow productCatalog = new ProductCatalogWindow();
            productCatalog.ShowDialog();
        }

        private void btnTracking_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtidd.Visibility = Visibility.Visible;
            lblTracking.Visibility = Visibility.Visible;
        }

        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;
            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
            || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (Char.IsControl(c)) return;
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
                            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other 

            return;

        }

        private void txtidd_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    BO.Order o = bl.Order.GetOrderByID(Convert.ToInt32(txtidd.Text));
                    OrderTrackingWindow orderTrackingWindow = new OrderTrackingWindow(sender, e, Convert.ToInt32(txtidd.Text));
                    orderTrackingWindow.Show();


                }
                catch(BO.BlInvalidInputException ex)
                {
                    string messageBoxText = ex.Message.ToString();
                    string caption = "error";
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                    if (result == MessageBoxResult.OK)
                        txtidd.Text = "";
                }
                catch (BO.BlIdDoNotExistException ex)//product doesnt exist
                {//throw an error message box 
                    string messageBoxText = ex.Message.ToString();
                    string caption = "error";
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                    if (result == MessageBoxResult.OK)
                        txtidd.Text = "";
                }
            }
        }
    }
}
