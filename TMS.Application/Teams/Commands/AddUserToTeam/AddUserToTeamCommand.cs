using MediatR;

namespace TMS.Application.Teams.Commands.AddUserToTeam
{
    public class AddUserToTeamCommand : IRequest<bool>
    {
        public int TeamId { get; set; }

        public string UserEmail { get; set; }

        public AddUserToTeamCommand(int teamId , string userEmail)
        {
            TeamId = teamId;
            UserEmail = userEmail;
        }
    }
}
