using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace vp.Converters
{
    public class TimeWatchedAndDurationToPercentageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is TimeSpan watched && values[1] is TimeSpan length)
            {
                return Math.Round((watched.TotalSeconds / length.TotalSeconds * 100), MidpointRounding.AwayFromZero)
                    .ToString();
            }
            throw new ArgumentException($"Expected {nameof(values.Length)} is 2 where both object should be of type {typeof(TimeSpan)}");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
