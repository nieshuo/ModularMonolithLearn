using Evently.Modules.Events.Domain.Abstractions;

namespace Evently.Modules.Events.Application.Abstractions.Messaging
{
    public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler
    where TDomainEvent : IDomainEvent
    {
        Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }

    public interface IDomainEventHandler
    {
        Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
