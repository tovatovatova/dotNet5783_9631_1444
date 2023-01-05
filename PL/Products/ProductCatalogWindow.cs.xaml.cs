﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ProductCatalogWindow.xaml
    /// </summary>
    public partial class ProductCatalogWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
       BO. Cart cart = new BO.Cart();

        //public List<BO.OrderForList?> PlOrder
        //{
        //    get { return (List<BO.OrderForList?>)GetValue(PlOrderProperty); }
        //    set { SetValue(PlOrderProperty, value); }//@#$%^&*()_(*&UY^T%R$#$%^&*(
        //} 
        public List<BO.ProductItem?> myProductCat
        {
            get { return (List<BO.ProductItem?>)GetValue(myProductCatProperty); }
            set { SetValue(myProductCatProperty, value); }
        }
        // Using a DependencyProperty as the backing store for PlProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProductCatProperty =
            DependencyProperty.Register("myProductCat", typeof(List<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));
       

        public BO.User? user
        {
            get { return (BO.User?)GetValue(userProperty); }
            set { SetValue(userProperty, value); }
        }

        // Using a DependencyProperty as the backing store for user.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty userProperty =
            DependencyProperty.Register("user", typeof(BO.User), typeof(Window), new PropertyMetadata(null));





        public ProductCatalogWindow(BO.User user)
        {
            InitializeComponent();
            cart=new BO.Cart();
            BO.User myUser= user;
            if (cart.Items == null)
            {
                cart.Items = new List<BO.OrderItem>();
            }
                txtAmountInCart.Text = cart.Items.Count().ToString();
                // listViewProducts.ItemsSource = bl.Product.GetCatalog().ToList();
                myProductCat = new List<BO.ProductItem>();
                myProductCat = bl.Product.GetCatalog().ToList();
       
            

        }

        private void listViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (listViewProducts.SelectedIndex == -1)
                return;
        ProductItemWindow p=new ProductItemWindow(cart, ((BO.ProductItem)listViewProducts.SelectedItem).ID);
            p.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (cart==null||cart.Items == null)
                txtAmountInCart.Text = "0";
            else
                txtAmountInCart.Text = cart.Items.Count().ToString();
               
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CartWindow c=new CartWindow(cart);
            c.ShowDialog();
        }
    }
    
}
