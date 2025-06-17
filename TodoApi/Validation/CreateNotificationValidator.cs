using FluentValidation;
using static TodoApi.DTOs.NotificationDto;

namespace TodoApi.Validation
{
    public class CreateNotificationValidator : AbstractValidator<CreateNotificationDTO>
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
