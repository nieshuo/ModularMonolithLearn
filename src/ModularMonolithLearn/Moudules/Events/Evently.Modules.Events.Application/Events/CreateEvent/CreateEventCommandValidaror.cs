using FluentValidation;

namespace Evently.Modules.Events.Application.Events.CreateEvent
{
    internal sealed class CreateEventCommandValidaror : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidaror()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.StartsAtUtc).NotEmpty();
            RuleFor(x => x.EndsAtUtc).Must((cmd, endsAtUtc) =>
            {
                return endsAtUtc > cmd.StartsAtUtc;
            }).When(x => x.EndsAtUtc.HasValue);
        }
    }
}
