using FluentValidation;
using TodoApi.DTOs;

namespace TodoApi.Validation
{
    public class CreateTagValidator : AbstractValidator<CreateTagDto>
    {
        public CreateTagValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[\p{L}\p{N}\s\-]+$")
                .WithMessage("Only letters, numbers, spaces and dashes are allowed.");
        }
    }
}
