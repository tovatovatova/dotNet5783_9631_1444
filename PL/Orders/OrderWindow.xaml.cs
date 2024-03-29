﻿using BO;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        BlApi.IBl bl = BlApi.Factory.Get();


        public BO.Order? MyOrder
        {
            get { return (BO.Order?)GetValue(MyOrderProperty); }
            set { SetValue(MyOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyOrderProperty =
            DependencyProperty.Register("MyOrder", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

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
            MyOrder = new BO.Order();
            PlOrderItem = new List<BO.OrderItem?>();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            //statusComboBox.SelectedItem = "All Orders";
        }
        public OrderWindow(object sender, EventArgs e, int id)
        {
            InitializeComponent();
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
            
            try
            {
                MyOrder = bl.Order.GetOrderByID(id);
            }
            catch (BO.BlIdDoNotExistException ex)
            {
               MessageBox.Show(ex.Message.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }
            PlOrderItem = MyOrder.Items.ToList();
            statusComboBox.SelectedItem = MyOrder.Status;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {//#$%^&*()_(*&^%
            try
            {
                MyOrder=bl.Order.UpdateOrder(MyOrder);
               // PlOrderItem = MyOrder.Items.ToList();
            }
            catch (BO.BlIdDoNotExistException ex)
            {
                 MessageBox.Show(ex.Message.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch(BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.ToString() + " \n check your input", "", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            Close();

        }

        private void btnUpdateDelivary_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyOrder = bl.Order.UpdateDelivery(MyOrder.Id);//tries to update delivary
            }
            catch (BO.BlIdDoNotExistException ex)//order id doesnt exist 
            {
                MessageBox.Show(ex.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (BO.BlIncorrectDateException ex)//worng date
            {
                MessageBox.Show(ex.message.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.ToString() + " \n check your input", "", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            Close();
        }

        private void btnUpdateShipping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyOrder = bl.Order.UpdateShip(MyOrder.Id);
            }
            catch (BO.BlIdDoNotExistException ex)//order id doesnt exist 
            {
                MessageBox.Show(ex.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (BO.BlIncorrectDateException ex)//worng date
            {
                MessageBox.Show(ex.message.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (BO.BlNullPropertyException ex)
            {
                MessageBox.Show(ex.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            catch (BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.ToString() + " \n check your input", "", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            //could update 
            Close();
        }
    }
}
