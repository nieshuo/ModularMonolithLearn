using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Events.RescheduleEvent;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Presentation.Events
{
    public static class RescheduleEvent
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("events/{id}/reschedule", async (Guid id, RescheduleEventRequest request, ISender sender) =>
            {
                Result result = await sender.Send(
                    new RescheduleEventCommand(id, request.StartsAtUtc, request.EndsAtUtc));

                return Results.NoContent();
            })
            .WithTags(Tags.Events);
        }

        internal sealed class RescheduleEventRequest
        {
            public DateTime StartsAtUtc { get; init; }

            public DateTime? EndsAtUtc { get; init; }
        }
    }

}
