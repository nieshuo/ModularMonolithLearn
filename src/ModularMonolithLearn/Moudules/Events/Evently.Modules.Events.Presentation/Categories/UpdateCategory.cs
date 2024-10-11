using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Categories.UpdateCategory;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories
{
    public static class UpdateCategory
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("categories/{id}", async (Guid id, UpdateCategoryRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateCategoryCommand(id, request.Name));

                return Results.Ok(result);
            })
            .WithTags(Tags.Categories);
        }

        internal sealed class UpdateCategoryRequest
        {
            public string Name { get; init; }
        }
    }

}
