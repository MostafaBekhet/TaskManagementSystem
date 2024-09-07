using Microsoft.AspNetCore.Identity;

namespace TMS.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public DateTime CreatedAt { get; } = DateTime.Now;

        public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();

        public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();

        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();

        public ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();

        public ICollection<TaskReminder> TaskReminders { get; set; } = new List<TaskReminder>();
    }
}
