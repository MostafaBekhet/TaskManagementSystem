using FluentValidation;
using TMS.Application.Teams.Commands.CreateTeam;

namespace TMS.Application.Teams.Validators
{
    public class CreateTeamValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamValidator()
        {
            RuleFor(x => x.TeamName).NotEmpty()
                                    .WithMessage("Team Name is required.")
                                    .Length(3 , 50)
                                    .WithMessage("Team Name min length 3 and max 50 characters.");
        }
    }
}
