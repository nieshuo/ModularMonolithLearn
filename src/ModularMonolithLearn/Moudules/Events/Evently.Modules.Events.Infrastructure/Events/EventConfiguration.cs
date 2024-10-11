using Eventity.Moudules.Events.Domain.Events;
using Evently.Modules.Events.Domain.Categories;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.Events
{
    internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasOne<Category>().WithMany();
        }
    }
}
