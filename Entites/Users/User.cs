using Applications.Models;
using Microsoft.AspNetCore.Identity;

namespace Entities.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Phone Phone { get; set; }
    }
}
