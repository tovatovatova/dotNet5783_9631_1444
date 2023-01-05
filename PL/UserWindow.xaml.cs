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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();



        public BO.User? user
        {
            get { return (BO.User?)GetValue(userProperty); }
            set { SetValue(userProperty, value); }
        }

        // Using a DependencyProperty as the backing store for user.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty userProperty =
            DependencyProperty.Register("user", typeof(BO.User), typeof(Window), new PropertyMetadata(null));





        public UserWindow()
        {
            InitializeComponent();
            user = new();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText,caption;
            MessageBoxImage icon;
            MessageBoxResult result;
            try//tries to add a customer to the system
            {
                bl.User.addUser(user);
                messageBoxText = "product added successfully";
                caption = "";
                icon = MessageBoxImage.Information;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.None);
                if (result == MessageBoxResult.OK)
                    Close();
            }
            catch (BO.BlInvalidInputException ex)
            {
                //throw an error message box 
                 messageBoxText = ex.Message.ToString();
                 caption = "error";
                 icon = MessageBoxImage.Error;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                return;
            }
            catch(BO.BlIdAlreadyExistException ex)
            {
                //throw an error message box 
                 messageBoxText = ex.Message.ToString();
                 caption = "error";
                 icon = MessageBoxImage.Error;
                result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK, icon, MessageBoxResult.OK);
                if (result == MessageBoxResult.OK)
                {
                    return;
                }
            }
            //user added successfully
             
        }
    }
}
