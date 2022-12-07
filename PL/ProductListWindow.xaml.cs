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
            ProductListView.ItemsSource=bl.Product.GetProductList();
            //IEnumerable<string?> cat = Enum.GetNames(typeof(BO.Category));
            //cat.Append("All Products");
            ProductSelector.Items.Add(BO.Category.Cakes.ToString());
            ProductSelector.Items.Add("Donats");
            ProductSelector.Items.Add("GiftBoxes");
            ProductSelector.Items.Add("Desserts");
            ProductSelector.Items.Add("Specials");
            ProductSelector.Items.Add("All Products");
        
           //ProductSelector.ItemsSource=cat/*Enum.GetValues(typeof(BO.Category))*/;
           //ComboBoxItem returnAllItem=new ComboBoxItem();
           //returnAllItem.Content = "All Product";
           //ProductSelector.Items.Add(returnAllItem);

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow p = new ProductWindow(e);
            p.ShowDialog();
            ProductSelector.SelectedItem = ProductSelector.Items.GetItemAt(0);
           // ProductListView.ItemsSource = bl.Product.GetProductList();

        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
          //  ProductListView.UnselectAll();
        }
        private void ProductListView_SelectionChanged( SelectionChangedEventArgs e)
        {
            
        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductWindow p = new ProductWindow(sender, e, (BO.ProsuctForList)ProductListView.SelectedItem);
            p.Show();
        }
    }
}
