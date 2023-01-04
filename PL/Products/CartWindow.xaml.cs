using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
   
    public partial class CartWindow : Window
    {


        BlApi.IBl bl = BlApi.Factory.Get();

       

     
        public List<BO.OrderItem?> myOrderItems
        {
            get { return (List<BO.OrderItem?>)GetValue(myOrderItemsProperty); }
            set { SetValue(myOrderItemsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for PlProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myOrderItemsProperty =
            DependencyProperty.Register("myOrderItems", typeof(List<BO.OrderItem?>), typeof(Window), new PropertyMetadata(null));
       public BO.Cart myCart
        {
            get { return (BO.Cart)GetValue(myCartProperty); }
            set { SetValue(myCartProperty, value); }
        }
        public static readonly DependencyProperty myCartProperty =
            DependencyProperty.Register("myCartItems", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));
       



        public CartWindow(Cart cart)
        {
            myCart = cart;
          
            InitializeComponent();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            e.Handled= true;
            try
            {
                myCart = bl.Cart.AddToCart(myCart, Convert.ToInt32((sender as Button).Tag.ToString()));

                orderItemsListView.ItemsSource = myCart.Items;
                orderItemsListView.Items.Refresh();
                txtTotalPCart.Text = "Total:" + myCart.TotalPrice.ToString() + "$";

            }
            catch (BO.BlOutOfStockException ex)
            {
                (sender as Button).IsEnabled = false;
            }
            
        }

        

       

        //private void btnAdd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{

        //    myCart = bl.Cart.AddToCart(myCart, Convert.ToInt32((sender as Button).Tag.ToString()));

        //    orderItemsListView.ItemsSource = myCart.Items;
        //    orderItemsListView.Items.Refresh();


        //}

        //private void btnLow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    myCart = bl.Cart.AddToCart(myCart, Convert.ToInt32((sender as Button).Tag.ToString()));

        //    orderItemsListView.ItemsSource = myCart.Items;
        //    orderItemsListView.Items.Refresh();
        //    txtTotalPCart.Text = myCart.TotalPrice.ToString();
        //}

        private void btnLow_Click(object sender, RoutedEventArgs e)
        {

            BO.OrderItem item = (BO.OrderItem)(sender as Button).DataContext;
            bl.Cart.UpdateProductInCart(myCart, item.Amount-1, item.ProductID);
            orderItemsListView.ItemsSource = myCart.Items;
            orderItemsListView.Items.Refresh();
            txtTotalPCart.Text = "Total:"+myCart.TotalPrice.ToString()+"$";


        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bl.Cart.UpdateProductInCart(myCart , 0, Convert.ToInt32((sender as TextBlock).Tag.ToString()));
            orderItemsListView.ItemsSource = myCart.Items;
            orderItemsListView.Items.Refresh();
            txtTotalPCart.Text = "Total:" + myCart.TotalPrice.ToString() + "$";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grid1.Visibility = Visibility.Visible ;
            (sender as Button).Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bl.Cart.OrderCreate(myCart);
        }
    }
}
