using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Events.Domain.Events;
using System.Data.Common;

namespace Evently.Modules.Events.Application.Events.GetEvent
{
    internal sealed class GetEventQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetEventQuery, EventResponse>
    {
        public async Task<Result<EventResponse>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
                $"""
             SELECT
                 e."Id" AS {nameof(EventResponse.Id)},
                 e."CategoryId" AS {nameof(EventResponse.CategoryId)},
                 e."Title" AS {nameof(EventResponse.Title)},
                 e."Description" AS {nameof(EventResponse.Description)},
                 e."Location" AS {nameof(EventResponse.Location)},
                 e."StartsAtUtc" AS {nameof(EventResponse.StartsAtUtc)},
                 e."EndsAtUtc" AS {nameof(EventResponse.EndsAtUtc)},
                 tt."Id" AS {nameof(TicketTypeResponse.TicketTypeId)},
                 tt."Name" AS {nameof(TicketTypeResponse.Name)},
                 tt."Price" AS {nameof(TicketTypeResponse.Price)},
                 tt."Currency" AS {nameof(TicketTypeResponse.Currency)},
                 tt."Quantity" AS {nameof(TicketTypeResponse.Quantity)}
             FROM events."Events" e
             LEFT JOIN events."TicketTypes" tt ON tt."EventId" = e."Id"
             WHERE e."Id" = @EventId
             """;

            Dictionary<Guid, EventResponse> eventsDictionary = [];
            await connection.QueryAsync<EventResponse, TicketTypeResponse?, EventResponse>(
                sql,
                (@event, ticketType) =>
                {
                    if (eventsDictionary.TryGetValue(@event.Id, out EventResponse? existingEvent))
                    {
                        @event = existingEvent;
                    }
                    else
                    {
                        eventsDictionary.Add(@event.Id, @event);
                    }

                    if (ticketType is not null)
                    {
                        @event.TicketTypes.Add(ticketType);
                    }

                    return @event;
                },
                request,
                splitOn: nameof(TicketTypeResponse.TicketTypeId));

            if (!eventsDictionary.TryGetValue(request.EventId, out EventResponse eventResponse))
            {
                return Result.Failure<EventResponse>(EventErrors.NotFound(request.EventId));
            }

            return eventResponse;
        }
    }
}
