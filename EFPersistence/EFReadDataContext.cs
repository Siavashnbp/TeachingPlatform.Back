using Applications.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFPersistence
{
    public class EFReadDataContext : EFDataContext
    {
        public EFReadDataContext(string connectionString, IUserInfoReader userInfoReader)
            : base(new DbContextOptionsBuilder<EFDataContext>()
                    .UseSqlServer(connectionString).Options,
                  userInfoReader)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public override ChangeTracker ChangeTracker
        {
            get
            {
                var tracker = base.ChangeTracker;
                tracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
                tracker.AutoDetectChangesEnabled = false;
                return tracker;
            }
        }

    }
}
