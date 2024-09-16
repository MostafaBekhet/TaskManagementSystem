using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Application.Teams.Dtos;
using TMS.Application.Users;
using TMS.Domain.Constants;
using TMS.Domain.Entities;

namespace TMS.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTemsQueryHandler(
        ILogger<GetAllTemsQueryHandler> _logger,
        IUserContext _userContext,
        ITeamRepository _teamRepository,
        IMapper _mapper) : IRequestHandler<GetAllTemsQuery, List<TeamViewDto>>
    {
        public async Task<List<TeamViewDto>> Handle(GetAllTemsQuery request, CancellationToken cancellationToken)
        {

            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("Getting all Teams for {UserEmail} with Role : {UserRole}",
                                        currentUser!.Email , currentUser.Roles.First());

            List<Team> result;

            if(currentUser.IsInRole(UserRoles.Administrator))
            {
                result = await _teamRepository.GetAllTeamWithMembers();
            }
            else
            {
                result = await _teamRepository.GetAllTeamWithMembers(currentUser.Id);
            }

            var teamDto = _mapper.Map<TeamViewDto>(result.First());

            Console.WriteLine(teamDto);

            Console.WriteLine(teamDto.TeamName);

            var teams = _mapper.Map<List<TeamViewDto>>(result);

            return teams;
        }
    }
}
