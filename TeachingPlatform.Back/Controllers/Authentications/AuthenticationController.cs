using Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Infrastructure.Identity.Contracts.Dtos;
using TeachingPlatform.Back.Configs.Identities.Jwt.Contracts;

namespace TeachingPlatform.Back.Controllers.Authentications
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationControllerr(
        IJwtTokenGenerator jwtGenerator,
        UserManager<User> userManager,
        SignInManager<User> signInManager) : ControllerBase
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
                Phone = request.Phone,
            };
            var result = await userManager.CreateAsync(user, request.Password);
            await userManager.AddToRoleAsync(user, "Student");
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok("User registered successfully.");
        }
    }
}
