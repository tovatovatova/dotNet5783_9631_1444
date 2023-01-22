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
    /// Interaction logic for logInWindow.xaml
    /// </summary>
    public partial class logInWindow : Window
    {
        public BO.User? MyUser
        {
            get { return (BO.User?)GetValue(MyUserProperty); }
            set { SetValue(MyUserProperty, value); }
        }

        public static readonly DependencyProperty MyUserProperty =
            DependencyProperty.Register("MyUser", typeof(BO.User), typeof(Window), new PropertyMetadata(null));

        public bool IsNew
        {
            get { return (bool)GetValue(IsNewProperty); }
            set { SetValue(IsNewProperty, value); }
        }
        public static readonly DependencyProperty IsNewProperty =
    DependencyProperty.Register("IsNew", typeof(bool), typeof(Window), new PropertyMetadata(false));
        BlApi.IBl bl = BlApi.Factory.Get();
       
        public logInWindow(bool newU)
        {
            InitializeComponent();
            IsNew= newU;
            MyUser = new BO.User();
        }

        private void OKSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.User.addUser(MyUser);
                MessageBox.Show("Welcome " + MyUser.UserName + "💖");
                MenuWindow menu = new MenuWindow(MyUser);
                Close();
                menu.Show();
            }
            catch (BO.BlInvalidInputException )
            {
                MessageBox.Show("try again");               
            }
            catch (BO.BlIdAlreadyExistException )
            {
                MessageBox.Show("this user Name is already use try another one please");
            }
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              bl.User.GetUser(MyUser.UserName,MyUser.Password); 
                MessageBox.Show("Welcome "+ MyUser.UserName+"💖");
                MenuWindow menu = new MenuWindow(MyUser);
                Close();
                menu.Show();
            }
            catch (BO.BlIdDoNotExistException )//if user is not in the system
            {
                MessageBox.Show("❌ wrong name or password, please try again");
                return;
            }
            catch (BO.BlInvalidInputException )
            {
                MessageBox.Show("❌ invalid details please check it");
                return;
            }
        }
    }
}
