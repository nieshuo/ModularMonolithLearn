using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Application.Abstractions.Messaging
{
    public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
    {
        public abstract Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

        public Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default) =>
            Handle((TDomainEvent)domainEvent, cancellationToken);
    }
}
