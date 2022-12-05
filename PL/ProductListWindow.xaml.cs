using BlApi;
using BlImplementation;
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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl = new Bl();

        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource=bl.Product.GetProductList();
            ProductSelector.ItemsSource=Enum.GetValues(typeof(BO.Category));
        }

        private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = from item in bl.Product.GetProductList()
                       where item.Category == Enum.Parse<BO.Category>(ProductSelector.SelectedItem.ToString())
                       select item;
            ProductListView.ItemsSource=list;
            //ProductListView.ItemsSource = bl.Product.GetProductListByCategory(item => item.Category == ((BO.Category)(ProductSelector.SelectedItem)));
        }
    }
}
