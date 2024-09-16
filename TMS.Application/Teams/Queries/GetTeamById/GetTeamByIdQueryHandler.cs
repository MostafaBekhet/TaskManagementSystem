using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Application.Teams.Dtos;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Teams.Queries.GetTeamById
{
    public class GetTeamByIdQueryHandler(
        ILogger<GetTeamByIdQueryHandler> _logger,
        ITeamRepository _teamRepository,
        IMapper _mapper) : IRequestHandler<GetTeamByIdQuery, TeamDto>
    {
        public async Task<TeamDto> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Getting Team By Id: {TeamId}.", request.TeamId);

            var result = await _teamRepository.GetTeamByIdAsync(request.TeamId);

            if (result is null)
            {
                throw new NotFoundException(nameof(Team) , request.TeamId);
            }

            var team = _mapper.Map<TeamDto>(result);

            return team;
        }
    }
}
