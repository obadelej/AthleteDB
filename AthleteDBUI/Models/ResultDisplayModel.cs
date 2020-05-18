using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteDBUI.Models
{
    public class ResultDisplayModel
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public int MeetId { get; set; }
        public int EventId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EventName { get; set; }
        public string MeetName { get; set; }
        public string Location { get; set; }
        public float Mark { get; set; }
        public string Wind { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PerfDate { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName } {LastName}";
            }
        }

        public string MarkDisplay
        {
            get
            {
                return ConvertFloatToMark(Mark, EventName);
            }
        }

        private string ConvertFloatToMark(float mrk, string eventName)
        {
            string output = "";
            if (eventName == "100M" || eventName == "200M" || eventName == "100MH" || eventName == "110H" || eventName == "400M" ||
                eventName == "400MH" || eventName == "800M" || eventName == "1500M" || eventName == "3000M" || eventName == "5000M")
            {

                if(Mark > 59)
                {
                    int mins = (int)mrk / 60;
                    float secs = mrk % 60;

                    if(secs < 10)
                    {
                        output = $"{mins}:0{Math.Round(secs, 2)}";
                        return output;
                    }
                    else
                    {
                        output = $"{mins}:{Math.Round(secs,2)}";
                        return output;
                    }
                    
                }
                else
                {
                    output = mrk.ToString();
                    return output;
                }

            }
            else
            {
                output = mrk.ToString();
                return output;
            }

            //return output;
        }

    }
}
