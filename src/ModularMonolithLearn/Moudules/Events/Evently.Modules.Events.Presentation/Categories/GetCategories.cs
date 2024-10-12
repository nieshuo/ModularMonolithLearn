using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Categories.GetCategories;
using Evently.Modules.Events.Application.Categories.GetCategory;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories
{
    public static class GetCategories
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("categories", async (ISender sender) =>
            {
                Result<IReadOnlyCollection<CategoryResponse>> result = await sender.Send(new GetCategoriesQuery());

                return Results.Ok(result);
            })
            .WithTags(Tags.Categories);
        }
    }

}
