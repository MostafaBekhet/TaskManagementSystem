using FluentValidation;
using TMS.Application.Comments.Commands.CreateComment;

namespace TMS.Application.Comments.Validators
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.CommentText).NotEmpty().WithMessage("Comment Can not be empty or null");
        }
    }
}
