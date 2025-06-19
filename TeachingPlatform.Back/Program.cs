using EFPersistence;
using Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
//builder.Services.AddDbContext<EFWriteDataContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("WriteConnectionString"),
//    x => x.MigrationsAssembly(typeof(EFDataContext).Assembly.FullName)));
//builder.Services.AddDbContext<EFReadDataContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ReadConnectionString")));
var app = builder.Build();

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
