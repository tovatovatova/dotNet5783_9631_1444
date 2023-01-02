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
        public List<BO.ProductItem?> myProd
        {
            get { return (List<BO.ProductItem?>)GetValue(myProdProperty); }
            set { SetValue(myProdProperty, value); }
        }
        // Using a DependencyProperty as the backing store for PlProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProdProperty =
            DependencyProperty.Register("myProd", typeof(List<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));


       

        public ProductCatalogWindow()
        {
            InitializeComponent();
           listViewProducts.ItemsSource = bl.Product.GetCatalog().ToList();
            
            
        }

        private void listViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (listViewProducts.SelectedIndex == -1)
                return;
            ProductWindow p = new ProductWindow(sender, e, ((BO.ProductItem)listViewProducts.SelectedItem).ID);//send the selected product id
            p.ShowDialog();
           
        }
    }
}
