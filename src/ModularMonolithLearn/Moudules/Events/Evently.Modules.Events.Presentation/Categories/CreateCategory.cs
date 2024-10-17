using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.Categories.CreateCategory;
using Evently.Common.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Evently.Common.Presentation.Results;
using Evently.Common.Presentation.Endpoints;

namespace Evently.Modules.Events.Presentation.Categories
{
    internal class CreateCategory : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("categories", async (CreateCategoryRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateCategoryCommand(request.Name));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Categories);
        }

        internal sealed class CreateCategoryRequest
        {
            public string Name { get; init; }
        }
    }
}
