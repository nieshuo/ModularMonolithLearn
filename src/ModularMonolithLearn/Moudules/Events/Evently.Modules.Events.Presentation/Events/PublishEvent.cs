using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Events.PublishEvent;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.Results;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.Events
{
    internal class PublishEvent:IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("events/{id}/publish", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(new PublishEventCommand(id));

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .WithTags(Tags.Events);
        }
    }

}
