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
    public class BoolToWindowStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return b ? WindowStyle.None : WindowStyle.SingleBorderWindow;
            }
            throw new ArgumentException("Unexpected argument type, bool was expected");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WindowStyle ws)
            {
                return ws == WindowStyle.None;
            }
            throw new ArgumentException("Unexpected argument type, WindowStyle was expected");
        }
    }
}
