using Microsoft.OpenApi.Models;

namespace TeachingPlatform.Back.Configs.Swagger
{
    public static class Startup
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.  
                        Enter 'Bearer' [space] and then your token in the text input below.
                        Example: 'Bearer eyJhbGci...'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            },
                            Array.Empty<string>()
                        }
                 });
                options.OperationFilter<TenantIdHeaderParameter>();
            });

            return services;
        }
    }
}
