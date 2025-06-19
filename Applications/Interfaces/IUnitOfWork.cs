namespace Applications.Interfaces
{
    public interface IUnitOfWork
    {
        public Task Complete();
        public Task CommitTransaction();
        public Task BeginTransaction();
        public Task RollBackTransaction();
    }
}
