using FluentValidation;
using TMS.Application.Tasks.Commands.CreateTask;

namespace TMS.Application.Tasks.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Status).Must(value => value == 0 || value == 1 || value == 2)
                                         .WithMessage("Status must be 0 (ToDo), 1 (InProgress), or 2 (Compeleted).");
            RuleFor(x => x.PriorityLevel).Must(value => value == 0 || value == 1 || value == 2)
                                         .WithMessage("PriorityLevel must be 0 (Low), 1 (Medium), or 2 (High).");
            RuleFor(x => x.DueDate).GreaterThan(DateTime.Now).WithMessage("Due date must be in the future.");
            RuleFor(x => x.CreatedByUserId).NotEmpty().WithMessage("Id for user created the task is required.");
        }
    }
}
