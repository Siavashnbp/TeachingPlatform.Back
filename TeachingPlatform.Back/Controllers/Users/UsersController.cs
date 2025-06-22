using Applications.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Feature.Users.Contracts;
using Services.Feature.Users.Contracts.Dtos;

namespace TeachingPlatform.Back.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserInfoReader userInfoReader,
        IUserQuery query) : ControllerBase
    {
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<GetUserInfoResponseDto?> GetUserInfo()
        {
            var userId = userInfoReader.UserId;
            return await query.GetUserInfoAsync(userId);
        }
    }
}
