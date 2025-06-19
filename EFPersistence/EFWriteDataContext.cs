using Microsoft.EntityFrameworkCore;

namespace EFPersistence
{
    public class EFWriteDataContext(DbContextOptions<EFDataContext> options) 
        : EFDataContext(options)
    {
        public EFWriteDataContext(string connectionString) 
            : this(new DbContextOptionsBuilder<EFDataContext>()
                  .UseSqlServer(connectionString)
                  .Options)
        {
        }
    }
}
