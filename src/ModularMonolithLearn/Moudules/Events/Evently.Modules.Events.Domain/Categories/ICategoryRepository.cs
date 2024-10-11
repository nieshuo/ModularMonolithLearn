namespace Evently.Modules.Events.Domain.Categories
{
    public interface ICategoryRepository
    {
        public Task<Category?> GetAsync(Guid id, CancellationToken cancellationToken = default);

        public void Insert(Category category);
    }
}
