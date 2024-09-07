using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Domain.Entities
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }

        public string TeamName { get; set; } = default!;

        public ICollection<UserTeam> UserTeams { get; set; } = new List<UserTeam>();

        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
    }
}
