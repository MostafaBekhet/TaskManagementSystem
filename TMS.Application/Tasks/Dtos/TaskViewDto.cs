namespace TMS.Application.Tasks.Dtos
{
    public record TaskViewDto(int TaskId, string Title, int Status, int PriorityLevel, DateTime DueDate);
}
