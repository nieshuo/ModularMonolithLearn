namespace Evently.Modules.Events.Domain.TicketTypes
{
    public interface ITicketTypeRepository
    {
        public Task<TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default);

        public Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default);

        public void Insert(TicketType ticketType);
    }

}
