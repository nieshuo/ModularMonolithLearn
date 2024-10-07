using Eventity.Moudules.Events.Api.Database;
using Eventity.Moudules.Events.Api.Events;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventity.Moudules.Events.Api
{
    public static class EventsModule
    {
        public static void MapEndpoints(IEndpointRouteBuilder app)
        {
            CreateEvent.MapEndpoint(app);
            GetEvent.MapEndpoint(app);
        }

        public static IServiceCollection AddEventsModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            string databaseConnectionString = configuration.GetConnectionString("Database")!;

            services.AddDbContext<EventsDbContext>(options =>
            {
                options.UseNpgsql(databaseConnectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName,Schemas.Events));
            });
            return services;
        }
    }
}
