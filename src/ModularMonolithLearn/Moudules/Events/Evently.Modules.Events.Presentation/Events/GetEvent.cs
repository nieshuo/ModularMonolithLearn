using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Events.GetEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventity.Moudules.Events.Application.Events
{
    public static class GetEvent
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("events/{id}", async (Guid id, ISender sender) =>
            {
                var @event = await sender.Send(new GetEventQuery(id));

                return @event is null ? Results.NotFound() : Results.Ok(@event);
            })
            .WithTags(Tags.Events);
        }
    }
}
