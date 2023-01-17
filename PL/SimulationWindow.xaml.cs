using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        BackgroundWorker updateStatus;
        bool flag = true;
    DateTime fakeTime= DateTime.Now;
        public List<BO.OrderForList?> SimulationOrders
        {
            get { return (List<BO.OrderForList?>)GetValue(SimulationOrdersProperty); }
            set { SetValue(SimulationOrdersProperty, value); }//@#$%^&*()_(*&UY^T%R$#$%^&*(
        }

        // Using a DependencyProperty as the backing store for PlOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SimulationOrdersProperty =
            DependencyProperty.Register(" SimulationOrders", typeof(List<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));

        public SimulationWindow()
        {
           
            SimulationOrders = new List<BO.OrderForList?>();
                     SimulationOrders=bl.Order.GetOrderList().ToList();
            InitializeComponent();
            updateStatus= new BackgroundWorker();
            updateStatus.DoWork += UpdateStatus_DoWork;
            updateStatus.ProgressChanged += UpdateStatus_ProgressChanged;
            updateStatus.RunWorkerCompleted += UpdateStatus_RunWorkerCompleted;
            updateStatus.WorkerReportsProgress = true;
            updateStatus.WorkerSupportsCancellation = true;
        }
        private void UpdateStatus_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (flag)
            {
                if (updateStatus.CancellationPending == true)
                {
                    e.Cancel= true;
                    break;
                }
                else
                {
                    Thread.Sleep(2000);
                    fakeTime = fakeTime.AddHours(3);
                    if(updateStatus.WorkerReportsProgress== true)
                    {
                        updateStatus.ReportProgress(11);
                    }
                }
            }
        }
        //foreach (var item in ordListTemp)
        //    {
        //        BO.Order orderSimulator = bl.Order.RequestOrderDeta(item?.ID ?? throw new NullReferenceException());
        //        if (timeSim - orderSimulator.OrderDate >= new TimeSpan(3, 0, 0, 0) && orderSimulator.Status == BO.OrderStatus.Ordered)
        //            bl.Order.UpdateSendOrder(orderSimulator.ID);//, timeSim);
        //        if (timeSim - orderSimulator.OrderDate >= new TimeSpan(10, 0, 0, 0) && orderSimulator.Status == BO.OrderStatus.Shipped)
        //            bl.Order.UpdateSupplyOrder(orderSimulator.ID);//, timeSim);

        //    }



        private void UpdateStatus_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            
            MessageBoxResult result = MessageBox.Show(" 😍", "nbb", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
          
        }

        private void UpdateStatus_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {

            foreach (var item in SimulationOrders)
            {
                BO.Order order = bl.Order.GetOrderByID(item?.ID ?? throw new NullReferenceException());
                if (fakeTime - order.OrderDate >= new TimeSpan(2, 0, 0, 0) && order.Status == BO.OrderStatus.Ordered)
                    bl.Order.UpdateShip(order.Id);
                if (fakeTime - order.OrderDate >= new TimeSpan(5, 0, 0, 0) && order.Status == BO.OrderStatus.Shipped)
                    bl.Order.UpdateDelivery(order.Id);
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (updateStatus.IsBusy != true)
            {
                this.Cursor = Cursors.Wait;
               updateStatus.RunWorkerAsync(11);
            }
        }
    }
}
