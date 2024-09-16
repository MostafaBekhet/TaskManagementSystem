using MediatR;

namespace TMS.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<int>
    {
        public string TeamName { get; set; } = default!;
    }
}
