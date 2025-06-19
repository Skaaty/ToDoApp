using FluentValidation;
using TodoApi.DTOs;

namespace TodoApi.Validation
{
    public class CreateTaskItemValidator : AbstractValidator<CreateTaskItemDto>
    {
        public CreateTaskItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(120);

            RuleFor(x => x.DueDate)
                .Must(d => d is null || d.Value.Date >= DateTime.UtcNow.Date)
                .WithMessage("Due date cannot be a date from the past.");

            RuleFor(x => x.TaskListId)
                .GreaterThan(0);

            RuleFor(x => x.TagIds)
                .NotNull();

            RuleForEach(x => x.TagIds)
                .GreaterThan(0);

            RuleFor(x => x.TagIds)
                .Must(ids => ids.Distinct().Count() == ids.Count())
                .WithMessage("Tag Ids must be unique.");
        }
    }
}
