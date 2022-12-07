using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

          //  Control container = new Control();
          

        }


        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           
            
            List<TextBox> texts = new List<TextBox>();
            texts.Add(txtID);
            texts.Add(txtName);
            texts.Add(txtPrice);
            texts.Add(txtInStock);
            int id,price,inStock;
            if (int.TryParse(txtID.Text.ToString(), out id))
                texts.Remove(txtID);
            if (int.TryParse(txtPrice.Text.ToString(), out price))
                texts.Remove(txtID);
            if (int.TryParse(txtInStock.Text.ToString(), out inStock))
                texts.Remove(txtID);
            foreach (var item in texts)
            {
               
            }
          // foreach (TextBox text in texts) (text=> BorderBrush.Transform. )
            //    txtID.BorderBrush = red;
            //if (txtPrice.Text == null)
            //    txtPrice.BorderBrush = red;
            //if (txtName.Text == null)
            //    txtName.BorderBrush = red;
            //if (txtInStock.Text == null)
            //    txtInStock.BorderBrush.SetValue(Colors.Red)
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
                //MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                //MessageBox.Show(ex.ToString() + "there is already product with the same ID");
                result=MessageBox.Show(messageBoxText, caption, MessageBoxButton.OKCancel, icon,MessageBoxResult.OK);    
            }
            Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            newProduct.Category = Enum.Parse<BO.Category>(cmbCategory.SelectedItem.ToString());
            newProduct.Name = txtName.Text.ToString();
            newProduct.ID = Convert.ToInt32(txtID.Text);
            newProduct.Price = Convert.ToInt32(txtPrice.Text);
            newProduct.InStock = Convert.ToInt32(txtInStock.Text);
            bl.Product.UpdateProduct(newProduct);
            Close();

        }

        private void txtID_TouchEnter(object sender, TouchEventArgs e)
        {
            //if(bl.Product.GetProductList(Convert.ToInt32(txtID.Text.ToString())==)
        }
    }
}
