using Evently.Modules.Events.Domain.Abstractions;

namespace Eventity.Moudules.Events.Domain.Events
{
    public sealed class Event:Entity
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public DateTime StartsAtUtc { get; private set; }
        public DateTime? EndsAtUtc { get; private set; }
        public EventStastus Status { get; private set; }

        private Event()
        {
        }

        public static Event Create(
            string title,
            string description,
            string location,
            DateTime startsAtUtc,
            DateTime? endsAtUtc)
        {
            var @event = new Event
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                Location = location,
                StartsAtUtc = startsAtUtc,
                EndsAtUtc = endsAtUtc,
                Status = EventStastus.Draft
            };

            @event.Raise(new EventCreatedDomainEvent(@event.Id));

            return @event;
        }
    }
}
