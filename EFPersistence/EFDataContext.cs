using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EFPersistence
{
    public class EFDataContext(DbContextOptions options)
        : IdentityDbContext(options)
    {
    }
}
