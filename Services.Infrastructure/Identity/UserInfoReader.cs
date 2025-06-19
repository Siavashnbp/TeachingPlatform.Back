using Applications.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Services.Infrastructure.Identity
{
    public class UserInfoReader(IHttpContextAccessor httpContextAccessor) : IUserInfoReader
    {
        public string? TenantID =>
            httpContextAccessor.HttpContext.Request.Headers
            .FirstOrDefault(header=> header.Key.ToLower() =="tenantid").Value;

        public string? UserID =>
            httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(claim=> claim.Type == ClaimTypes.NameIdentifier)?.Value;

        public string? UserName => 
            httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(claim => claim.Type == "preferred_username")?.Value;
    }
}
