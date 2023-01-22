
using PL.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;




namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BlApi.IBl bl = BlApi.Factory.Get();


      
      


        public MainWindow()
        {
        
            InitializeComponent();
        }


        private void btnLogIn_Click(object sender, RoutedEventArgs e)//open the log in window with not option of new user
        {
            logInWindow l = new logInWindow(false);
          
            l.Show();
        }
        private void Button_Click(object sender, RoutedEventArgs e)//open the simulation window
        {
            SimulationWindow o = new SimulationWindow();
            o.Show();
        }

        private void SigIn_MouseDoubleClick(object sender, MouseButtonEventArgs e)//open the sign in window with the option of new user
        {
            logInWindow l=new logInWindow(true);
            l.Show();
        }
    }
}
