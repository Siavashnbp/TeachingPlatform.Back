using Microsoft.EntityFrameworkCore;

namespace EFPersistence
{
    public class EFWriteDataContext : EFDataContext
    {
        public EFWriteDataContext(string connectionString)
            : base(new DbContextOptionsBuilder<EFDataContext>()
                    .UseSqlServer(connectionString).Options)
        {

        }
        //public EFWriteDataContext(string connectionString)
        //    :this (new DbContextOptionsBuilder<EFDataContext>()
        //         .UseSqlServer(connectionString).Options)
        //{

        //}
    }
}
