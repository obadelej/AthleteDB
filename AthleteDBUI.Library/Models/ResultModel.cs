using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteDBUI.Library.Models
{
    public class ResultModel
    {
        public int Id { get; set; }
        public int AthleteId { get; set; }
        public int MeetId { get; set; }
        public int EventId { get; set; }
        public float Mark { get; set; }
        public string Wind { get; set; }
        public DateTime PerfDate { get; set; }
    }
}
