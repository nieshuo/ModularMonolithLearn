using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.SearchEvents
{
    public sealed record SearchEventsQuery(
        int? Status,
        Guid? CategoryId,
        DateTime? StartDate,
        DateTime? EndDate,
        int Page,
        int PageSize) : IQuery<SearchEventsResponse>;
}
