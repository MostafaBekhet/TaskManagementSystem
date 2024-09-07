using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Domain.Entities
{
    public  class TaskReminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReminderId { get; set; }

        public string UserId { get; set; } = default!;
        public User User { get; set; } = default!;

        public int TaskId { get; set; }
        public TaskItem TaskItem { get; set; } = default!;

        public DateTime ReminderDate { get; set; }
    }
}
