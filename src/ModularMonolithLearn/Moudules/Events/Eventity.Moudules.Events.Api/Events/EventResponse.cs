﻿namespace Eventity.Moudules.Events.Api.Events
{
    public sealed record EventResponse(
        Guid Id,
        string Title,
        string Description,
        string Location,
        DateTime StartsAtUtc,
        DateTime? EndAtUtc
    );
}
