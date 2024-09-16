using MediatR;
using Microsoft.Extensions.Logging;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Domain.Exceptions;

namespace TMS.Application.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandHandler(
        ILogger<DeleteTeamCommandHandler> logger,
        ITeamRepository _teamRepository) : IRequestHandler<DeleteTeamCommand>
    {
        public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetTeamByIdAsync(request.TeamId);

            if (team == null)
            {
                throw new NotFoundException(nameof(Team) , request.TeamId);
            }

            await _teamRepository.DeleteAsync(request.TeamId);

            await _teamRepository.SaveChangesAsync();

        }
    }
}
