using Applications.Models;
using System.ComponentModel.DataAnnotations;

namespace Services.Infrastructure.Identity.Contracts.Dtos
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(50)]
        public Phone Phone { get; set; }
    }
}
