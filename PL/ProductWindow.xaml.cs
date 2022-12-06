﻿using BlApi;
using BlImplementation;
using BO;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();
        private BO.Product newProduct = new BO.Product() { };
        private Brush red;

        public ProductWindow()
        {
            InitializeComponent();
           
            
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
          //  Control container = new Control();
          

        }
        public ProductWindow(EventArgs btnAdd_Click)
        {
            InitializeComponent();
           
            
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
          //  Control container = new Control();
          

        }
        public ProductWindow(object sender, EventArgs e, BO.ProsuctForList sender2)
        {
            InitializeComponent();
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            BO.Product pt = bl.Product.GetProductDetails(sender2.ID);
            cmbCategory.SelectedItem = sender2.Category;
            txtID.Text =sender2.ID.ToString();
            txtID.IsEnabled = false;
           cmbCategory.IsEnabled = false;
            txtPrice.Text=sender2.Price.ToString();
            txtInStock.Text=pt.InStock.ToString();
            txtName.Text = sender2.Name;


          //  Control container = new Control();
          

        }


        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           
            if (txtID.Text == null)
                txtID.BorderBrush = red;
            if (txtPrice.Text == null)
                txtPrice.BorderBrush = red;
            if (txtName.Text == null)
                txtName.BorderBrush = red;
            if (txtInStock.Text == null)
                txtInStock.BorderBrush = red;
            newProduct.Category = Enum.Parse<BO.Category>(cmbCategory.SelectedItem.ToString());
            newProduct.Name = txtName.Text.ToString();
            newProduct.ID = Convert.ToInt32(txtID.Text);
            newProduct.Price = Convert.ToInt32(txtPrice.Text);
            newProduct.InStock= Convert.ToInt32(txtInStock.Text);
            bl.Product.AddProduct(newProduct);
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            newProduct.Category = Enum.Parse<BO.Category>(cmbCategory.SelectedItem.ToString());
            newProduct.Name = txtName.Text.ToString();
            newProduct.ID = Convert.ToInt32(txtID.Text);
            newProduct.Price = Convert.ToInt32(txtPrice.Text);
            newProduct.InStock = Convert.ToInt32(txtInStock.Text);
            bl.Product.UpdateProduct(newProduct);
            this.Close();

        }
    }
}
