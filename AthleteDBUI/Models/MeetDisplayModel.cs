﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteDBUI.Models
{
    public class MeetDisplayModel
    {
        public int Id { get; set; }
        public string MeetName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
    }
}
