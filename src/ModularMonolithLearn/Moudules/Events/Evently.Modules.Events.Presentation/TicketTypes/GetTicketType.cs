using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes
{
    public static class GetTicketType
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("ticket-types/{id}", async (Guid id, ISender sender) =>
            {
                Result<TicketTypeResponse> result = await sender.Send(new GetTicketTypeQuery(id));

                return Results.Ok(result);
            })
            .WithTags(Tags.TicketTypes);
        }
    }
}
