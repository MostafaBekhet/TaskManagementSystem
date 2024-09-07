using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMS.Domain.Entities.Enums;

namespace TMS.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }

        public string Title { get; set; } = default!;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public Status Status { get; set; } = default!;

        public PriorityLevel PriorityLevel { get; set; } = default!;

        public string CreatedByUserId { get; set; } = default!;
        public User CreatedByUser { get; set; } = default!;


        public string? AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        public int? AssignedToTeamId { get; set; }
        public Team? AssigendToTeam { get; set; }

        public ICollection<TaskAttachment> Attachments { get; set; } = new List<TaskAttachment>();

        public ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

        public ICollection<TaskReminder> TaskReminders { get; set; } = new List<TaskReminder>();
    }
}
