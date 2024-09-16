using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Application.Users;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Teams.Commands.AddUserToTeam
{
    public class AddUserToTeamCommandHandler(
        ILogger<AddUserToTeamCommandHandler> _logger,
        ITeamRepository _teamRepository,
        UserManager<User> _userManager,
        IUserContext _userContext) : IRequestHandler<AddUserToTeamCommand, bool>
    {
        public async Task<bool> Handle(AddUserToTeamCommand request, CancellationToken cancellationToken)
        {

            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("{Email} adding {UserEmail} to Team with Id: {TeamId}.",
                                       currentUser?.Email, request.UserEmail, request.TeamId);

            var team = await _teamRepository.GetTeamByIdWithUserTeamsAsync(request.TeamId);

            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if(team == null)
            {
                throw new NotFoundException(nameof(Team) , request.TeamId);
            }

            if(user == null)
            {
                throw new NotFoundException(nameof(User) , request.UserEmail);
            }

            if (team.UserTeams.Any(ut => ut.UserId == user.Id))
            {
                throw new NotValidOperationException($"User with email {user.Email} is already a member of this team");
            }

            team.UserTeams.Add(new UserTeam
            {
                UserId = user.Id,
                TeamId = request.TeamId
            });

            await _teamRepository.UpdateAsync(team);

            return true;
        }
    }
}
