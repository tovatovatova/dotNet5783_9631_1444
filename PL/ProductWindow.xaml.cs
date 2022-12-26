
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
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

        // Using a DependencyProperty as the backing store for PlProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlProductProperty =
            DependencyProperty.Register("PlProduct", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));


        /// <summary>
        /// productWindow empty constructor provide the option to add new product
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            categoryComboBox.ItemsSource = Enum.GetValues(typeof(BO.Category));
            btnAdd.Visibility = Visibility.Visible;//add button getting visible
            PlProduct = new BO.Product();
            
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

            //BO.Product p = new BO.Product();
            try
            {
                PlProduct = bl.Product.GetProductDetails(id);//return product from BO 
            }
            catch (BO.BlIdDoNotExistException ex)//product doesnt exist
            {//throw an error message box 
                string messageBoxText = ex.Message.ToString();
                string caption = "error";
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
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
            string messageBoxText;
            string caption;
            MessageBoxImage icon;
            MessageBoxResult result;
            if (categoryComboBox.SelectedItem == null)//check if the user choose category and show message if not
            {
                messageBoxText = "choose category";
                caption = "error";
                icon = MessageBoxImage.Error;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                    return;
                
            }
                try//try to add the product with call to a bl function  add
                {
                    bl.Product.AddProduct(PlProduct!);
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
                catch (BO.BlInvalidInputException ex)
                {
                    messageBoxText = ex.Message + "\n" + "check your input";
                    caption = "";
                    icon = MessageBoxImage.Information;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.None);
                    if (result == MessageBoxResult.OK)
                        return;
                }
                catch (BO.BlWrongCategoryException ex)
                {
                    messageBoxText = ex.Message + "\n" + "choose category again";
                    caption = "";
                    icon = MessageBoxImage.Information;
                    result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.None);
                    if (result == MessageBoxResult.OK)
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

            try//if all are valid -try to update them
            {              
                bl.Product.UpdateProduct(PlProduct!);
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
            iDTextBox.BorderBrush = Background;
            iDTextBox.BorderBrush = Background;
            inStockTextBox.BorderBrush = Background;
            nameTextBox.BorderBrush = Background;
            priceTextBox.BorderBrush = Background;
        }
    }
   
}
