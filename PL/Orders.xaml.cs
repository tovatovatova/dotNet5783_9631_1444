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
    /// Interaction logic for Orders.xaml
    /// </summary>
    /// 
    public partial class Orders : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public List<BO.ProsuctForList?> myProd
        {
            get { return (List<BO.ProsuctForList?>)GetValue(myProdProperty); }
            set { SetValue(myProdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty myProdProperty =
            DependencyProperty.Register("myProd", typeof(List<BO.ProsuctForList?>), typeof(Window), new PropertyMetadata(null));



        public Orders()
        {
            InitializeComponent();
            myProd = bl.Product.GetProductList().ToList();


        }
    }
}
