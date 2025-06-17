using FluentValidation;
using static TodoApi.DTOs.TaskListDto;

namespace TodoApi.Validation
{
    public class UpdateTaskListValidator : AbstractValidator<UpdateTaskListDTO>
    {
        public UpdateTaskListValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(120);

            RuleFor(y => y.Name)
                .MaximumLength(500);
        }
    }
}
