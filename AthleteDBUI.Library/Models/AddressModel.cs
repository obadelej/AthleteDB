using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteDBUI.Library.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string Town { get; set; }
        public string Parish { get; set; }
        public string Country { get; set; }
    }
}
