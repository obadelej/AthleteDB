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
        public int Mark { get; set; }
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

    }
}
