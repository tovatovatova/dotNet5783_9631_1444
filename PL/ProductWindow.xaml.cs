using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
            List<TextBox> texts = new List<TextBox>();


        public ProductWindow()
        {
            InitializeComponent();
           
            
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            btnAdd.Visibility = Visibility.Visible;
            texts.Add(txtID);
            texts.Add(txtName);
            texts.Add(txtPrice);
            texts.Add(txtInStock);

        }
        //public ProductWindow(EventArgs btnAdd_Click)
        //{
        //    InitializeComponent();
           
            
        //    cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        //  //  Control container = new Control();
          

        //}
        public ProductWindow(object sender, EventArgs e, BO.ProsuctForList sender2)
        {
            InitializeComponent();
            if (sender2 != null)
            {
                cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
                BO.Product pt = bl.Product.GetProductDetails(sender2.ID);
                cmbCategory.SelectedItem = sender2.Category;
                txtID.Text = sender2.ID.ToString();
                txtID.IsEnabled = false;
                cmbCategory.IsEnabled = false;
                txtPrice.Text = sender2.Price.ToString();
                txtInStock.Text = pt.InStock.ToString();
                txtName.Text = sender2.Name;
            }
            btnUpdate.Visibility = Visibility.Visible;
            //  Control container = new Control();


        }


        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in texts)
            {
             
            }
            int check=0;

            
            int id, price, inStock;
            if (int.TryParse(txtID.Text.ToString(), out id))
            {
                check++;
                texts.Remove(txtID);
            }
            if (int.TryParse(txtPrice.Text.ToString(), out price))
            {
                texts.Remove(txtPrice);
                check++;
            }
            if (int.TryParse(txtInStock.Text.ToString(), out inStock))
            {
                texts.Remove(txtInStock);
                check++;
            }
            if (txtName.Text != "")
            {
                texts.Remove(txtName);
                check++;
            }
            if (check==4)
            {
                try
                {
                    newProduct.Category = Enum.Parse<BO.Category>(cmbCategory.SelectedItem.ToString());
                    newProduct.Name = txtName.Text.ToString();
                    newProduct.ID = Convert.ToInt32(txtID.Text);
                    newProduct.Price = Convert.ToInt32(txtPrice.Text);
                    newProduct.InStock = Convert.ToInt32(txtInStock.Text);
                    bl.Product.AddProduct(newProduct);
                }
                catch (BO.BlIdAlreadyExistException ex)
                {
                    string messageBoxText = ex.Message.ToString();
                    string caption = "error";
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                    if(result == MessageBoxResult.OK)
                        foreach (var item in texts)
                        {
                            item.Clear();
                        }
                }
                Close();
            }
            else
            {
                string messageBoxText = "invalid values";

                string caption = "error";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    foreach (var item in texts)

                    {
                        item.Focus();
                        item.BorderBrush = new SolidColorBrush(Colors.Red);
                        item.IsTabStop=IsTabStop;
                       
                    }
                    Keyboard.ClearFocus();
                }

               

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newProduct.Category = Enum.Parse<BO.Category>(cmbCategory.SelectedItem.ToString());
                newProduct.Name = txtName.Text.ToString();
                newProduct.ID = Convert.ToInt32(txtID.Text);
                newProduct.Price = Convert.ToInt32(txtPrice.Text);
                newProduct.InStock = Convert.ToInt32(txtInStock.Text);
                bl.Product.UpdateProduct(newProduct);
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
                string messageBoxText = ex.ToString()+"\ntry again";
                string caption = "error";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    return;
                }
            }
            //newProduct.Category = Enum.Parse<BO.Category>(cmbCategory.SelectedItem.ToString());
            //newProduct.Name = txtName.Text.ToString();
            //newProduct.ID = Convert.ToInt32(txtID.Text);
            //newProduct.Price = Convert.ToInt32(txtPrice.Text);
            //newProduct.InStock = Convert.ToInt32(txtInStock.Text);
            //bl.Product.UpdateProduct(newProduct);
            Close();
            

        }

       

   
    }
}
