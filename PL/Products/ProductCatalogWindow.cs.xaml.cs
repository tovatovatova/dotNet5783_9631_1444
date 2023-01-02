using BO;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductCatalogWindow.xaml
    /// </summary>
    public partial class ProductCatalogWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        Cart cart = new Cart();

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


       

        public ProductCatalogWindow()
        {
            InitializeComponent();
          // listViewProducts.ItemsSource = bl.Product.GetCatalog().ToList();
            myProductCat=new List<BO.ProductItem>();
            myProductCat = bl.Product.GetCatalog().ToList();
            
        }

        private void listViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (listViewProducts.SelectedIndex == -1)
                return;
        ProductItemWindow p=new ProductItemWindow(cart, ((ProductItem)listViewProducts.SelectedItem).ID);
            p.ShowDialog();
        }
    }
    class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value) < 15 ? false : true;
        }
    }
}
