using BlApi;
using BlImplementation;
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
        private IBl bl = new Bl();

        public ProductListWindow()
        {

            InitializeComponent();
            ProductListView.Items.Clear();
            ProductListView.ItemsSource = bl.Product.GetProductList();
            ProductSelector.Items.Add("All products");

            ProductSelector.SelectedItem = ProductSelector.Items.GetItemAt(0);
            foreach (var item in Enum.GetValues(typeof(BO.Category)))
            {
                ProductSelector.Items.Add(item);
            }

        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductWindow p = new ProductWindow(sender, e, ((BO.ProsuctForList)ProductListView.SelectedItem).ID);
            p.ShowDialog();
            ProductSelector.SelectedItem = ProductSelector.Items.GetItemAt(0);
            ProductListView.ItemsSource= bl.Product.GetProductList();
        }






        private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductSelector.SelectedItem == ProductSelector.Items.GetItemAt(0))
            {
                ProductListView.ItemsSource = bl.Product.GetProductList();
                return;
            }

            else
            {
                //var list = from item in bl.Product.GetProductList()
                //           where item.Category == Enum.Parse<BO.Category>(ProductSelector.SelectedItem.ToString())
                //           select item;
                //ProductListView.ItemsSource = list;
                ProductListView.ItemsSource = bl.Product.GetListedListByFilter(item => item.Category == Enum.Parse<BO.Category>(ProductSelector.SelectedItem.ToString()));

            }
        }
       




        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            ProductWindow p = new ProductWindow();

            p.ShowDialog();
            ProductListView.ItemsSource = bl.Product.GetProductList();

        }

        private void ProductSelector_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ProductSelector.SelectedItem == ProductSelector.Items.GetItemAt(0))
            {
                ProductListView.ItemsSource = bl.Product.GetProductList();

            }
            else
            {
                var list = from item in bl.Product.GetProductList()
                           where item.Category == Enum.Parse<BO.Category>(ProductSelector.SelectedItem.ToString())
                           select item;
                ProductListView.ItemsSource = list;
            }
        }

        
    }
}
