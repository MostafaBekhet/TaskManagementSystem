using MediatR;

namespace TMS.Application.Teams.Commands.RemoveUserFormTeam
{
    public class RemoveUserFormTeamQuery : IRequest<bool>
    {
        public int TeamId { get; set; }
        public string UserEmail { get; set; }

        public RemoveUserFormTeamQuery(int teamId , string userEmail)
        {
            TeamId = teamId;
            UserEmail = userEmail;
        }
    }
}
