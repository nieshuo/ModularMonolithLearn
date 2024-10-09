using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using MediatR;
using System.Data.Common;

namespace Evently.Modules.Events.Application.Events.GetEvent
{
    internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory)
        : IRequestHandler<GetEventQuery, EventResponse?>
    {
        public async Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
            $"""
             SELECT 
                e."Id" AS {nameof(EventResponse.Id)},
                e."Title" AS {nameof(EventResponse.Title)},
                e."Description" AS {nameof(EventResponse.Description)},
                e."Location" AS {nameof(EventResponse.Location)},
                e."StartsAtUtc" AS {nameof(EventResponse.StartsAtUtc)},
                e."EndsAtUtc" AS {nameof(EventResponse.EndsAtUtc)}
             FROM events."Events" e
             WHERE e."Id" = @EventId
             """;

            EventResponse? @events = await connection.QuerySingleOrDefaultAsync<EventResponse?>(sql, new { EventId = request.EventId });

            return @events;
        }
    }
}
