namespace AuthServer.Core.UnitOfWork
{
    //SaveChanges Methodu
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
