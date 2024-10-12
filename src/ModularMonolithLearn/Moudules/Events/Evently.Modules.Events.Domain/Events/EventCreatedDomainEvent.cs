using Evently.Common.Domain;

namespace Eventity.Moudules.Events.Domain.Events
{
    public sealed class EventCreatedDomainEvent(Guid eventId) : DomainEvent
    {
        public Guid EventId { get; init; } = eventId;
    }
}
