using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteDBUI.Converters
{
    public static class TimeConverter
    {
        public static string ConvertFloatToMark(float mark, string eventName)
        {
            string output = "";
            if (eventName == "100M" || eventName == "200M" || eventName == "100MH" || eventName == "110H" || eventName == "400M" ||
                eventName == "400MH" || eventName == "800M" || eventName == "1500M" || eventName == "3000M" || eventName == "5000M")
            {

                if (mark > 59)
                {
                    int mins = (int)mark / 60;
                    float secs = mark % 60;

                    if (secs < 10)
                    {
                        output = $"{mins}:0{Math.Round(secs, 2)}";
                        return output;
                    }
                    else
                    {
                        output = $"{mins}:{Math.Round(secs, 2)}";
                        return output;
                    }

                }
                else
                {
                    output = mark.ToString();
                    return output;
                }

            }
            else
            {
                output = mark.ToString();
                return output;
            }

            //return output;
        }

        public static float ConvertMarkToFloat(string mark, string eventName)
        {
            float output = 0.00f;
            //string eventName = GetEventName();
            if (eventName == "100M" || eventName == "200M" || eventName == "100MH" || eventName == "110H" || eventName == "400M" ||
                eventName == "400MH" || eventName == "800M" || eventName == "1500M" || eventName == "3000M" || eventName == "5000M"
                || eventName == "Long Jump" || eventName == "High Jump" || eventName == "Triple Jump" || eventName == "Pole Vault"
                || eventName == "Javelin" || eventName == "Shot Put" || eventName == "Discus" || eventName == "Hammer")
            {
                string[] t = mark.Split(':');

                if (t.Length == 1)
                {
                    output = float.Parse(mark);
                    return output;

                }
                else if (t.Length == 2)
                {
                    float mins = float.Parse(t[0]);
                    output = mins * 60;
                    float secs = float.Parse(t[1]);
                    output += secs;
                    return output;
                }

            }

            return output;
        }
    }
}
