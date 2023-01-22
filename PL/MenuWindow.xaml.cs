using PL.Products;
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
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public BO.User? MyMenuUser
        {
            get { return (BO.User?)GetValue(MyMenuUserProperty); }
            set { SetValue(MyMenuUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyMenuUserProperty =
            DependencyProperty.Register("MyMenuUser", typeof(BO.User), typeof(Window), new PropertyMetadata(null));
        public bool IsAdmin
        {
            get { return (bool)GetValue(IsAdminProperty); }
            set { SetValue(IsAdminProperty, value); }
        }
        public static readonly DependencyProperty IsAdminProperty =
    DependencyProperty.Register("IsAdmin", typeof(bool), typeof(Window), new PropertyMetadata(false));

        public MenuWindow(BO.User user)
        {
                  InitializeComponent();
            MyMenuUser = user;
            if (MyMenuUser.Log == BO.LogIn.Maneger)
                IsAdmin = true;
            else IsAdmin= false;

        }

        private void btnOrder_Click_1(object sender, RoutedEventArgs e)
        {
            OrderForListWindow o = new OrderForListWindow();
            o.Show();
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductsListWindow p=new ProductsListWindow();
            p.Show();
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            ProductCatalogWindow catalog=new ProductCatalogWindow(MyMenuUser);
            catalog.Show();
        }

        private void btnTracking_Click(object sender, RoutedEventArgs e)
        {
            TrackingId trackingId=new TrackingId();
            trackingId.ShowDialog();
        }
    }
}
