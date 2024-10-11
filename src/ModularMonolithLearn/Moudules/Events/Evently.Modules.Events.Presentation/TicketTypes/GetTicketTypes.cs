using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypes;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes
{
    public static class GetTicketTypes
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("ticket-types", async (Guid eventId, ISender sender) =>
            {
                Result<IReadOnlyCollection<TicketTypeResponse>> result = await sender.Send(
                    new GetTicketTypesQuery(eventId));

                return Results.Ok(result);
            })
            .WithTags(Tags.TicketTypes);
        }
    }
}
