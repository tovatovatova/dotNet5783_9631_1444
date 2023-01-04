using BO;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
   
    public partial class CartWindow : Window
    {


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
            return;
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Button).Content == "+")
            {
                ((OrderItem)orderItemsListView.SelectedItem).Amount++;
            }
        }

    }
}
public class AllContext
{
    public List<BO.OrderItem> orderItems { get; set; }
    public BO.Cart cart { get; set; }
}