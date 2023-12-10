using FluentValidation;

namespace TSCore.Application.Ticket.Commands;

public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
{
    public UpdateTicketCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
        
        RuleFor(x => x.TicketTitle)
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(x => x.ShortDescription)
            .MaximumLength(1024);

        RuleFor(x => x.TeamId)
            .NotNull();

        RuleFor(x => x.TicketCreatorId)
            .NotNull();
    }
}