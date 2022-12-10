﻿using BlApi;
using BlImplementation;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();
        private BO.Product newProduct = new BO.Product() { };
        List<TextBox> texts = new List<TextBox>();

        /// <summary>
        /// productWindow empty constructor provide the option to add new product
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            btnAdd.Visibility = Visibility.Visible;//add button getting visible
            //kipping list of textboxs
            texts.Add(txtID);
            texts.Add(txtName);
            texts.Add(txtPrice);
            texts.Add(txtInStock);

        }
       


        //}
        /// <summary>
        /// gets an id of product-return the product for BO and show its details-update case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        public ProductWindow(object sender, EventArgs e,int id )
        {
            InitializeComponent();
            BO.Product p = new BO.Product();
            try
            {
                p=bl.Product.GetProductDetails(id);//return product from BO 
            }
            catch (BO.BlIdDoNotExistException ex)//product doesnt exist
            {//throw an error message box 
                string messageBoxText = ex.Message.ToString();
                string caption = "error";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
            }

            //show details of product
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            cmbCategory.SelectedItem = p.Category;
            txtID.Text = p.ID.ToString();
            txtID.IsEnabled = false;//cant change id
            txtPrice.Text = p.Price.ToString();
            txtInStock.Text = p.InStock.ToString();
            txtName.Text = p.Name;
            btnUpdate.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// the event when user press the butt-ADD in order to finish the adding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText;
            string caption;
            MessageBoxImage icon;
            MessageBoxResult result;
            texts.Clear();
            double price;
            int inStock, id;
            if (cmbCategory.SelectedItem == null)//check if the user choose category and show message if not
            {
                messageBoxText = "choose category";
                caption = "error";
                icon = MessageBoxImage.Error;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    return;
                }
            }
            if (!int.TryParse(txtID.Text.ToString(), out id))//id validation
            {
                texts.Add(txtID);
                lblXid.Visibility = Visibility;
            }
            else if (id < 0)
            {
                texts.Add(txtID);
                lblXid.Visibility = Visibility;
            }
            if (!double.TryParse(txtPrice.Text.ToString(), out price))//check price validation
            {
                texts.Add(txtPrice);
                lblXPrice.Visibility = Visibility;
            }
            else if (price < 0)
            {
                texts.Add(txtPrice);
                lblXPrice.Visibility = Visibility;
            }
            if (!int.TryParse(txtInStock.Text.ToString(), out inStock))//amount in stock validation
            {
                texts.Add(txtInStock);
                lblXInStock.Visibility = Visibility;

            }
            else if (inStock < 0)
            {
                texts.Add(txtInStock);
                lblXInStock.Visibility = Visibility;
            }
            if (txtName.Text == "")//check if name is written
            {
                texts.Add(txtName);
                lblXName.Visibility = Visibility;
            }

            if (texts.Count > 0)//there is at least one error with at least one input
            {
                messageBoxText = "you insert invalid values\n please try again";
                caption = "error";
                icon = MessageBoxImage.Error;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    foreach (var item in texts)
                    {
                        item.BorderBrush = new SolidColorBrush(Colors.Red);//brush the text border with red -to show were the error is
                    }
                }

            }
            else
            {
                try//try to add the product with call to a bl function od add
                {

                    newProduct.Category = Enum.Parse<BO.Category>(cmbCategory.SelectedItem.ToString());
                    newProduct.Name = txtName.Text.ToString();
                    newProduct.ID = id;
                    newProduct.Price = price;
                    newProduct.InStock = inStock;
                    bl.Product.AddProduct(newProduct);
                    messageBoxText = "product added successfully";
                    caption = "";
                    icon = MessageBoxImage.Information;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.None);
                    if (result == MessageBoxResult.OK)
                        Close();
                }
                catch (BO.BlIdAlreadyExistException ex)
                {
                    messageBoxText = ex.Message + "\n" + "try again";
                    caption = "";
                    icon = MessageBoxImage.Information;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.None);
                    if (result == MessageBoxResult.OK)
                        return;

                }
            }

        }
            
        /// <summary>
        /// when update button press- the function calls and responsible of meking the update of existin product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            texts.Clear();
            double price;
            int inStock;

            BO.Category cat;
            cat = Enum.Parse<BO.Category>(cmbCategory.SelectedItem.ToString());
            //check validation of the inputs
            if (!double.TryParse(txtPrice.Text.ToString(), out price))
            {
                texts.Add(txtPrice);
                lblXPrice.Visibility = Visibility;
            }
            else if (price < 0)
            {
                texts.Add(txtPrice);
                lblXPrice.Visibility = Visibility;
            }
            if (!int.TryParse(txtInStock.Text.ToString(), out inStock))
            {
                texts.Add(txtInStock);
                lblXInStock.Visibility = Visibility;

            }
            else if (inStock < 0)
            {
                texts.Add(txtInStock);
                lblXInStock.Visibility = Visibility;
            }
            if (txtName.Text == "")
            {
                texts.Add(txtName);
                lblXName.Visibility = Visibility;
            }
            //if at least one of the texts went wrong
            if (texts.Count > 0)//there is at least one error with at least one input
            {
                string messageBoxText = "you insert invalid values\n please try again";
                string caption = "error";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    foreach (var item in texts)
                    {
                        item.BorderBrush = new SolidColorBrush(Colors.Red);
                    }
                }

            }
            else try//if all are valid -try to update them
                {
                    newProduct.Category = cat;
                    newProduct.Name = txtName.Text;
                    newProduct.ID = Convert.ToInt32(txtID.Text);
                    newProduct.Price = price;
                    newProduct.InStock = inStock;
                    bl.Product.UpdateProduct(newProduct);
                    Close();
                }
                catch (BO.BlIdDoNotExistException ex)
                {
                    string messageBoxText = ex.Message.ToString();
                    string caption = "error";
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        return;
                    }
                }
                catch (BO.BlInvalidInputException ex)
                {
                    string messageBoxText = ex.ToString() + "\ntry again";
                    string caption = "error";
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        return;
                    }
                }
           



        }

        
        
        /// <summary>
        /// event happens when user press left mouse button (no matter where)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lblXInStock.Visibility = Visibility.Hidden;
            lblXName.Visibility = Visibility.Hidden;
            lblXPrice.Visibility = Visibility.Hidden;
        }

       
    }
}
