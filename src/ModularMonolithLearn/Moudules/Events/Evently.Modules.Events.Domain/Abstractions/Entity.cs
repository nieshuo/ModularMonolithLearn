namespace Evently.Modules.Events.Domain.Abstractions
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        protected Entity()
        {
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents()
        {
            return _domainEvents.ToList();
        }
    }
}
