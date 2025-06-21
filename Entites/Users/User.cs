using Applications.Models;
using Microsoft.AspNetCore.Identity;

namespace Entities.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCallingCode { get; set; }
    }
}
