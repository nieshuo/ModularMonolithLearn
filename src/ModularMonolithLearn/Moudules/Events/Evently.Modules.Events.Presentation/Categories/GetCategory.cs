using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Categories.GetCategory;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories
{
    public static class GetCategory
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("categories/{id}", async (Guid id, ISender sender) =>
            {
                Result<CategoryResponse> result = await sender.Send(new GetCategoryQuery(id));

                return Results.Ok(result);
            })
            .WithTags(Tags.Categories);
        }
    }
}
