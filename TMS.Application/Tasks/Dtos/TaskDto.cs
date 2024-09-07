using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Application.Tasks.Dtos
{
    public class TaskDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Status { get; set; }
        public int PriorityLevel { get; set; }
        public DateTime DueDate { get; set; }
        public string CreatedByUserId { get; set; } = default!;
    }
}
