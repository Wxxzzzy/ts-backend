using FluentValidation;

namespace TSCore.Application.Comment.Commands;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Content).MaximumLength(2048);
        RuleFor(x => x.TicketId).NotNull();
    }
}