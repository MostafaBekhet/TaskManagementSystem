using MediatR;

namespace TMS.Application.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommand : IRequest
    {
        public int TeamId { get; set; }

        public DeleteTeamCommand(int id)
        {
            TeamId = id;
        }
    }
}
