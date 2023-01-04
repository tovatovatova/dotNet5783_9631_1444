using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Imaging;
using DalApi;
using System.Windows.Controls;

namespace PL
{
   public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value) < 15 ? false : true;
        }
    }
    public class NoBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToInt32(value) < 15 ? false : true;
        }
    }
    public class StringToBitmap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string startImage = value?.ToString()??throw new Exception();
                string addDir = Environment.CurrentDirectory[..^4];
                string final = addDir +@"\Images\"+startImage+".jpg";
                BitmapImage bitmap = new BitmapImage(new Uri(final));
                return bitmap;
            }
            catch(Exception ex)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(Environment.CurrentDirectory[..^4]+@"\Images\noPicture.jpg"));
                return bitmap;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return value.ToString();
       
        }
    }
    public class AmountToComboBox : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            { 
                BO.Cart cart = value as BO.Cart;
                int items = cart.Items.Count();
                List<int> lst = new List<int>();
                
                for (int i = 0; i < items; i++)
                {
                    lst.Add(i);
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
    
}
//public class IdToProduct()
