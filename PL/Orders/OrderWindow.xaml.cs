﻿using System;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        BlApi.IBl bl = BlApi.Factory.Get();
        public BO.Order? PlOrder
        {
            get { return (BO.Order?)GetValue(PlOrderProperty); }
            set { SetValue(PlOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlOrderProperty =
            DependencyProperty.Register("PlOrder", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));



        public List<BO.OrderItem?> PlOrderItem
        {
            get { return (List<BO.OrderItem?>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(List<BO.OrderItem?>), typeof(Window), new PropertyMetadata(null));


        public OrderWindow()
        {
            InitializeComponent();
            PlOrder=new BO.Order();
            PlOrderItem = new List<BO.OrderItem?>();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));

        }
        public OrderWindow(object sender, EventArgs e, int id)
        {
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            InitializeComponent();
            try
            {
                PlOrder = bl.Order.GetOrderByID(id);
            }
            catch (BO.BlIdDoNotExistException ex)
            {

                //throw an error message box 
                string messageBoxText = ex.Message.ToString();
                string caption = "error";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
            }
            PlOrderItem = PlOrder.Items.ToList();
        }
    }
}
