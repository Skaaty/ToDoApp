using FluentValidation;
using TodoApi.DTOs;

namespace TodoApi.Validation
{
    public class UpdateTagValidator : AbstractValidator<CreateTagDto>
    {
        public UpdateTagValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[\p{L}\p{N}\s\-]+$");
        }
    }
}
