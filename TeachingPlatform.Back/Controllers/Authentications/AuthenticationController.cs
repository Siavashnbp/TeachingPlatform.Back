using Applications.Interfaces;
using Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Infrastructure.Identity.Contracts.Dtos;
using TeachingPlatform.Back.Configs.Identities.Jwt.Contracts;

namespace TeachingPlatform.Back.Controllers.Authentications
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(
        IJwtTokenGenerator jwtGenerator,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null) return Unauthorized("Invalid credentials");

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid credentials");

            var roles = await userManager.GetRolesAsync(user);
            var token = jwtGenerator.Generate(user.Id, roles[0]);
            return Ok(new { Token = token });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            await unitOfWork.BeginTransaction();
            try
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user is not null)
                {
                    return BadRequest("Email is already registered");
                }
                user = new User
                {
                    Email = request.Email,
                    UserName = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    CountryCallingCode = request.Phone.CountryCallingCode,
                    PhoneNumber = request.Phone.PhoneNumber,
                };
                var userCreationResult = await userManager.CreateAsync(user, request.Password);
                var roleCreationResult = await userManager.AddToRoleAsync(user, "Student");

                if (!userCreationResult.Succeeded)
                {
                    await unitOfWork.RollBackTransaction();
                    return BadRequest(userCreationResult.Errors);
                }
                if (!roleCreationResult.Succeeded)
                {
                    await unitOfWork.RollBackTransaction();
                    return BadRequest(roleCreationResult.Errors);
                }
                await unitOfWork.CommitTransaction();
                return Ok("User registered successfully.");
            }
            catch (Exception exception)
            {
                await unitOfWork.RollBackTransaction();
                return StatusCode(500, exception.Message);
            }

        }
    }
}
