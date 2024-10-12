using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using System.Data.Common;

namespace Evently.Modules.Events.Application.Events.GetEvents
{
    internal sealed class GetEventsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetEventsQuery, IReadOnlyCollection<EventResponse>>
    {
        public async Task<Result<IReadOnlyCollection<EventResponse>>> Handle(
            GetEventsQuery request,
            CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
                $"""
             SELECT
                 "Id" AS {nameof(EventResponse.Id)},
                 "CategoryId" AS {nameof(EventResponse.CategoryId)},
                 "Title" AS {nameof(EventResponse.Title)},
                 "Description" AS {nameof(EventResponse.Description)},
                 "Location" AS {nameof(EventResponse.Location)},
                 "StartsAtUtc" AS {nameof(EventResponse.StartsAtUtc)},
                 "EndsAtUtc" AS {nameof(EventResponse.EndsAtUtc)}
             FROM events."Events"
             """;

            List<EventResponse> events = (await connection.QueryAsync<EventResponse>(sql, request)).AsList();

            return events;
        }
    }

}
