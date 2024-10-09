namespace Evently.Modules.Events.Application.Abstractions.Data
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
