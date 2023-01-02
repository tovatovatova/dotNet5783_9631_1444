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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {

        BlApi.IBl bl = BlApi.Factory.Get();
        Cart cart = new Cart();
        public BO.ProductItem myProduct
        {
            get { return (BO.ProductItem)GetValue(myProductProperty); }
            set { SetValue(myProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProductProperty =
            DependencyProperty.Register("myProduct", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));


        public ProductItemWindow(Cart cart,int id)
        {
            InitializeComponent();
            cart = cart;
            myProduct = bl.Product.GetProductByID(cart,id);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
                bl.Cart.AddToCart(cart, myProduct.ID);}
        }
    }

