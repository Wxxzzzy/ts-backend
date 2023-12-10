using FluentValidation;
using TSCore.Application.Ticket.Commands;

namespace TSCore.Application.Comment.Commands;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.Content).MaximumLength(2048);
    }
}