using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace vp.Converters
{
    public class BoolToWindowStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return b ? WindowState.Maximized : WindowState.Normal;
            }
            throw new ArgumentException("Unexpected argument type, bool was expected");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowState ws)
            {
                return ws == WindowState.Maximized;
            }
            throw new ArgumentException("Unexpected argument type, WindowState was expected");
        }
    }
}
