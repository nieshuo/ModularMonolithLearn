using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Events.PublishEvent;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events
{
    public static class PublishEvent
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("events/{id}/publish", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(new PublishEventCommand(id));

                return Results.NoContent();
            })
            .WithTags(Tags.Events);
        }
    }

}
