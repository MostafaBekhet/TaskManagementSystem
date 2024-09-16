using FluentValidation;
using TMS.Application.Tasks.Dtos;

namespace TMS.Application.Tasks.Validators
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Title)
                    .NotEmpty()
                    .WithMessage("Title must not be empty or null.")
                    .When(x => x.Title != null);

            RuleFor(x => x.Description)
                    .NotEmpty()
                    .WithMessage("Description must not be empty or null.")
                    .When(x => x.Description != null);

            RuleFor(x => x.Status).Must(value => value == 0 || value == 1 || value == 2)
                                  .WithMessage("Status must be 0 (ToDo), 1 (InProgress), or 2 (Compeleted).")
                                  .When(x => x.Status != default);

            RuleFor(x => x.PriorityLevel).Must(value => value == 0 || value == 1 || value == 2)
                                         .WithMessage("PriorityLevel must be 0 (Low), 1 (Medium), or 2 (High).")
                                         .When(x => x.PriorityLevel != default);

            RuleFor(x => x.DueDate).GreaterThan(DateTime.Now)
                                   .WithMessage("Due date must be in the future.")
                                   .When(x => x.DueDate != default(DateTime));

        }
    }
}
