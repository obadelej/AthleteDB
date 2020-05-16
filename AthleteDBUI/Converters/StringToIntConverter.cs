using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AthleteDBUI.Converters
{
    public class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result = 0;
            var s = int.TryParse((string)value, out result);
            if (s)
            {
                return result;
            }

            return 0;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            return result = value.ToString();
        }
    }
}
