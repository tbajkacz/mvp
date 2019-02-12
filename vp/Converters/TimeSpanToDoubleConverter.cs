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
    public class TimeSpanToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case TimeSpan ts:
                    return ts.TotalSeconds;
                case Duration d:
                    if (d.HasTimeSpan)
                    {
                        return d.TimeSpan.TotalSeconds;
                    }
                    else
                    {
                        return 0;
                    }
            }
            throw new ArgumentException($"Argument {nameof(value)} in {nameof(TimeSpanToDoubleConverter.Convert)} should be a TimeSpan or Duration");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double d)
            {
                return TimeSpan.FromSeconds(d);
            }
            throw new ArgumentException($"Argument {nameof(value)} in {nameof(TimeSpanToDoubleConverter.Convert)} should be a double");
        }
    }
}
