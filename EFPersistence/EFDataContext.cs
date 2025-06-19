using Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFPersistence
{
    public class EFDataContext(DbContextOptions option) : IdentityDbContext(option)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IEFPersistenceAssembly).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
