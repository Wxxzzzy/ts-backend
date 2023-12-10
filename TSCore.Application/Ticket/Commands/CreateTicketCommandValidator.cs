using FluentValidation;

namespace TSCore.Application.Ticket.Commands;

public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(x => x.TicketTitle)
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(x => x.ShortDescription)
            .MaximumLength(1024);

        RuleFor(x => x.TeamId)
            .NotNull();

    }
}