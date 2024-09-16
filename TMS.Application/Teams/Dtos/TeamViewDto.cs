using TMS.Application.Tasks.Dtos;
using TMS.Application.Users.Dtos;

namespace TMS.Application.Teams.Dtos
{
    public record TeamViewDto(int TeamId, string TeamName, List<UserViewDto> Members, List<TaskViewDto> Tasks);
}
