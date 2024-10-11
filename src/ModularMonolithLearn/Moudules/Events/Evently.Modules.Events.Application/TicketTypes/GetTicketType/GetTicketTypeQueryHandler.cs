using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Abstractions.Messaging;
using Evently.Modules.Events.Domain.Abstractions;
using Evently.Modules.Events.Domain.TicketTypes;
using System.Data.Common;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketType
{
    internal sealed class GetTicketTypeQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTicketTypeQuery, TicketTypeResponse>
    {
        public async Task<Result<TicketTypeResponse>> Handle(
            GetTicketTypeQuery request,
            CancellationToken cancellationToken)
        {
            await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

            const string sql =
                $"""
             SELECT
                 "Id" AS {nameof(TicketTypeResponse.Id)},
                 "EventId" AS {nameof(TicketTypeResponse.EventId)},
                 "Name" AS {nameof(TicketTypeResponse.Name)},
                 "Price" AS {nameof(TicketTypeResponse.Price)},
                 "Currency" AS {nameof(TicketTypeResponse.Currency)},
                 "Quantity" AS {nameof(TicketTypeResponse.Quantity)}
             FROM events."TicketTypes"
             WHERE "Id" = @TicketTypeId
             """;

            TicketTypeResponse? ticketType =
                await connection.QuerySingleOrDefaultAsync<TicketTypeResponse>(sql, request);

            if (ticketType is null)
            {
                return Result.Failure<TicketTypeResponse>(TicketTypeErrors.NotFound(request.TicketTypeId));
            }

            return ticketType;
        }
    }
}
