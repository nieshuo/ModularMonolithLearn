using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Eventity.Moudules.Events.Application.Events
{
    public static class CreateEvent
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("events", async (CreateEventRequest request,ISender sender) =>
            {
                var command = new CreateEventCommand(
                    request.CategoryId,
                    request.Title,
                    request.Description,
                    request.Location,
                    request.StartsAtUtc,
                    request.EndsAtUtc);
                Result<Guid> result = await sender.Send(command);

                return Results.Ok(result);
            })
            .WithTags(Tags.Events);
        }
    }
    internal sealed class CreateEventRequest
    {
        public Guid CategoryId { get; init; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartsAtUtc { get; set; }
        public DateTime? EndsAtUtc { get; set; }
    }
}
