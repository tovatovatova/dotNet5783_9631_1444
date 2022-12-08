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

           
            //ProductSelector.Items.DeferRefresh();
            // ProductSelector.ItemsSource=Enum.GetValues(typeof(BO.Category));
        }

        //ProductSelector.ItemsSource=cat/*Enum.GetValues(typeof(BO.Category))*/;
        //ComboBoxItem returnAllItem=new ComboBoxItem();
        //returnAllItem.Content = "All Product";
        //ProductSelector.Items.Add(returnAllItem);


        //private void btnUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    ProductWindow p = new ProductWindow();
        //    p.ShowDialog();
        //    ProductSelector.SelectedItem = ProductSelector.Items.GetItemAt(0);
        //    // ProductListView.ItemsSource = bl.Product.GetProductList();

        //}




        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductWindow p = new ProductWindow(sender, e, ((BO.ProsuctForList)ProductListView.SelectedItem).ID);
            p.ShowDialog();
            ProductSelector.SelectedItem = ProductSelector.Items.GetItemAt(0);
        }

        private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (ProductSelector.SelectedItem == ProductSelector.Items.GetItemAt(0))
            {
                ProductListView.ItemsSource = bl.Product.GetProductList();

            }

            else
            {
                //var list = from item in bl.Product.GetProductList()
                //           where item.Category == Enum.Parse<BO.Category>(ProductSelector.SelectedItem.ToString())
                //           select item;
                //ProductListView.ItemsSource = list;
                ProductListView.ItemsSource = bl.Product.GetListedListByCategory(item => item.Category == Enum.Parse<BO.Category>(ProductSelector.SelectedItem.ToString()));




            }



        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           Image image = new Image();
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "pic2.png";
            // image.Source = new Uri("C: \Users\tovar\source\repos\tovatovatova\dotNet5783_9631_1444\PL\pic2.png")
           image.Source = new BitmapImage(new Uri(path));
            //btnAdd.Background = Brushes(image);
            //btnAdd.Background = image.Source("pic2.jpg");
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
