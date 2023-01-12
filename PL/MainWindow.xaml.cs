﻿
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


        public BO.User? PlUser
        {
            get { return (BO.User?)GetValue(PlUserProperty); }
            set { SetValue(PlUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlUserProperty =
            DependencyProperty.Register("PlUser", typeof(BO.User), typeof(Window), new PropertyMetadata(null));



        public MainWindow()
        {
            PlUser = new BO.User();
            InitializeComponent();

        }




        /// <summary>
        /// when pressing the admin buttum open the product list window file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            //LogInWindow l = new LogInWindow();
            //l.Show();
            //need to see order and product buttuns-transfer to logIn!!
            
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
            ProductCatalogWindow productCatalog = new ProductCatalogWindow(PlUser);
            productCatalog.Show();
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
                    string messageBoxText = "This "+ex.Message.ToString()+" doesnt exist";
                    string caption = "error";
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                    if (result == MessageBoxResult.OK)
                        txtidd.Text = "";
                }
            }
        }
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow m = new MainWindow();
            if (PlUser.UserName == null)//empty name 
            {
                string messageBoxText = " invalid input";
                string caption = " ";
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result==MessageBoxResult.OK)
                {
                    this.Show();
                    return;
                }
                //Close();
                //// MainWindow m = new MainWindow();
                //this.btnLogIn.Visibility = Visibility.Visible;
                //this.btnGuest.Visibility = Visibility.Visible;
                //this.btnNewOrder.Visibility = Visibility.Hidden;
                //this.btnTracking.Visibility = Visibility.Hidden;
                //this.btnCreate.Visibility = Visibility.Visible;
                //this.Show();
                //return;
            }

            try
            {
                bl.User.compare(PlUser);
            }
            catch (BO.BlIdDoNotExistException ex)//if user is not in the system
            {

                string messageBoxText = ex.Entity?.ToString() + " try again ";
                string caption = " ";
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                //Close();
                // MainWindow m = new MainWindow();
                //log.Visibility = Visibility.Hidden;
                //this.btnLogIn.Visibility = Visibility.Visible;
                //this.btnGuest.Visibility = Visibility.Visible;
                //this.btnNewOrder.Visibility = Visibility.Hidden;
                //this.btnTracking.Visibility = Visibility.Hidden;
                //this.Show();
                if (result == MessageBoxResult.OK)
                {
                    this.Show();
                    return;
                }
            }
            //if customer
            if (PlUser.Log == BO.LogIn.Customer)
            {
               // Close();
                //   MainWindow m = new MainWindow();
                // m.btnAdmin.Visibility = Visibility.Hidden;//need to remove...
                this.btnLogIn.Visibility = Visibility.Hidden;
                this.btnNewOrder.Visibility = Visibility.Visible;
               // this.btnGuest.Visibility = Visibility.Hidden;
                this.btnTracking.Visibility = Visibility.Visible;
                this.btnCreate.Visibility = Visibility.Hidden;
                log.Visibility = Visibility.Hidden;
                this.Show();
            }
            //if maneger
            if (PlUser.Log == BO.LogIn.Maneger)
            {
                //Close();
                //  MainWindow m = new MainWindow();

                this.btnProduct.Visibility = Visibility.Visible;
                this.btnOrder.Visibility = Visibility.Visible;
                // m.btnAdmin.Visibility = Visibility.Hidden;
                this.btnLogIn.Visibility = Visibility.Hidden;
                //this.btnGuest.Visibility = Visibility.Hidden;
                this.btnNewOrder.Visibility = Visibility.Hidden;
                this.btnTracking.Visibility = Visibility.Hidden;
                this.btnCreate.Visibility = Visibility.Hidden;
                log.Visibility = Visibility.Hidden;
                this.Show();
            }

        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            btnCreate.Visibility = Visibility.Hidden;

            log.Visibility = Visibility.Visible;
           // LogInWindow l = new LogInWindow();
           // l.Show();
        }

        private void imgLogIn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LogInWindow l = new LogInWindow();
            l.Show();
        }
        
        //private void btnGuest_Click(object sender, RoutedEventArgs e)
        //{
        //    //MainWindow m = new MainWindow();
        //    this.btnLogIn.Visibility = Visibility.Hidden;
        //    this.btnNewOrder.Visibility = Visibility.Visible;
        //    this.btnTracking.Visibility = Visibility.Visible;
        //    this.btnProduct.Visibility = Visibility.Hidden;
        //    this.btnOrder.Visibility = Visibility.Hidden;
        //  //  this.btnGuest.Visibility = Visibility.Hidden;
        //    this.btnCreate.Visibility = Visibility.Hidden;
        //    this.Show();
        //}

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            submit.Visibility = Visibility.Visible;
            btnLogIn.Visibility = Visibility.Hidden;

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText, caption;
            MessageBoxImage icon;
            MessageBoxResult result;

            if (PlUser.UserName == null)//empty name 
            {
                messageBoxText = " invalid input";
                caption = " ";
                icon = MessageBoxImage.Information;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    this.Show();
                    return;
                }
            }

                try//tries to add a customer to the system
            {
                bl.User.addUser(PlUser);
                messageBoxText = "user added successfully";
                caption = "";
                icon = MessageBoxImage.Information;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.None);
                if (result==MessageBoxResult.OK)
                {
                    //user added successfully
                    //its a customer
                    this.btnLogIn.Visibility = Visibility.Hidden;
                    this.btnNewOrder.Visibility = Visibility.Visible;
                    //this.btnGuest.Visibility = Visibility.Hidden;
                    this.btnTracking.Visibility = Visibility.Visible;
                    this.btnCreate.Visibility = Visibility.Hidden;
                    submit.Visibility = Visibility.Hidden;
                    //PlUser = new();
                    this.Show();
                }
            }
            catch (BO.BlInvalidInputException ex)
            {
                //throw an error message box 
                messageBoxText = ex.Message.ToString();
                caption = "error";
                icon = MessageBoxImage.Error;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    this.Show();
                    return;
                }
            }
            catch (BO.BlIdAlreadyExistException ex)
            {
                //throw an error message box 
                messageBoxText = ex.Message.ToString();
                caption = "error";
                icon = MessageBoxImage.Error;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    this.Show();
                    return;
                }
            }
            

        }
    }
}
