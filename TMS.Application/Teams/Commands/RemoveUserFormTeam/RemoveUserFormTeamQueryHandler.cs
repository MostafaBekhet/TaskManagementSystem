using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Teams.Commands.RemoveUserFormTeam
{
    public class RemoveUserFormTeamQueryHandler(
        ILogger<RemoveUserFormTeamQueryHandler> _logger,
        ITeamRepository _teamRepository,
        IUserContext _userContext,
        UserManager<User> _userManager) : IRequestHandler<RemoveUserFormTeamQuery, bool>
    {
        public async Task<bool> Handle(RemoveUserFormTeamQuery request, CancellationToken cancellationToken)
        {
            
            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("{Email} is Deletting {UserEmail} from team with Id: {TeamId}",
                                currentUser?.Email, request.UserEmail, request.TeamId);

            var team = await _teamRepository.GetTeamByIdWithUserTeamsAsync(request.TeamId);

            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if(team == null)
            {
                throw new NotFoundException(nameof(Team) , request.TeamId);
            }

            if (user == null)
            {
                throw new NotFoundException(nameof(User) , request.UserEmail);
            }

            var userTeam = team.UserTeams.FirstOrDefault(ut => ut.UserId == user.Id);

            if(userTeam == null)
            {
                throw new NotFoundException($"{request.UserEmail} is not a member of the team with Id: {request.TeamId} to be deleted.");
            }

            team.UserTeams.Remove(userTeam);

            await _teamRepository.UpdateAsync(team);

            return true;
        }
    }
}
