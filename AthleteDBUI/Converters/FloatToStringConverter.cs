using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace AthleteDBUI.Converters
{
    public class FloatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string output = "";
            
            string eventName = ((TextBlock)parameter).Text;
            if (eventName == "100M" || eventName == "200M" || eventName == "100MH" || eventName == "110H" || eventName == "400M" ||
                eventName == "400MH" || eventName == "800M" || eventName == "1500M" || eventName == "3000M" || eventName == "5000M")
            {
                if((float)value > 59)
                {
                    var mins = (int)((float)value / 60);
                    
                    var secs = (float)value % 60;

                    if(secs < 10)
                    {
                        output = mins.ToString() + ":0" + secs.ToString("0.##");
                    }
                    else
                    {
                        output = mins.ToString() + ":" + secs.ToString("0.##");
                    }
                   
                }
                else
                {
                    output = value.ToString();
                }

            }

            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
