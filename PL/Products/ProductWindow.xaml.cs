
using BO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        BlApi.IBl bl = BlApi.Factory.Get();
        public static readonly DependencyProperty MyImageSourceProperty =
        DependencyProperty.Register("MyImageSource", typeof(BitmapImage), typeof(Window), new PropertyMetadata(null));
        public BitmapImage MyImageSource
        {
            get { return (BitmapImage)GetValue(MyImageSourceProperty); }
            set { SetValue(MyImageSourceProperty, value); }
        }

        public BO.Product? PlProduct
        {
            get { return (BO.Product?)GetValue(PlProductProperty); }
            set { SetValue(PlProductProperty, value); }
        }

        public static readonly DependencyProperty PlProductProperty =
            DependencyProperty.Register("PlProduct", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));
        List<TextBox> txtlst = new List<TextBox>();
        public bool IsUpdate
        {
            get { return (bool)GetValue(IsUpdateProperty); }
            set { SetValue(IsUpdateProperty, value); }
        }
        public static readonly DependencyProperty IsUpdateProperty =
    DependencyProperty.Register("IsUpdate", typeof(bool), typeof(Window), new PropertyMetadata(false));
        /// <summary>
        /// productWindow empty constructor provide the option to add new product
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            PlProduct = new BO.Product();
            txtlst.Add(nameTextBox);
            txtlst.Add(priceTextBox);
            txtlst.Add(inStockTextBox);
            txtlst.Add(iDTextBox);


        }

        //}
        /// <summary>
        /// gets an id of product-return the product for BO and show its details-update case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        public ProductWindow(object sender, EventArgs e, int id)
        {
            InitializeComponent();
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));

            //BO.ProductAddProduct p = new BO.Product();
            try
            {
                PlProduct = bl.Product.GetProductDetails(id);//return product from BO 
                                                             // categoryComboBox.SelectedItem = PlProduct.Category;
            }
            catch (BO.BlIdDoNotExistException)//product doesnt exist
            {//throw an error message box 
                MessageBox.Show("oops,this product is unavaliable😪");
            }
            IsUpdate = true;
        }
        /// <summary>
        /// the event when user press the butt-ADD in order to finish the adding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

        /// <summary>
        /// when update button press- the function calls and responsible of meking the update of existin product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

       

        private void btnAddPic_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {

            if (PlProduct.ImagesSource != null)//has picture
            {
                string imName = PlProduct.ImagesSource.Substring(PlProduct.ImagesSource.LastIndexOf("\\"));
                if (!File.Exists(Environment.CurrentDirectory[..^4] + @"\Images" + imName))//check if there is anything in the path
                {//if not
                    File.Copy(PlProduct.ImagesSource, Environment.CurrentDirectory[..^4] + @"\Images" + imName);//creates a path
                }
                PlProduct.ImagesSource = @"Images" + imName;

            }
            else
            {
                MessageBoxResult result = MessageBox.Show("do you want to add product without picture?","Confirmation", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.No)
                {
                    return;
                }

            }
            if (IsUpdate)
            {
                try//if all are valid -try to update them
                {
                    var lst = txtlst.Where(item => item.Text.ToString() == " ");
                    if (lst.Count() != 0)
                    {
                        MessageBox.Show("check the details maybe you forgot something");
                        return;
                    }
                    bl.Product.UpdateProduct(PlProduct);
                    Close();
                }
                catch (BO.BlIdDoNotExistException ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    return;
                }
                catch (BO.BlInvalidInputException ex)
                {
                    MessageBox.Show(ex.ToString() + "\ntry again", "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    return;
                }
            }
            else
            {
                if (categoryComboBox.SelectedItem == null)//check if the user choose category and show message if not
                {
                    MessageBox.Show("choose category");
                    return;
                }

                try//try to add the product - call to a bl function  add
                {
                    bl.Product.AddProduct(PlProduct);
                    MessageBox.Show("product added successfully");
                    Close();
                }
                catch (BO.BlIdAlreadyExistException)
                {
                    MessageBox.Show("oops, product with this ID already exist");
                    return;
                }
                catch (BO.BlInvalidInputException)
                {
                    MessageBox.Show("check your input, something wrong");
                    return;
                }
                catch (BO.BlWrongCategoryException)
                {
                    MessageBox.Show("choose category again");
                    return;
                }
            }
        }

        private void btnCHoosePic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    MyImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                    PlProduct.ImagesSource = openFileDialog.FileName;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("we cant add this picture");
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.DeleteProduct(PlProduct.ID);//try to delete product
            }

            catch (BO.BlIdDoNotExistException ex)//product doesnt exist
            {
                MessageBox.Show("can not delete");
                return;
            }
            catch (BO.BlIdAlreadyExistException ex)//product exists in order-cant delete
            {
                MessageBox.Show("oops, you cant delete product that already exist in order");
            }
            Close();
        }
    }

}
