using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Categories.GetCategory;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.Results;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.Categories
{
    internal class GetCategory:IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("categories/{id}", async (Guid id, ISender sender) =>
            {
                Result<CategoryResponse> result = await sender.Send(new GetCategoryQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Categories);
        }
    }
}
