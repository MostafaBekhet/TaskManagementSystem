using TMS.Application.Comments.Dtos;
using TMS.Application.Teams.Dtos;
using TMS.Application.Users.Dtos;

namespace TMS.Application.Tasks.Dtos
{
    public record TaskDetailsDto(int TaskId,
                                 string Title,
                                 string Description,
                                 int Status,
                                 int PriorityLevel,
                                 DateTime DueDate,
                                 string CreatedByUserId,
                                 TeamTaskDto AssignedTeam,
                                 UserTaskDto AssignedUser,
                                 List<CommentDto> Comments);
}
