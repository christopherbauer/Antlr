namespace Antlr.ValueConverter
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class MarginLeftValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var left = System.Convert.ToInt32(value);
            return new Thickness(left, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}