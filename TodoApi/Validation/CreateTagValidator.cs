using FluentValidation;
using TodoApi.DTOs;
using static TodoApi.DTOs.TagDto;

namespace TodoApi.Validation
{
    public class CreateTagValidator : AbstractValidator<CreateTagDTO>
    {
        public CreateTagValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
