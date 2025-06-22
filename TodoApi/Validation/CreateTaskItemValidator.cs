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
                .GreaterThanOrEqualTo(DateTime.Today)
                .When(x => x.DueDate.HasValue);

            RuleFor(x => x.Priority)
                .IsInEnum()
                .When(x => x.Priority.HasValue);

            RuleFor(x => x.TaskListId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.TagIds)
                .NotNull();

            RuleForEach(x => x.TagIds)
                .GreaterThan(0);

            RuleFor(x => x.TagIds)
                .Must(list => list == null || list.Count() <= 10)
                .WithMessage("A task can have at most 10 tags.")
                .Must(list => list == null || list.Distinct().Count() == list.Count())
                .WithMessage("Duplicate tags are not allowed.");
        }
    }
}
