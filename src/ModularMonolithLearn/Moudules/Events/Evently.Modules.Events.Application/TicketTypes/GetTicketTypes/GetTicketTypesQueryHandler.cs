using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Application.Abstractions.Messaging;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using Evently.Modules.Events.Domain.Abstractions;
using System.Data.Common;

namespace Evently.Modules.Events.Application.TicketTypes.GetTicketTypes
{
    internal sealed class GetTicketTypesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetTicketTypesQuery, IReadOnlyCollection<TicketTypeResponse>>
    {
        public async Task<Result<IReadOnlyCollection<TicketTypeResponse>>> Handle(
            GetTicketTypesQuery request,
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
             WHERE "EventId" = @EventId
             """;

            List<TicketTypeResponse> ticketTypes =
                (await connection.QueryAsync<TicketTypeResponse>(sql, request)).AsList();

            return ticketTypes;
        }
    }
}
