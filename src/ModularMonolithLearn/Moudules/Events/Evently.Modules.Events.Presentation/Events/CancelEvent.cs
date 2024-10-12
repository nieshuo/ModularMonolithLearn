using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Events.CancelEvent;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events
{
    public static class CancelEvent
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("events/{id}/cancel", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(new CancelEventCommand(id));

                return Results.NoContent();
            })
            .WithTags(Tags.Events);
        }
    }
}
