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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {


        public BO.OrderTracking? PlOrderTracking
        {
            get { return (BO.OrderTracking?)GetValue(PlOrderTrackingProperty); }
            set { SetValue(PlOrderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlOrderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlOrderTrackingProperty =
            DependencyProperty.Register("PlOrderTracking", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));


        public OrderTrackingWindow(object sender, EventArgs e, string id)
        {
            InitializeComponent();
            PlOrderTracking = new BO.OrderTracking();

        }

    }
}
