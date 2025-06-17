using FluentValidation;
using static TodoApi.DTOs.TagDto;

namespace TodoApi.Validation
{
    public class UpdateTagValidator : AbstractValidator<CreateTagDTO>
    {
        public UpdateTagValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
