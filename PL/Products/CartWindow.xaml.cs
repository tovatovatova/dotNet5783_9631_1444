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

        public AllContext allContext
        {
            get { return (AllContext)GetValue(allContextProperty); }
            set { SetValue(allContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for allContext.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty allContextProperty =
            DependencyProperty.Register("allContext", typeof(AllContext), typeof(Window), new PropertyMetadata(null));


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
           allContext= new AllContext();
            allContext.cart = cart;
            allContext.orderItems= new List<BO.OrderItem>();
            allContext.orderItems = cart.Items.ToList();
            InitializeComponent();
          //  if (myCart.TotalPrice == 0)  emptyCart.Visibility = Visibility.Visible : emptyCart.Visibility = Visibility.Hidden;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            e.Handled= true;
            try
            {
                myCart = bl.Cart.AddToCart(myCart, Convert.ToInt32((sender as Button).Tag.ToString()));

                orderItemsListView.ItemsSource = myCart.Items;
                orderItemsListView.Items.Refresh();
            }
            catch(BO.BlOutOfStockException ex)
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

        private void btnLow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            myCart = bl.Cart.AddToCart(myCart, Convert.ToInt32((sender as Button).Tag.ToString()));

            orderItemsListView.ItemsSource = myCart.Items;
            orderItemsListView.Items.Refresh();
        }

        private void btnLow_Click(object sender, RoutedEventArgs e)
        {

            BO.OrderItem item = (BO.OrderItem)(sender as Button).DataContext;
            bl.Cart.UpdateProductInCart(allContext.cart, item.Amount-1, item.ProductID);
            orderItemsListView.ItemsSource = myCart.Items;
            orderItemsListView.Items.Refresh();
            myCart.TotalPrice = 0;
            
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bl.Cart.UpdateProductInCart(allContext.cart, 0, Convert.ToInt32((sender as TextBlock).Tag.ToString()));
            orderItemsListView.ItemsSource = myCart.Items;
            orderItemsListView.Items.Refresh();
        }
    }
}
public class AllContext
{
    public List<BO.OrderItem> orderItems { get; set; }
    public BO.Cart cart { get; set; }
  
}