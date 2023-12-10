using FluentValidation;

namespace TSCore.Application.Team.Commands;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.TeamName)
            .NotEmpty();
    }
}