using FluentValidation;
using TodoApi.DTOs;
namespace TodoApi.Validation
{
    public class TaskItemQueryValidator : AbstractValidator<TaskItemQuery>
    {
        public TaskItemQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100);

            RuleFor(x => x.TagId)
                .Must(id => id is null || id > 0);

            RuleFor(x => x.DueDate)
                .Must(d => d is null || d.Value.Date >= DateTime.UtcNow)
                .WithMessage("DueDate cannot be a date from the past.");
        }
    }
}
