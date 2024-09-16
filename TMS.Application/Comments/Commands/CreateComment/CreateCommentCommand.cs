using ErrorOr;
using MediatR;

namespace TMS.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<ErrorOr<bool>>
    {
        public string CommentText { get; set; }
        public int taskId { get; set; }

        public string CommentedBy { get; set; } = default!;

        public CreateCommentCommand(string comment , int taskId)
        {
            this.CommentText = comment;
            this.taskId = taskId;
        }
    }
}
