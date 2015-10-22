namespace Antlr.ValueConverter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    using Antlr.Core;

    public class StatusValueConverter : IValueConverter
    {
        private readonly FilterColorHelper filterColorHelper = new FilterColorHelper();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var filterColor = Color.FromArgb(255,150,150,150);
            if (value != null)
            {
                var status = (FilterStatus)value;
                filterColor = this.filterColorHelper.GetFilterColor(status);
            }

            return new SolidColorBrush(filterColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}