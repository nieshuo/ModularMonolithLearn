using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Events.SearchEvents;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events
{
    public static class SearchEvents
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {

            app.MapGet("events/search", async (
                ISender sender,
                int? status,
                Guid? categoryId,
                DateTime? startDate,
                DateTime? endDate,
                int page = 0,
                int pageSize = 15) =>
            {
                Result<SearchEventsResponse> result = await sender.Send(
                    new SearchEventsQuery(status,categoryId, startDate, endDate, page, pageSize));

                return Results.Ok(result);
            })
            .WithTags(Tags.Events);
        }
    }

}
