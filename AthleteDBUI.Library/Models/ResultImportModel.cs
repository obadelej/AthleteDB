using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteDBUI.Library.Models
{
	public class ResultImportModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EventCode { get; set; }
		public string MeetName { get; set; }
		public float Mark { get; set; }
		public string Wind { get; set; }
		public int MeetDay { get; set; }
		public DateTime PerfDate { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}
