using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Categories.ArchiveCategory;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.Results;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.Categories
{
    internal class ArchiveCategory : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("categories/{id}/archive", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(new ArchiveCategoryCommand(id));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            })
            .WithTags(Tags.Categories);
        }
    }
}
