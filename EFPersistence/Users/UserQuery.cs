using Applications.Models;
using Entities.Users;
using Microsoft.EntityFrameworkCore;
using Services.Feature.Users.Contracts;
using Services.Feature.Users.Contracts.Dtos;

namespace EFPersistence.Users
{
    public class UserQuery(EFReadDataContext context) : IUserQuery
    {
        public async Task<GetUserInfoResponseDto?> GetUserInfoAsync(string userId)
        {
            return await
                (from user in context.Set<User>()
                 where user.Id == userId
                 select new GetUserInfoResponseDto
                 {
                     FirstName = user.FirstName,
                     LastName = user.LastName,
                     Phone =  new Phone
                     {
                         PhoneNumber = user.PhoneNumber,
                         CountryCallingCode = user.CountryCallingCode,
                     }
                 }).FirstOrDefaultAsync();
        }
    }
}
