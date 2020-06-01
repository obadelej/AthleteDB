using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteDBUI.Models
{
    public class ProfileDisplayModel
    {
        public int AthleteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Male { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ParentName { get; set; }
        public string ParentSurname { get; set; }
        public string ParentPhone { get; set; }
        public string ParentEmail { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string Town { get; set; }
        public string Parish { get; set; }
        public string Country { get; set; }
        public string School { get; set; }
        public string SchoolPhone { get; set; }
        public string SchoolLocation { get; set; }
        public string CoachName { get; set; }
        public string CoachSurname { get; set; }
        public string CoachPhone { get; set; }
        public string CoachEmail { get; set; }
        public string MeetName { get; set; }
        public DateTime StartDate { get; set; }
        public string EndDate { get; set; }
        public string MeetLocation { get; set; }
        public string EventName { get; set; }
        public float EventMark { get; set; }
        public string Wind { get; set; }
        public DateTime PerfDate { get; set; }

        //public string Mark
        //{
        //    get { return GetMark(EventMark); }
        //}

        //private string GetMark(float mark)
        //{
        //    string output = "";
        //    int min = 0;
        //    float sec = 0.00f;

        //    if(mark > 59)
        //    {
        //        min = (int)mark / 60;
        //        sec = mark % 60;

        //        if(sec < 10)
        //        {
        //            output = min + ":0" + sec;
        //        }
        //        else
        //        {
        //            output = min + ":" + sec;
        //        }           

        //    }
        //    return output;
        //}
    }
}
