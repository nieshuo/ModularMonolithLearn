using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Events.SearchEvents;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.Results;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.Events
{
    internal class SearchEvents:IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
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

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Events);
        }
    }

}
