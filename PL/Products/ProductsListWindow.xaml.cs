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
    /// Interaction logic for ProductsListWindow.xaml
    /// </summary>
    public partial class ProductsListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public List<BO.ProsuctForList?> myProd
        {
            get { return (List<BO.ProsuctForList?>)GetValue(myProdProperty); }
            set { SetValue(myProdProperty, value); }
        }
        // Using a DependencyProperty as the backing store for PlProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProdProperty =
            DependencyProperty.Register("myProd", typeof(List<BO.ProsuctForList?>), typeof(Window), new PropertyMetadata(null));

        public ProductsListWindow()
        {
            InitializeComponent();
            myProd = bl.Product.GetProductList().ToList();
            cmbCategory.Items.Add("All Products");
            foreach (var item in Enum.GetValues(typeof(BO.Category)))//add the products' category to combo box
            {
                cmbCategory.Items.Add(item);
            }
            cmbCategory.SelectedIndex = 0;
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategory.SelectedIndex == 0)
            {
                myProd = bl.Product.GetProductList().ToList();
                return;
            }
               
            myProd = bl.Product.GetListedListByFilter(item => item.Category == Enum.Parse<BO.Category>(cmbCategory.SelectedItem.ToString())).ToList();

           
        }

        private void prosuctForListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (prosuctForListDataGrid.SelectedIndex == -1)
                return;
            ProductWindow p = new ProductWindow(sender, e, ((BO.ProsuctForList)prosuctForListDataGrid.SelectedItem).ID);//send the selected product id
            p.ShowDialog();
            cmbCategory.SelectedItem = cmbCategory.Items.GetItemAt(0);
            myProd = bl.Product.GetProductList().ToList();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new ProductWindow();
            p.ShowDialog();
            cmbCategory.SelectedItem = cmbCategory.Items.GetItemAt(0);
            myProd = bl.Product.GetProductList().ToList();
        }
    }
}
