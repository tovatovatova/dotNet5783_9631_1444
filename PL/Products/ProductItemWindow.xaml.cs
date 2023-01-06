
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
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {

        BlApi.IBl bl = BlApi.Factory.Get();
        BO.Cart myCart = new BO.Cart();
        public BO.ProductItem myProduct
        {
            get { return (BO.ProductItem)GetValue(myProductProperty); }
            set { SetValue(myProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProductProperty =
            DependencyProperty.Register("myProduct", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));


        public ProductItemWindow(BO.Cart cart1, int id)
        {
            InitializeComponent();
            myCart = cart1;
            myProduct = bl.Product.GetProductByID(myCart, id);

        }

        
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                 bl.Cart.AddToCart(myCart, myProduct.ID);
                 Close();
            }
            catch (BO.BlOutOfStockException ex)
            {
                string messageBoxText = ex.Entity.ToString();
                string caption = " ";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                Close();
            }
            

        }
    }
}

