using Eventity.Moudules.Events.Domain.Events;
using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.TicketTypes
{
    public sealed class TicketType : Entity
    {

        public Guid Id { get; private set; }

        public Guid EventId { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public string Currency { get; private set; }

        public decimal Quantity { get; private set; }
        private TicketType()
        {
        }

        public static TicketType Create(
            Event @event,
            string name,
            decimal price,
            string currency,
            decimal quantity)
        {
            var ticketType = new TicketType
            {
                Id = Guid.NewGuid(),
                EventId = @event.Id,
                Name = name,
                Price = price,
                Currency = currency,
                Quantity = quantity
            };

            return ticketType;
        }

        public void UpdatePrice(decimal price)
        {
            if (Price == price)
            {
                return;
            }

            Price = price;

            Raise(new TicketTypePriceChangedDomainEvent(Id, Price));
        }
    }

}
