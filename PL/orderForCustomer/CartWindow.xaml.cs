﻿using BlApi;
using BO;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>

    public partial class CartWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
          public BO.Cart myCart
        {
            get { return (BO.Cart)GetValue(myCartProperty); }
            set { SetValue(myCartProperty, value); }
        }
        public static readonly DependencyProperty myCartProperty =
            DependencyProperty.Register("myCart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));


        public BO.User? myUser
        {
            get { return (BO.User?)GetValue(myUserProperty); }
            set { SetValue(myUserProperty, value); }
        }

        // Using a DependencyProperty as the backing store for myUser.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myUserProperty =
            DependencyProperty.Register("myUser", typeof(BO.User), typeof(Window), new PropertyMetadata(null));




        public CartWindow(Cart cart)
        {
            myCart = cart;
            InitializeComponent();

        }
        public CartWindow(Cart cart, User user)//showing cart of submited user
        {
            myCart = cart;
            myUser = user;
            InitializeComponent();

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            e.Handled = true;
            try
            {
                myCart = bl.Cart.AddToCart(myCart, Convert.ToInt32((sender as Button).Tag.ToString()));

                orderItemsListView.ItemsSource = myCart.Items;
                orderItemsListView.Items.Refresh();
                myCart.TotalPrice = myCart.TotalPrice;
                myCart = myCart;
                txtTotalPCart.Text = "Total:" + myCart.TotalPrice.ToString() + "$";

            }
            catch (BO.BlOutOfStockException)
            {
                (sender as Button).IsEnabled = false;
            }

        }







        private void btnLow_Click(object sender, RoutedEventArgs e)
        {

            BO.OrderItem item = (BO.OrderItem)(sender as Button).DataContext;
           myCart=bl.Cart.UpdateProductInCart(myCart, item.Amount - 1, item.ProductID);
            orderItemsListView.ItemsSource = myCart.Items;
            orderItemsListView.Items.Refresh();
            txtTotalPCart.Text = "Total:" + myCart.TotalPrice.ToString() + "$";


        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bl.Cart.UpdateProductInCart(myCart, 0, Convert.ToInt32((sender as TextBlock).Tag.ToString()));
            orderItemsListView.ItemsSource = myCart.Items;
            orderItemsListView.Items.Refresh();
            txtTotalPCart.Text = "Total:" + myCart.TotalPrice.ToString() + "$";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (myCart != null && myCart.Items.Count() != 0) { }
            {

                grid1.Visibility = Visibility.Visible;
                (sender as Button).IsEnabled = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);
            if (myCart.CustomerAddress == null || myCart.CustomerEmail == null || myCart.CustomerName == null || !re.IsMatch(myCart.CustomerEmail))
            {
                string messageBoxText = "wrong details try again";
                string caption = "error";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    return;
                }
            }
            else
            {
                try
                {
                    (sender as Button).Visibility = Visibility.Hidden;
                    checkuot.Visibility = Visibility.Hidden;
                    int id = bl.Cart.OrderCreate(myCart);
                    string messegeBoxText = @"Your order has been successfully placed
      Order Number: " + id;
                    string caption = " ";
                    MessageBoxImage icon = MessageBoxImage.Information;
                    MessageBoxResult result;
                    result = MessageBox.Show(messegeBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        myCart.Items = null;

                        ProductCatalogWindow p = new ProductCatalogWindow(myUser, myCart);
                        p.ShowDialog();
                        Close();
                    }
                }
                catch (BO.BlIdDoNotExistException)
                {
                    MessageBox.Show("something went wrong", "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
                catch (BO.BlInvalidInputException ex)
                {
                    MessageBox.Show("wrong details try again", "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
                catch (BO.BlNullPropertyException ex)
                {
                    MessageBox.Show(ex.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ProductCatalogWindow p = new ProductCatalogWindow(myUser, myCart);
            Close();
            p.Show();
        }

    }
}
