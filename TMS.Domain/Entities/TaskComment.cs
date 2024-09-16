using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Domain.Entities
{
    public class TaskComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public string CommentText { get; set; } = default!;

        public string CommentedBy { get; set; } = default!;
        public User User { get; set; } = default!;

        public int TaskId { get; set; }
        public TaskItem TaskItem { get; set; } = default!;

        public DateTime CommentDate { get; set; }

        public TaskComment()
        {
            CommentDate = DateTime.Now;
        }
    }
}
