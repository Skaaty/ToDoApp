using FluentValidation;
using TodoApi.DTOs;

namespace TodoApi.Validation
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationDto>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.TaskItemId)
                .NotEmpty();

            RuleFor(x => x.Message)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
