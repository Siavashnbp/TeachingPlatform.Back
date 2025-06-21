using Services.Feature.Users.Contracts.Dtos;

namespace Services.Feature.Users.Contracts
{
    public interface IUserQuery
    {
        public Task<GetUserInfoResponseDto?> GetUserInfoAsync(string userId);
    }
}
