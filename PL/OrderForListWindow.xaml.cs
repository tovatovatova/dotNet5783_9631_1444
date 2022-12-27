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
    /// Interaction logic for OrderForListWindow.xaml
    /// </summary>
    public partial class OrderForListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();

        public List<BO.OrderForList?> PlOrder
        {
            get { return (List<BO.OrderForList?>)GetValue(PlOrderProperty); }
            set { SetValue(PlOrderProperty, value); }//@#$%^&*()_(*&UY^T%R$#$%^&*(
        }

        // Using a DependencyProperty as the backing store for PlOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlOrderProperty =
            DependencyProperty.Register("PlOrder", typeof(BO.OrderForList), typeof(Window), new PropertyMetadata(null));
        public OrderForListWindow()
        {
            InitializeComponent();
            PlOrder = bl.Order.GetOrderList().ToList();
            orderForListView.ItemsSource = PlOrder.OrderByDescending(var => var.TotalPrice);
            cmbStatus.Items.Add("All Orders");
            cmbStatus.SelectedItem=cmbStatus.Items.GetItemAt(0);//defult for all orders
            foreach (var item in Enum.GetValues(typeof(BO.OrderStatus)))//add the order's status' category to combo box
            {
                cmbStatus.Items.Add(item);
            }


        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow order = new OrderWindow();
            order.ShowDialog();
            orderForListView.ItemsSource = bl.Order.GetOrderList();
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)//filterig list by the choosen category 
        {
            if (cmbStatus.SelectedItem == cmbStatus.Items.GetItemAt(0))//all product option
            {
                orderForListView.ItemsSource = bl.Order.GetOrderList();
                return;
            }

            else
            {
                orderForListView.ItemsSource = bl.Order.GetListedListByFilter(item => item.Status == Enum.Parse<BO.OrderStatus>(cmbStatus.SelectedItem.ToString()));//sort order list view by category
            }
        }

        private void orderForListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (cmbStatus.SelectedIndex == -1)
                return;
            ProductWindow p = new ProductWindow(sender, e, ((BO.ProsuctForList)orderForListView.SelectedItem).ID);//send the selected product id
            p.ShowDialog();
            cmbStatus.SelectedItem = cmbStatus.Items.GetItemAt(0);
            cmbStatus.ItemsSource = bl.Product.GetProductList();//can show all the products back again-after updating
        }
    }
}
