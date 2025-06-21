using Applications.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFPersistence
{
    public class EFWriteDataContext : EFDataContext
    {
        public EFWriteDataContext(string connectionString,
            IUserInfoReader userInfoReader)
            : base(new DbContextOptionsBuilder<EFDataContext>()
                    .UseSqlServer(connectionString).Options,
                  userInfoReader)
        {

        }
    }
}
