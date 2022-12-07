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
            ProductSelector.Items.Add("All products");
            foreach (var item in Enum.GetValues(typeof(BO.Category)))
            {
                ProductSelector.Items.Add(item);
            }
           // ProductSelector.ItemsSource=Enum.GetValues(typeof(BO.Category));
        }

        private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            var list = from item in bl.Product.GetProductList()
                       where item.Category == Enum.Parse<BO.Category>(ProductSelector.SelectedItem.ToString())
                       select item;
            ProductListView.ItemsSource=list;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow p=new ProductWindow(e);
            p.Show();
        }

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
