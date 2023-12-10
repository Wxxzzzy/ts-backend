using FluentValidation;

namespace TSCore.Application.Team.Commands;

public class AddMemberToTeamCommandValidator : AbstractValidator<AddMemberToTeamCommand>
{
    public AddMemberToTeamCommandValidator()
    {
        RuleFor(x => x.TeamId).NotNull();
        RuleFor(x => x.TeamId).NotNull();
    }
}