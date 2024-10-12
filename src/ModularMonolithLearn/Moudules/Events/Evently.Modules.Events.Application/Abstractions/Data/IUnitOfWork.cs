namespace Evently.Common.Application.Data
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
