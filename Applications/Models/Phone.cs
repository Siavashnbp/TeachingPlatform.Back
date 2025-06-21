namespace Applications.Models
{
    public class Phone
    {
        public string? CountryCallingCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string ViewMobile()
        {
            return CountryCallingCode + "-" + PhoneNumber;
        }
    }
}
