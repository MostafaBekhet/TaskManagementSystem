using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandHandler(
        ILogger<CreateTeamCommandHandler> _logger,
        ITeamRepository _teamRepository,
        IMapper _mapper) : IRequestHandler<CreateTeamCommand, int>
    {
        public async Task<int> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Creating new Team with name: {TeamName}.", request.TeamName);

            var team = _mapper.Map<Team>(request);

            await _teamRepository.AddAsync(team);

            await _teamRepository.SaveChangesAsync();

            return team.TeamId;
        }
    }
}
