
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>

    public partial class ProductListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        /// <summary>
        /// initialize product list window -combo box options and list view
        /// </summary>
        public ProductListWindow()
        {

            InitializeComponent();
            // ProductListView.Items.Clear();
            ProductListView.ItemsSource = (bl.Product.GetProductList().ToList().OrderByDescending(x => x.Price));
            ;//see all products
            ProductSelector.Items.Add("All products");//add option for combo box of seeing all the products

            ProductSelector.SelectedItem = ProductSelector.Items.GetItemAt(0);//all products
            
            foreach (var item in Enum.GetValues(typeof(BO.Category)))//add the products' category to combo box
            {
                ProductSelector.Items.Add(item);
            }

        }
        /// <summary>
        /// when pressing a product a product window file will be open with option of adding or updating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductListView.SelectedIndex==-1)
                return;
            ProductWindow p = new ProductWindow(sender, e, ((BO.ProsuctForList)ProductListView.SelectedItem).ID);//send the selected product id
            p.ShowDialog();
            ProductSelector.SelectedItem = ProductSelector.Items.GetItemAt(0);
            ProductListView.ItemsSource= bl.Product.GetProductList();//can show all the products back again-after updating
        }
        /// <summary>
        /// when pressing an item in the combo box the products will be shown with filter-the chosen category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductSelector.SelectedItem == ProductSelector.Items.GetItemAt(0))//all product option
            {
                ProductListView.ItemsSource = bl.Product.GetProductList().OrderBy(x=>x.Price);
                return;
            }

            else
            {
                
                ProductListView.ItemsSource = bl.Product.GetListedListByFilter(item => item.Category == Enum.Parse<BO.Category>(ProductSelector.SelectedItem.ToString())).OrderBy(x=>x.Price);//sort product list view by category

            }
        }
        /// <summary>
        /// when pressing the add buttum product window will be shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            ProductWindow p = new ProductWindow();

            p.ShowDialog();
            ProductListView.ItemsSource = bl.Product.GetProductList();//refreshing product list view

        }

        
    }
}

