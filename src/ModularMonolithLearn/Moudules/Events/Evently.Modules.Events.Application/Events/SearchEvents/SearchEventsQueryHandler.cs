using Dapper;
using Eventity.Moudules.Events.Domain.Events;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Application.Events.GetEvents;
using Evently.Common.Domain;
using System.Data.Common;

namespace Evently.Modules.Events.Application.Events.SearchEvents
{
    internal sealed class SearchEventsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<SearchEventsQuery, SearchEventsResponse>
    {

        public async Task<Result<SearchEventsResponse>> Handle(
            SearchEventsQuery request,
            CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            var parameters = new SearchEventsParameters(
                request.Status,
                request.CategoryId,
                request.StartDate?.Date,
                request.EndDate?.Date,
                request.PageSize,
                (request.Page - 1) * request.PageSize);

            IReadOnlyCollection<EventResponse> events = await GetEventsAsync(connection, parameters);

            int totalCount = await CountEventsAsync(connection, parameters);

            return new SearchEventsResponse(request.Page, request.PageSize, totalCount, events);
        }

        private static async Task<IReadOnlyCollection<EventResponse>> GetEventsAsync(
            DbConnection connection,
            SearchEventsParameters parameters)
        {
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
             WHERE
                (@Status Is Null Or "Status" = @Status) AND
                (@CategoryId IS NULL OR "CategoryId" = @CategoryId) AND
                (@StartDate::timestamp IS NULL OR "StartsAtUtc" >= @StartDate::timestamp) AND
                (@EndDate::timestamp IS NULL OR "EndsAtUtc" >= @EndDate::timestamp)
             ORDER BY  "StartsAtUtc"
             OFFSET @Skip
             LIMIT @Take
             """;

            List<EventResponse> events = (await connection.QueryAsync<EventResponse>(sql, parameters)).AsList();

            return events;
        }

        private static async Task<int> CountEventsAsync(DbConnection connection, SearchEventsParameters parameters)
        {
            const string sql =
                """
            SELECT COUNT(*)
            FROM events."Events"
            WHERE
               "Status" = @Status AND
               (@CategoryId IS NULL OR "CategoryId" = @CategoryId) AND
               (@StartDate::timestamp IS NULL OR "StartsAtUtc" >= @StartDate::timestamp) AND
               (@EndDate::timestamp IS NULL OR "EndsAtUtc" >= @EndDate::timestamp)
            """;

            int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

            return totalCount;
        }

        private sealed record SearchEventsParameters(
            int? Status,
            Guid? CategoryId,
            DateTime? StartDate,
            DateTime? EndDate,
            int Take,
            int Skip);
    }

}
