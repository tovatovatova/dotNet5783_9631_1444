using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Imaging;

using System.Windows.Controls;
using System.Windows.Media;

namespace PL
{
    //gets boolean variable and provides visibility property acording to the boolean value
    public class BooleanToVisibilityConverter : IValueConverter
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
    //gets boolean variable and provides visibility property acording to the boolean value(the opposite way)
    public class NoBooleanToVisibilityConverter : IValueConverter
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
    //provide vasibility property from numerical value (visible if the number is positive)
    public class DoubleToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value > 0 ? Visibility.Visible : Visibility.Hidden;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
    //provide vasibility property from numerical value (visible if the number is positive)
    public class DoubleToHiddenConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value <= 0 ? Visibility.Hidden : Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
    //provide visible property if the value is hidden
    public class HiddenTOVisible : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Collapsed;
        }
    }
    //convert from status to brushes color 
    public class StatusToColor : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((BO.OrderStatus)value == BO.OrderStatus.Ordered)
                return Brushes.LightGreen;
            else if ((BO.OrderStatus)value == BO.OrderStatus.Shipped)
                return Brushes.LightSkyBlue;
            else
                return Brushes.LightPink;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BO.OrderStatus.Delivered;
        }
    }
    // get status(enum ) and provid value for the progresbar 
    public class StatusToInt : IValueConverter
    {
        private static Random rand = new Random();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((BO.OrderStatus)value == BO.OrderStatus.Ordered)
                return rand.Next(1, 30);
            else if ((BO.OrderStatus)value == BO.OrderStatus.Shipped)
                return rand.Next(31, 70);
            else
                return 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BO.OrderStatus.Delivered;
        }
    }
    //convert from string to bitmap (to provide image source)
    public class StringToBitmap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {//gets string of product and create a path to picture
            BitmapImage bitmap;
            try
            {
                string startImage = value?.ToString() ?? throw new Exception();
                string addDir = Environment.CurrentDirectory[..^4];
                string final = addDir + @"\Images\" + startImage + ".jpg";
                bitmap = new BitmapImage(new Uri(final));
                return bitmap;
            }
            catch (Exception)
            {
                try
                {
                    string startImage = value?.ToString() ?? throw new Exception();
                    string addDir = Environment.CurrentDirectory[..^3];
                    string final = addDir + startImage + ".jpg";
                    bitmap = new BitmapImage(new Uri(final));
                    return bitmap;
                }
                catch (Exception)
                {
                    bitmap = new BitmapImage(new Uri(Environment.CurrentDirectory[..^4] + @"\Images\noPicture.jpg"));
                    return bitmap;//return defult picture
                }
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();

        }
    }
}

       

    





