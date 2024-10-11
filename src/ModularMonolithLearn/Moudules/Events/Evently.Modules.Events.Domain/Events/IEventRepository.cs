using Eventity.Moudules.Events.Domain.Events;

namespace Evently.Modules.Events.Domain.Events
{
    public interface IEventRepository
    {
        public void Insert(Event @event);
        public Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
