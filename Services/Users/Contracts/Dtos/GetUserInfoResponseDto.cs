using Applications.Models;

namespace Services.Feature.Users.Contracts.Dtos
{
    public class GetUserInfoResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Phone? Phone { get; set; }
    }
}
