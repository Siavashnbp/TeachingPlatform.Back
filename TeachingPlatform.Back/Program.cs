using Microsoft.AspNetCore.Identity;
using TeachingPlatform.Back.Configs.EFCore;
using TeachingPlatform.Back.Configs.Identities;
using TeachingPlatform.Back.Configs.Services;
using TeachingPlatform.Back.Configs.Swagger;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .RegisterInfrastructureServices()
    .RegisterFeatureQueries();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services
    .AddEFCore(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddIdentity();

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


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
