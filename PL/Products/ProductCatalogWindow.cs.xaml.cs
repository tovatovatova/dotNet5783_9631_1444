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
       BO. Cart cart = new BO.Cart();

        //public List<BO.OrderForList?> PlOrder
        //{
        //    get { return (List<BO.OrderForList?>)GetValue(PlOrderProperty); }
        //    set { SetValue(PlOrderProperty, value); }//@#$%^&*()_(*&UY^T%R$#$%^&*(
        //} 
        public List<BO.ProductItem?> myProductCat
        {
            get { return (List<BO.ProductItem?>)GetValue(myProductCatProperty); }
            set { SetValue(myProductCatProperty, value); }
        }
        // Using a DependencyProperty as the backing store for PlProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProductCatProperty =
            DependencyProperty.Register("myProductCat", typeof(List<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));
        prodAndCart myDataC = new prodAndCart();




        public ProductCatalogWindow()
        {
            InitializeComponent();
            if (cart == null || cart.Items == null)
                txtAmountInCart.Text = "0";
            else
                txtAmountInCart.Text = cart.Items.Count().ToString();
            // listViewProducts.ItemsSource = bl.Product.GetCatalog().ToList();
            myProductCat =new List<BO.ProductItem>();
            myProductCat = bl.Product.GetCatalog().ToList();
            prodAndCart myDataC = new prodAndCart();
            myDataC.products = myProductCat;
            myDataC.cart = cart;

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
            if (cart==null||cart.Items == null)
                txtAmountInCart.Text = "0";
            else
                txtAmountInCart.Text = cart.Items.Count().ToString();
               
        }
    }
    //class BooleanToVisibilityConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return (bool)value ? Visibility.Hidden : Visibility.Visible;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return System.Convert.ToInt32(value) < 15 ? false : true;
    //    }
    //}
    public class prodAndCart
    {
        public List< BO.ProductItem> products { get; set; }
        public BO.Cart cart { get; set; }
    }
}
