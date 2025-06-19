using FluentValidation;
using TodoApi.DTOs;

namespace TodoApi.Validation
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationDto>
    {
        public CreateNotificationValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.Message)
                .NotEmpty()
                .MaximumLength(250);
        }
    }
}
