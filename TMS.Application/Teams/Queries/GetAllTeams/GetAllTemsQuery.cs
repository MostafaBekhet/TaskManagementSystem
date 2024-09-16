using MediatR;
using TMS.Application.Teams.Dtos;

namespace TMS.Application.Teams.Queries.GetAllTeams
{
    public class GetAllTemsQuery : IRequest<List<TeamViewDto>>
    {
    }
}
