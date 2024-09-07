using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS.Domain.Entities
{
    public class TaskAttachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttachmentId;

        public int TaskId {  get; set; }
        public TaskItem TaskItem { get; set; } = default!;

        public string FilePath { get; set; } = default!;

        public DateTime UploadedDate { get; } = DateTime.Now;
    }
}
