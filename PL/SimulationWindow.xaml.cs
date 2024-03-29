﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
       private BlApi.IBl bl = BlApi.Factory.Get();
        BackgroundWorker updateStatus;
        bool flag = true;
    DateTime fakeTime= DateTime.Now;
        public List<BO.OrderForList?> SimulationOrders
        {
            get { return (List<BO.OrderForList?>)GetValue(SimulationOrdersProperty); }
            set { SetValue(SimulationOrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SimulationOrdersProperty =
            DependencyProperty.Register("SimulationOrders", typeof(List<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));

        public SimulationWindow()
        {
            InitializeComponent();
            SimulationOrders = new(bl.Order.GetOrderList());
            updateStatus = new BackgroundWorker();
            updateStatus.DoWork += UpdateStatus_DoWork;
            updateStatus.ProgressChanged += UpdateStatus_ProgressChanged;
            updateStatus.RunWorkerCompleted += UpdateStatus_RunWorkerCompleted;
            updateStatus.WorkerReportsProgress = true;
            updateStatus.WorkerSupportsCancellation = true;

        }
        private void UpdateStatus_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (updateStatus.CancellationPending == true)
                {
                    e.Cancel= true;
                    break;
                }
                else
                {
                    fakeTime = fakeTime.AddHours(3);
                    if(updateStatus.WorkerReportsProgress== true)
                    {
                        updateStatus.ReportProgress(11117);
                    }
                }
                Thread.Sleep(2000);
            }
        }
       


        private void UpdateStatus_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {

            if (flag == true)
            {
                MessageBox.Show("finish😍");
            }
            else if (e.Cancelled == true)
            {
                MessageBox.Show("cancled");
            }
            this.Cursor = Cursors.Arrow;
        }

        private void UpdateStatus_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            Random rn = new Random();
            List<BO.OrderForList?> temp = bl.Order.GetOrderList().ToList();
            foreach (var item in SimulationOrders)
            {
                BO.Order order = bl.Order.GetOrderByID(item?.ID ?? throw new NullReferenceException());
                if (fakeTime - order.OrderDate >= new TimeSpan(3, 0, 0, 0) && order.Status == BO.OrderStatus.Ordered)
                    bl.Order.UpdateShip(order.Id);
                if (fakeTime - order.OrderDate >= new TimeSpan(3, 0, 0, 0) && order.Status == BO.OrderStatus.Shipped)
                    bl.Order.UpdateDelivery(order.Id);
                SimulationOrders = bl.Order.GetOrderList().ToList();
             
            }
         
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (updateStatus.IsBusy != true)
            {
               //this.Cursor = Cursors.Wait;
                updateStatus.RunWorkerAsync();
            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            if (updateStatus.WorkerSupportsCancellation == true)
                updateStatus.CancelAsync(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string idd = ((sender as Button).Tag).ToString();
            int id = Convert.ToInt32(idd);
            MessageBox.Show(bl.Order.OrderTracking(id).ToString()+"📦");
        }
    }
}
