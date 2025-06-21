using Applications.Interfaces;
using EFPersistence;
using EFPersistence.Users;
using Services.Feature.Users.Contracts;
using Services.Infrastructure.Identity;
using TeachingPlatform.Back.Configs.Identities.Jwt;
using TeachingPlatform.Back.Configs.Identities.Jwt.Contracts;

namespace TeachingPlatform.Back.Configs.Services
{
    public static class Startup
    {
        public static IServiceCollection RegisterFeatureQueries(this IServiceCollection services)
        {
            services.AddScoped<IUserQuery, UserQuery>();
            return services;
        }
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,EFUnitOfWork>();
            services.AddScoped<IUserInfoReader, UserInfoReader>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            return services;
        }
    }
}
