using System;
using System.Globalization;
using System.Windows.Data;

namespace Mdm.Windows.Desktop.Assets
{
    public class RadioBoolToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int)value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = 0;
            if (parameter is string && int.TryParse(parameter.ToString(), out integer))
                return integer;

            return parameter;
        }
    }
}
