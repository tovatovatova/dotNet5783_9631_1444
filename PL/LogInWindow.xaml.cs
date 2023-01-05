using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
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

        public LogInWindow()
        {
            InitializeComponent();
            PlUser = new BO.User();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            if (PlUser.UserName==null)//empty name 
            {
                string messageBoxText = " invalid input";
                string caption = " ";
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                Close();
                // MainWindow m = new MainWindow();
                m.btnLogIn.Visibility = Visibility.Visible;
                m.btnGuest.Visibility = Visibility.Visible;
                m.btnNewOrder.Visibility = Visibility.Hidden;
                m.btnTracking.Visibility = Visibility.Hidden;
                m.Show();
                return;
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
               // Close();
               // MainWindow m = new MainWindow();
                //m.btnLogIn.Visibility = Visibility.Visible;
                //m.btnGuest.Visibility = Visibility.Visible;
                //m.btnNewOrder.Visibility = Visibility.Hidden;
                //m.btnTracking.Visibility = Visibility.Hidden;
                //m.Show();
                
                return;
            }
            //if customer
            if (PlUser.Log==BO.LogIn.Customer)
            {
                Close();
             //   MainWindow m = new MainWindow();
               // m.btnAdmin.Visibility = Visibility.Hidden;//need to remove...
                m.btnLogIn.Visibility = Visibility.Hidden;
                m.btnNewOrder.Visibility = Visibility.Visible;
                m.btnGuest.Visibility = Visibility.Hidden;
                m.btnTracking.Visibility = Visibility.Visible;
                m.Show();
            }
            //if maneger
            if (PlUser.Log == BO.LogIn.Maneger)
            {
                Close();
              //  MainWindow m = new MainWindow();
                m.btnProduct.Visibility = Visibility.Visible;
                m.btnOrder.Visibility = Visibility.Visible;
               // m.btnAdmin.Visibility = Visibility.Hidden;
                m.btnLogIn.Visibility = Visibility.Hidden;
                m.btnGuest.Visibility = Visibility.Hidden;
                m.btnNewOrder.Visibility = Visibility.Hidden;
                m.btnTracking.Visibility = Visibility.Hidden;
                m.Show();
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UserWindow u = new();
            u.ShowDialog();
        }
    }
  
}
