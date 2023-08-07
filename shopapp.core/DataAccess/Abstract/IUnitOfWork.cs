namespace shopapp.core.DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync(bool state = true);
        bool Commit(bool state = true);
    }
}
