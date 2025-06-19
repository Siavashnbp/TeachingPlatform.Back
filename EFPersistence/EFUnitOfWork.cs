using Applications.Interfaces;

namespace EFPersistence
{
    public class EFUnitOfWork(EFWriteDataContext context) : IUnitOfWork
    {
        public async Task BeginTransaction()
        {
            await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await context.Database.CommitTransactionAsync();
        }

        public async Task Complete()
        {
            await context.SaveChangesAsync();
        }

        public async Task RollBackTransaction()
        {
            await context.Database.RollbackTransactionAsync();
        }
    }
}
