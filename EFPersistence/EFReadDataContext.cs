using Microsoft.EntityFrameworkCore;

namespace EFPersistence
{
    public class EFReadDataContext(DbContextOptions<EFDataContext> options)
        : EFDataContext(options)
    {
        public EFReadDataContext(string connectionString) 
            : this(new DbContextOptionsBuilder<EFDataContext>()
                  .UseSqlServer(connectionString)
                  .Options)
        {
        }
    }
}
