namespace TMS.Application.Tasks.Dtos
{
    public class UpdateTaskDto
    {
        public string? Title { get; set; }

        public string? Description { get; set; }


        public DateTime DueDate { get; set; }

        public int? Status { get; set; }

        public int? PriorityLevel { get; set; }
    }
}
