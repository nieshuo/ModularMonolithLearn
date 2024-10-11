using Eventity.Moudules.Events.Presentation;
using Evently.Modules.Events.Application.TicketTypes.CreateTicketType;
using Evently.Modules.Events.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Presentation.TicketTypes
{
    public static class CreateTicketType
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("ticket-types", async (CreateTicketTypeRequest request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateTicketTypeCommand(
                    request.EventId,
                    request.Name,
                    request.Price,
                    request.Currency,
                    request.Quantity));

                return Results.Ok(result);
            })
            .WithTags(Tags.TicketTypes);
        }

        internal sealed class CreateTicketTypeRequest
        {
            public Guid EventId { get; init; }

            public string Name { get; init; }

            public decimal Price { get; init; }

            public string Currency { get; init; }

            public decimal Quantity { get; init; }
        }
    }

}
