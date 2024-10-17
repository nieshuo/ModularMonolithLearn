using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.TicketTypes.GetTicketType;
using Evently.Modules.Events.Application.TicketTypes.GetTicketTypes;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.Results;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.TicketTypes
{
    internal class GetTicketTypes:IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("ticket-types", async (Guid eventId, ISender sender) =>
            {
                Result<IReadOnlyCollection<TicketTypeResponse>> result = await sender.Send(
                    new GetTicketTypesQuery(eventId));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.TicketTypes);
        }
    }
}
