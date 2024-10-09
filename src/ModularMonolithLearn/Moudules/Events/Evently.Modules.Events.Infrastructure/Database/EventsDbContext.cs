using Eventity.Moudules.Events.Domain.Events;
using Evently.Modules.Events.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Infrastructure.Database
{
    public sealed class EventsDbContext(DbContextOptions<EventsDbContext> options):DbContext(options),IUnitOfWork
    {
        internal DbSet<Event> Events { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schemas.Events);

            base.OnModelCreating(modelBuilder);
        }
    }
}
