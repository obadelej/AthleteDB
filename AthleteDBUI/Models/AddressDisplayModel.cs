namespace AthleteDBUI.Models
{
    public class AddressDisplayModel
    {
        public int Id { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string Town { get; set; }
        public string Parish { get; set; }
        public string Country { get; set; }
        public string FullAddress
        {
            get
            {
                return Street1 + ", " + Street2 + ", " + Town + ", " + Parish + ", " + Country;
            }
        }
    }
}
