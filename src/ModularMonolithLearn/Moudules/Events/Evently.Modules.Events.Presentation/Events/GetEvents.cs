using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Events.GetEvents;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.Results;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.Events
{
    internal class GetEvents:IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("events", async (ISender sender) =>
            {
                Result<IReadOnlyCollection<EventResponse>> result = await sender.Send(new GetEventsQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Events);
        }
    }

}
