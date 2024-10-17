using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Categories.UpdateCategory;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.Results;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.Categories
{
    internal class UpdateCategory:IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("categories/{id}", async (Guid id, UpdateCategoryRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateCategoryCommand(id, request.Name));

                return result.Match(() => Results.Ok(), ApiResults.Problem);
            })
            .WithTags(Tags.Categories);
        }

        internal sealed class UpdateCategoryRequest
        {
            public string Name { get; init; }
        }
    }

}
