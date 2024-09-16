using MediatR;
using TMS.Application.Teams.Dtos;

namespace TMS.Application.Teams.Queries.GetTeamById
{
    public class GetTeamByIdQuery : IRequest<TeamDto>
    {
        public int TeamId { get; set; }

        public GetTeamByIdQuery(int teamId)
        {
            TeamId = teamId;
        }
    }
}
