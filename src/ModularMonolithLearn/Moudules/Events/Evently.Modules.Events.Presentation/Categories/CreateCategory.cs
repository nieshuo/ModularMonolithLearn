using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Categories.CreateCategory;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories
{
    public static class CreateCategory
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("categories", async (CreateCategoryRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateCategoryCommand(request.Name));

                return Results.Ok(result);
            })
            .WithTags(Tags.Categories);
        }

        internal sealed class CreateCategoryRequest
        {
            public string Name { get; init; }
        }
    }
}
