using Applications.Interfaces;
using EFPersistence;
using Microsoft.EntityFrameworkCore;

namespace TeachingPlatform.Back.Configs.EFCore
{
    public static class Startup
    {
        public static IServiceCollection AddEFCore(this IServiceCollection services, IConfiguration configs)
        {
            var writeConnectionString = configs.GetConnectionString("WriteConnectionString")
                ?? throw new InvalidOperationException();
            var readConnectionString = configs.GetConnectionString("ReadConnectionString")
                ?? throw new InvalidOperationException();

            services.AddDbContext<EFDataContext>(options =>
                options.UseSqlServer(writeConnectionString,
                x => x.MigrationsAssembly(typeof(EFDataContext).Assembly.FullName)));
            var userInfoReaderService = services.FirstOrDefault(_=>_.ServiceType == typeof(IUserInfoReader));
            services.AddScoped(_ => new EFWriteDataContext(writeConnectionString,userInfoReaderService.ImplementationInstance as IUserInfoReader));
            services.AddScoped(_ => new EFReadDataContext(readConnectionString, userInfoReaderService.ImplementationInstance as IUserInfoReader));
            return services;
        }
    }
}
