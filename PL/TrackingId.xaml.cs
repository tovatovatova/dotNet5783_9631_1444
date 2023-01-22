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
    /// Interaction logic for TrackingId.xaml
    /// </summary>
    public partial class TrackingId : Window
    {
        private int orderID
        {
            get { return (int)GetValue(orderIDProperty); }
            set { SetValue(orderIDProperty, value); }
        }
        public static readonly DependencyProperty orderIDProperty =
            DependencyProperty.Register("orderID", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));
        BlApi.IBl bl = BlApi.Factory.Get();


        public TrackingId()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    BO.Order o= bl.Order.GetOrderByID(orderID);
                    OrderTrackingWindow orderTrackingWindow = new OrderTrackingWindow(sender, e, orderID);
                    orderTrackingWindow.Show();
                }
                catch (BO.BlInvalidInputException )
                {
                    MessageBox.Show("wrong Id ☹");
                }
                catch (BO.BlIdDoNotExistException )//order doesnt exist
                {//throw an error message box 
                    MessageBox.Show("wrong Id ☹");
                }
            }
        }
    }
}
