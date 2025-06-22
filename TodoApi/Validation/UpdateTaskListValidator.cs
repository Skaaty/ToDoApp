using FluentValidation;
using TodoApi.DTOs;

namespace TodoApi.Validation
{
    public class UpdateTaskListValidator : AbstractValidator<UpdateTaskListDto>
    {
        public UpdateTaskListValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(120);

            RuleFor(x => x.Description)
                .MaximumLength(300);
        }
    }
}
