
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
        Regex rg = new Regex("[0-9]+");

        public BO.Product? PlProduct
        {
            get { return (BO.Product?)GetValue(PlProductProperty); }
            set { SetValue(PlProductProperty, value); }
        }

        public static readonly DependencyProperty PlProductProperty =
            DependencyProperty.Register("PlProduct", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));
        List<TextBox> txtlst = new List<TextBox>();

        /// <summary>
        /// productWindow empty constructor provide the option to add new product
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            btnAdd.Visibility = Visibility.Visible;//add button getting visible
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
            catch (BO.BlIdDoNotExistException ex)//product doesnt exist
            {//throw an error message box 
                MessageBox.Show(ex.Message.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }
            btnUpdate.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// the event when user press the butt-ADD in order to finish the adding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (categoryComboBox.SelectedItem == null)//check if the user choose category and show message if not
            {
                MessageBox.Show("choose category", "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }

            try//try to add the product - call to a bl function  add
            {
                bl.Product.AddProduct(PlProduct);
                MessageBox.Show("product added successfully", " ", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                Close();
            }
            catch (BO.BlIdAlreadyExistException ex)
            {
                MessageBox.Show(ex.Message.ToString() + "\n try again", "", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }
            catch (BO.BlInvalidInputException ex)
            {
                MessageBox.Show(ex.ToString() + " \n check your input", "", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }
            catch (BO.BlWrongCategoryException ex)
            {
                MessageBox.Show(ex.ToString() + "\n" + "choose category again", "", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }
        }

        /// <summary>
        /// when update button press- the function calls and responsible of meking the update of existin product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            try//if all are valid -try to update them
            {
                foreach (var item in txtlst)
                {
                    if (item.BorderBrush == Brushes.Red)
                        flag = true;
                }
                bl.Product.UpdateProduct(PlProduct!);
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
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.DeleteProduct(PlProduct.ID);//try to delete product
            }
            catch (BO.BlInvalidInputException ex)//worng input
            {

                MessageBox.Show(ex.Entity+ "\ntry again", "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }
            catch (BO.BlIdDoNotExistException ex)//product doesnt exist
            {
                MessageBox.Show(ex.Message.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }
            catch (BO.BlNullPropertyException ex)//product exists in order-cant delete
            {
                MessageBox.Show(ex.ToString(), "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return;
            }
            Close();
        }

        private void btnAddPic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                picImgBox.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                PlProduct.ImagesSource = openFileDialog.FileName;
            }
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
        }
    }

}
