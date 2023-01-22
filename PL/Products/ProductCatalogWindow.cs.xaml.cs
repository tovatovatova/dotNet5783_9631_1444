
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ProductCatalogWindow.xaml
    /// </summary>
    public partial class ProductCatalogWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
       

        public BO.Cart cart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));

        public List<BO.ProductItem?> myProductCat
        {
            get { return (List<BO.ProductItem?>)GetValue(myProductCatProperty); }
            set { SetValue(myProductCatProperty, value); }
        }
        // Using a DependencyProperty as the backing store for PlProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProductCatProperty =
            DependencyProperty.Register("myProductCat", typeof(List<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));
        public int amount
        {
            get { return (int)GetValue(amountProperty); }
            set { SetValue(amountProperty, value); }
        }
        public static readonly DependencyProperty amountProperty =
            DependencyProperty.Register("amount", typeof(int), typeof(Window), new PropertyMetadata(0));
       

        public BO.User? user
        {
            get { return (BO.User?)GetValue(userProperty); }
            set { SetValue(userProperty, value); }
        }

        // Using a DependencyProperty as the backing store for user.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty userProperty =
            DependencyProperty.Register("user", typeof(BO.User), typeof(Window), new PropertyMetadata(null));





        public ProductCatalogWindow(BO.User u, BO.Cart Oldcart = null)
        {
           
            if (Oldcart == null)
            {
                cart = new BO.Cart();
            }
            else
            {
                cart = Oldcart;
            }
           
            user = u;
            if (cart.Items == null)
            {
                cart.Items = new List<BO.OrderItem>();
            }
            InitializeComponent();
            myProductCat = new List<BO.ProductItem>();
            myProductCat = bl.Product.GetCatalog().ToList();
            amount = cart.Items.Count();


        }

        private void listViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (listViewProducts.SelectedIndex == -1)
                return;
        ProductItemWindow p=new ProductItemWindow(cart, ((BO.ProductItem)listViewProducts.SelectedItem).ID);
            p.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (cart == null || cart.Items.Count() == 0 || cart.Items == null)
                amount = 0;
            else
                amount = cart.Items.Count();
               
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CartWindow c;
            if (user != null)//user in system
            {
                c = new CartWindow(cart, user);
                Close();
                c.Show();
            }
            else
            {
             c=new CartWindow( cart);
                Close();
                c.Show();
            }
            
            
            
        }

        private void cxbSortByCategory_Checked(object sender, RoutedEventArgs e)
        {
            myProductCat = bl.Product.getByGrouping().ToList();
        }

        private void cxbSortByCategory_Unchecked(object sender, RoutedEventArgs e)
        {
            myProductCat=bl.Product.GetCatalog().ToList();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            myProductCat = bl.Product.GetCatalog().ToList().Where(item => item.Category == Enum.Parse<BO.Category>((sender as TextBlock).Text.ToString())).ToList();
        }
    }
    
}
