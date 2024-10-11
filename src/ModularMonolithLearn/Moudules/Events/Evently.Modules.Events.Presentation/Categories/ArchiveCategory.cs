using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Categories.ArchiveCategory;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories
{
    public static class ArchiveCategory
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("categories/{id}/archive", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(new ArchiveCategoryCommand(id));

                return Results.Ok(result);
            })
            .WithTags(Tags.Categories);
        }
    }
}
