using EFPersistence;
using Entities.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TeachingPlatform.Back.Configs.Identities;
using TeachingPlatform.Back.Configs.Identities.Jwt;
using TeachingPlatform.Back.Configs.Identities.Jwt.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var writeConnectionString = builder.Configuration.GetConnectionString("WriteConnectionString")
    ?? throw new InvalidOperationException();
var readConnectionString = builder.Configuration.GetConnectionString("ReadConnectionString")
    ?? throw new InvalidOperationException();

builder.Services.AddDbContext<EFDataContext>(options =>
    options.UseSqlServer(writeConnectionString,
    x => x.MigrationsAssembly(typeof(EFDataContext).Assembly.FullName)));
builder.Services.AddScoped(_ => new EFWriteDataContext(writeConnectionString));
builder.Services.AddScoped(_ => new EFWriteDataContext(readConnectionString));

var jwtSettings = new JwtSetting();
builder.Configuration.GetSection("Jwt").Bind(jwtSettings);
var key = Encoding.UTF8.GetBytes(jwtSettings.Key);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    // Lockout settings (optional)
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings (optional)
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<EFDataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IJwtTokenGenerator,JwtTokenGenerator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRoles.SeedRolesAsync(roleManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
