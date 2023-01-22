using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
       
      //  private ObservableCollection List<Tuple<DateTime, string>> tracking = new ObservableCollection List<Tuple<DateTime?, string>>>();


//add items to the collection …
//this.DataContext = _myCollection;


       private BO.OrderTracking? orderTracking
        {
            get { return (BO.OrderTracking?)GetValue(orderTrackingProperty); }
            set { SetValue(orderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlOrderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderTrackingProperty =
            DependencyProperty.Register("orderTracking", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));
             BlApi.IBl bl = BlApi.Factory.Get();


   
        public OrderTrackingWindow(object sender, EventArgs e, int id)
        {
            InitializeComponent();
            orderTracking = bl.Order.OrderTracking(id);
       //     tracking = OrderTracking.Tracking!;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OrderDetailsWindow o = new OrderDetailsWindow(sender,e,orderTracking.ID);
            o.ShowDialog();
        }
    }
}
