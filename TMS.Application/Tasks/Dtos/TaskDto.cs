namespace TMS.Application.Tasks.Dtos
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Status { get; set; }
        public int PriorityLevel { get; set; }
        public DateTime DueDate { get; set; }
        public string CreatedByUserId { get; set; } = default!;
    }
}
